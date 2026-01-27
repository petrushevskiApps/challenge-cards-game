using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService
{
    /// <summary>
    /// Concrete implementation of <see cref="IInfiniteScrollList" />.
    /// </summary>
    public class InfiniteScrollList : MonoBehaviour, IInfiniteScrollList
    {
        private const float VISIBILITY_TOLERANCE = 0.15f;

        [SerializeField]
        private RectTransform _container;

        [SerializeField]
        private RectTransform _viewPort;

        [SerializeField]
        private ScrollRect _scrollRect;

        [SerializeField]
        private float _verticalSpacing;

        [SerializeField]
        private float _scrollSpeed = 100f;

        [SerializeField]
        private float _scrollLength = 300f;

        [SerializeField]
        private float _bottomPadding;

        // Events
        public event EventHandler OnListEndEvent;
        public event EventHandler ScrolledToTopEvent;
        public event EventHandler ScrolledToBottomEvent;
        public event EventHandler<ListRow> RowVisibleEvent;
        public event EventHandler<ListRow> RowHiddenEvent;
        public event EventHandler RowsVisibilityUpdatedEvent;
        public event EventHandler ScrollingCompletedEvent;
        public event EventHandler RowsAddedEvent;
        
        private readonly List<ListRow> _rows = new();

        private float _headerSize;
        private bool _isScrollActive;
        private bool _isScrollBottom;
        private bool _isScrollTop;
        private bool _lockScroll = true;

        private Vector2 _previousScrollPercent = new(1f, 1f);
        private float _rowHeight;
        private Coroutine _scrollCheckCoroutine;
        private Coroutine _scrollDownCoroutine;
        private float _scrollThreshold;

        // Coroutines
        private Coroutine _scrollUpCoroutine;
        private RectHeightListener _viewportHeightListener;

        public bool IsScrolledToTop
        {
            get => _isScrollTop;
            set
            {
                if (value != _isScrollTop) ScrolledToTopEvent?.Invoke(this, EventArgs.Empty);
                _isScrollTop = value;
            }
        }

        public bool IsScrolledToBottom
        {
            get => _isScrollBottom;
            set
            {
                if (value != _isScrollBottom) ScrolledToBottomEvent?.Invoke(this, EventArgs.Empty);
                _isScrollBottom = value;
            }
        }

        private float ViewPortHeight => _scrollRect.viewport.rect.height;
        public float RowsInView { get; private set; }

        private void Awake()
        {
            _viewportHeightListener = _scrollRect.viewport.gameObject.GetComponent<RectHeightListener>();
            if (_viewportHeightListener == null)
            {
                _viewportHeightListener = _scrollRect.viewport.gameObject.AddComponent<RectHeightListener>();
            }
            _viewportHeightListener.HeightUpdatedEvent += OnViewportHeightUpdated;
            _scrollRect.onValueChanged.AddListener(OnScrolling);
        }

        private void OnDestroy()
        {
            _viewportHeightListener.HeightUpdatedEvent -= OnViewportHeightUpdated;
            _scrollRect.onValueChanged.RemoveListener(OnScrolling);
        }
        
        public void Initialize(
            float headerSize,
            float elementHeight)
        {
            _headerSize = headerSize;
            _rowHeight = elementHeight;
        }

        public void AddRows(int rowsPerPage, int scrollToRow = -1)
        {
            if (!gameObject.activeInHierarchy)
            {
                Debug.LogWarning("Rows can not be added. List is not active in hierarchy");
                return;
            }

            StartCoroutine(
                WaitForViewPortHeight(
                    () =>
                    {
                        var newRows = new List<ListRow>();
                        for (var i = _rows.Count; i < _rows.Count + rowsPerPage; i++)
                        {
                            var position = CalculateRowPosition(i);
                            newRows.Add(new ListRow(i, position));
                        }

                        _rows.AddRange(newRows);
                        UpdateContainerSize(_rows.Count);
                        if (scrollToRow >= 0)
                        {
                            ToElement(scrollToRow);
                        }
                        RowsAddedEvent?.Invoke(this, EventArgs.Empty);
                        NotifyRowsVisibility();
                    }));
        }

        public void ClearList()
        {
            StopAllScrollCoroutines();
            _rows.Clear();
            ToTop();
            UpdateContainerSize(_rows.Count);
        }

        /// <inheritdoc cref="IInfiniteScrollList.ScrollUp" />
        public void ScrollUp()
        {
            if (_scrollUpCoroutine != null || !gameObject.activeInHierarchy)
            {
                return;
            }
            StopActiveCoroutine(ref _scrollDownCoroutine);
            _scrollUpCoroutine = StartCoroutine(Scroll(-1));
        }

        /// <inheritdoc cref="IInfiniteScrollList.ScrollDown" />
        public void ScrollDown()
        {
            if (_scrollDownCoroutine != null)
            {
                return;
            }
            StopActiveCoroutine(ref _scrollUpCoroutine);
            _scrollDownCoroutine = StartCoroutine(Scroll(1));
        }

        /// <inheritdoc cref="IInfiniteScrollList.ToTop" />
        public void ToTop()
        {
            StopAllScrollCoroutines();
            _container.anchoredPosition = GetContainerPosition(0);
        }

        /// <inheritdoc cref="IInfiniteScrollList.ToBottom" />
        public void ToBottom()
        {
            StopAllScrollCoroutines();
            _container.anchoredPosition = GetMaximumContainerPosition();
        }

        /// <inheritdoc cref="IInfiniteScrollList.ToElement" />
        public void ToElement(int rowIndex, AlignAt elementAlignment = AlignAt.Top)
        {
            if (!_isScrollActive || rowIndex >= _rows.Count)
            {
                return;
            }
            StopAllScrollCoroutines();
            var index = Mathf.Clamp(rowIndex, 0, int.MaxValue);

            float offset = 0;
            if (index == 0 || elementAlignment == AlignAt.Top)
                offset = _rowHeight / 2;
            else if (elementAlignment == AlignAt.Bottom) offset = _rowHeight * RowsInView - _rowHeight / 2;
            var rowPosition = (CalculateRowPosition(index) + offset) * -1;
            // When we reach the end of the content we can Over-Scroll
            // which will cause weird behavior on the scrollbar.
            // That is why we need to remove the extra scroll size.
            var normalize = rowPosition - Mathf.Max(0, ViewPortHeight + rowPosition - _container.sizeDelta.y);
            _container.anchoredPosition = GetContainerPosition(Mathf.Max(0, normalize));
        }

        public bool IsViewFullyVisible(RectTransform rect)
        {
            // Get the world corners of the target RectTransform
            var targetCorners = new Vector3[4];
            rect.GetWorldCorners(targetCorners);

            // Get the world corners of the viewport
            var viewportCorners = new Vector3[4];
            _viewPort.GetWorldCorners(viewportCorners);

            // Expand viewport bounds vertically to account for tolerance
            var minY = viewportCorners[0].y - VISIBILITY_TOLERANCE; // Bottom boundary
            var maxY = viewportCorners[1].y + VISIBILITY_TOLERANCE; // Top boundary

            // Check the bottom-left and top-left corners of the target
            var bottomLeftInside = targetCorners[0].y >= minY && targetCorners[0].y <= maxY; // Bottom-left corner
            var topLeftInside = targetCorners[1].y >= minY && targetCorners[1].y <= maxY; // Top-left corner

            // Both corners must be inside the vertical bounds
            return bottomLeftInside && topLeftInside;
        }

        private void OnViewportHeightUpdated(object sender, float viewportHeight)
        {
            UpdateContainerSize(_rows.Count);
            NotifyRowsVisibility();
        }

        private void NotifyRowsVisibility()
        {
            foreach (var row in _rows)
            {
                if (IsCenterInViewPort(row.Position))
                {
                    RowVisibleEvent?.Invoke(this, row);
                }
                else
                {
                    RowHiddenEvent?.Invoke(this, row);
                }
            }
            RowsVisibilityUpdatedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateContainerSize(int rowsCount)
        {
            var containerHeight = _headerSize + rowsCount * (_verticalSpacing + _rowHeight) + _bottomPadding;
            _container.sizeDelta = new Vector2(_container.sizeDelta.x, containerHeight);
            RowsInView = ViewPortHeight / _rowHeight;
            _scrollThreshold = RowsInView / _container.rect.height;
            _lockScroll = false;
            _isScrollActive = _container.sizeDelta.y > ViewPortHeight;
            SetScrollNavigationState();
        }

        private void OnScrolling(Vector2 scrollPercent)
        {
            if (Mathf.Abs(_previousScrollPercent.y - scrollPercent.y) > _scrollThreshold)
            {
                _previousScrollPercent = scrollPercent;
                NotifyRowsVisibility();
                _scrollCheckCoroutine ??= StartCoroutine(ScrollingCheck());
            }

            if (float.IsInfinity(_scrollThreshold))
            {
                _previousScrollPercent = scrollPercent;
            }
            else if (!_lockScroll && scrollPercent.y <= _scrollThreshold)
            {
                _lockScroll = true;
                OnListEndEvent?.Invoke(this, EventArgs.Empty);
            }

            SetScrollNavigationState();
        }

        private IEnumerator ScrollingCheck()
        {
            yield return new WaitUntil(() => _scrollRect.velocity.magnitude < 0.1f);
            ScrollingCompletedEvent?.Invoke(this, EventArgs.Empty);
            _scrollCheckCoroutine = null;
        }

        private void SetScrollNavigationState()
        {
            Vector3 localPosition = _container.anchoredPosition;
            IsScrolledToTop = !_isScrollActive || Mathf.Approximately(localPosition.y, 0f);
            IsScrolledToBottom = !_isScrollActive || localPosition.y >= GetMaximumContainerPosition().y;
        }

        private float CalculateRowPosition(int index)
        {
            var totalSpacing = _headerSize + (index + 1) * _verticalSpacing;
            var prevRow = index * _rowHeight;
            var rowCenter = _rowHeight / 2f;

            return (totalSpacing + prevRow + rowCenter) * -1;
        }

        private IEnumerator Scroll(int direction)
        {
            Vector3 currentPos = _container.anchoredPosition;
            var maxYPosition = GetMaximumContainerPosition().y;
            var targetPositionY = Mathf.Clamp(currentPos.y + _scrollLength * direction, 0, maxYPosition);
            var targetPos = new Vector3(currentPos.x, targetPositionY, currentPos.z);
            var distance = Vector3.Distance(currentPos, targetPos);

            var duration = distance / _scrollSpeed;

            var timePassed = 0f;

            while (timePassed < duration)
            {
                // always a factor between 0 and 1
                var factor = timePassed / duration;

                _container.anchoredPosition = Vector3.Lerp(currentPos, targetPos, factor);

                // increase timePassed with Mathf.Min to avoid overshooting
                timePassed += Mathf.Min(Time.deltaTime, duration - timePassed);

                // "Pause" the routine here, render this frame and continue
                // from here in the next frame
                yield return null;
            }

            _container.anchoredPosition = targetPos;
            StopAllScrollCoroutines();
        }

        private Vector3 GetContainerPosition(float y)
        {
            Vector3 containerPosition = _container.anchoredPosition;
            return new Vector3(containerPosition.x, y, containerPosition.z);
        }

        private Vector3 GetMaximumContainerPosition()
        {
            return GetContainerPosition(_container.rect.height - ViewPortHeight);
        }

        public bool IsRowVisible(int rowIndex)
        {
            return IsCenterInViewPort(_rows[rowIndex].Position);
        }

        private bool IsCenterInViewPort(float rowPosition)
        {
            // Convert the point's Y position in content to world space
            var worldPoint = _container.TransformPoint(new Vector3(0, rowPosition, 0));

            // Convert the world space position to the local space of the viewport
            var localPointInViewport = _viewPort.InverseTransformPoint(worldPoint);

            // Get the vertical bounds of the viewport and add a tolerance of third of the row height.
            var tolerance = _rowHeight;
            var viewPortRect = _viewPort.rect;
            var viewportMinY = viewPortRect.yMin - tolerance;
            var viewportMaxY = viewPortRect.yMax + tolerance;

            // Check if the Y position is within the viewport's vertical bounds
            return localPointInViewport.y >= viewportMinY && localPointInViewport.y <= viewportMaxY;
        }

        private IEnumerator WaitForViewPortHeight(Action onEndOfFrameContinue)
        {
            yield return new WaitUntil(() => _viewPort.rect.height != 0);
            onEndOfFrameContinue?.Invoke();
        }

        private void StopAllScrollCoroutines()
        {
            StopActiveCoroutine(ref _scrollUpCoroutine);
            StopActiveCoroutine(ref _scrollDownCoroutine);
            StopActiveCoroutine(ref _scrollCheckCoroutine);
        }

        private void StopActiveCoroutine(ref Coroutine activeCoroutine)
        {
            if (activeCoroutine == null)
            {
                return;
            }
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }
    }
}