using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class PopupProvider : MonoBehaviour, IWindowProvider
    {
        [SerializeField]
        [Tooltip("List of screens to be provided to Navigation Controller")]
        private List<UIPopup> _popups = new();

        [Inject]
        private INavigationManager _navigationManager;

        [Inject]
        private IPopupNavigation _popupNavigation;

        private void Awake()
        {
            _navigationManager.AllScreensClosedEvent += OnAllScreensClosed;
        }

        private void OnDestroy()
        {
            _navigationManager.AllScreensClosedEvent -= OnAllScreensClosed;
        }

        public IWindow GetScreen<T>() where T : IWindow
        {
            return _popups.Find(x => x.GetType() == typeof(T));
        }

        private void OnAllScreensClosed(object sender, EventArgs e)
        {
            _popupNavigation.ShowExitGamePopup();
        }
    }
}