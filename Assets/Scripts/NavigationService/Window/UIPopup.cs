using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using PrimeTween;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public abstract class UIPopup : MonoBehaviour, IWindow, IUiPopup
    {
        [Header("Popup Navigation")]
        [SerializeField]
        [Tooltip("True: Popup goes on backstack when hidden. False: One time popup.")]
        private bool _isBackStackable = true;
        [SerializeField]
        [Tooltip("True: When this popup is shown the Game Time is set to 0. False: Ignores this setting.")]
        private bool _pauseGameWhenActive;
        [SerializeField]
        [Tooltip("Clickable background which disposes the popup when clicked.Same as the Back Button.")]
        private Button _popupClickableBackground;
        [SerializeField]
        [Tooltip("Button which discards popups.")]
        private Button _closeButton;

        [Header("Animation")]
        [SerializeField]
        [Tooltip("Group holding the popup content.")]
        private GameObject _contentGroup;
        [SerializeField]
        [Tooltip("Duration of the popup animation in seconds.")]
        private float _animationDuration = 0.3f;
        [SerializeField]
        private CanvasGroup _canvasGroup;
        
        // Injected
        protected INavigationManager NavigationManager;
        private IPauseGameController _pauseGameController;

        private Sequence _currentAnimation;

        // Events
        public event EventHandler PopupShownEvent;
        public event EventHandler PopupResumedEvent;
        public event EventHandler PopupHiddenEvent;
        public event EventHandler PopupClosedEvent;

        public virtual string ScreenTitle => gameObject.name;
        public bool IsBackStackable => _isBackStackable;

        [Inject]
        public void Initialize(
            INavigationManager navigationManager, 
            IPauseGameController pauseGameController)
        {
            NavigationManager = navigationManager;
            _pauseGameController = pauseGameController;
        }

        public virtual void Show<TArguments>(TArguments navArguments)
        {
            PopupShownEvent?.Invoke(this, EventArgs.Empty);
            Resume();
        }

        public virtual void Resume()
        {
            PopupResumedEvent?.Invoke(this, EventArgs.Empty);
            _popupClickableBackground.onClick.AddListener(BackgroundClicked);
            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(CloseButtonClicked);
                
            }
            gameObject.SetActive(true);
            PauseGame(true);

            AnimateShow();
        }

        public virtual void Hide()
        {
            PopupHiddenEvent?.Invoke(this, EventArgs.Empty);
            _popupClickableBackground.onClick.RemoveListener(BackgroundClicked);
            if (_closeButton != null)
            {
                _closeButton.onClick.RemoveListener(CloseButtonClicked);
            }

            AnimateHiding(() =>
            {
                gameObject.SetActive(false);
                PauseGame(false);
            });
        }

        private void AnimateShow()
        {
            _currentAnimation.Stop();
            _contentGroup.transform.localScale = Vector3.zero;
            
            _canvasGroup.alpha = 1f;

            _currentAnimation = Sequence.Create(useUnscaledTime: true)
                .Chain(Tween.Scale(
                    _contentGroup.transform,
                    endValue: Vector3.one,
                    duration: _animationDuration,
                    ease: Ease.OutBack
                ));
        }

        private void AnimateHiding(Action onComplete)
        {
            _currentAnimation.Stop();

            _currentAnimation = Sequence.Create(useUnscaledTime: true)
                .Group(Tween.Scale(_contentGroup.transform, endValue: Vector3.zero, duration: _animationDuration,
                    ease: Ease.InBack))
                .Group(Tween.Alpha(_canvasGroup, endValue: 0f, duration: _animationDuration, ease: Ease.InQuad))
                .Chain(Tween.Delay(duration: 0f, onComplete: () =>
                {
                    onComplete?.Invoke();
                    _canvasGroup.alpha = 1f;
                    _contentGroup.transform.localScale = Vector3.one;
                }));
        }

        public virtual void Close()
        {
            PopupClosedEvent?.Invoke(this, EventArgs.Empty);
            Hide();
        }

        public void OnBackTriggered()
        {
            NavigationManager.GoBack();
        }

        private void PauseGame(bool pause)
        {
            if (_pauseGameWhenActive)
            {
                _pauseGameController.TogglePauseGame(pause);
            }
        }

        protected virtual void CloseButtonClicked()
        {
            NavigationManager.GoBack();
        }

        protected virtual void BackgroundClicked()
        {
            NavigationManager.GoBack();
        }
    }
}