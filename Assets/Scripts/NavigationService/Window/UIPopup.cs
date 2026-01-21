using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public abstract class UIPopup : MonoBehaviour, IWindow, IUiPopup
    {
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

        // Injected
        protected INavigationManager NavigationManager;
        private IPauseGameController _pauseGameController;

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
        }
        
        public virtual void Hide()
        {
            PopupHiddenEvent?.Invoke(this, EventArgs.Empty);
            _popupClickableBackground.onClick.AddListener(BackgroundClicked);
            if (_closeButton != null)
            {
                _closeButton.onClick.AddListener(CloseButtonClicked);
            }
            gameObject.SetActive(false);
            PauseGame(false);
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

        protected abstract void CloseButtonClicked();
        protected abstract void BackgroundClicked();
    }
}