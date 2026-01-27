using System;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.InfiniteScrollList
{
    public class RectHeightListener : MonoBehaviour
    {
        private float _previousHeight;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnRectTransformDimensionsChange()
        {
            if (_rectTransform == null || !(Math.Abs(_rectTransform.rect.height - _previousHeight) > 0.000001f)) return;
            _previousHeight = _rectTransform.rect.height;
            HeightUpdatedEvent?.Invoke(this, _previousHeight);
        }

        public event EventHandler<float> HeightUpdatedEvent;
    }
}