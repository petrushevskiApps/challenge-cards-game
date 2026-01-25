using System;
using TMPro;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UserInterface.Views;
using Zenject;

namespace UserInterface.Screens
{
    public class MainScreen: UIScreen, IMainScreenView
    {
        [SerializeField]
        private Button _createPackageButton;
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private Button _settingsButton;
        [SerializeField]
        private TextMeshProUGUI _message;
        [SerializeField]
        private ListView _listView;
        
        public IListView ListView => _listView;
        
        private IMainScreenController _controller;

        [Inject]
        public void Initialize(IMainScreenController mainScreenController)
        {
            _controller = mainScreenController;
        }

        public override void Show<TArguments>(TArguments navArguments)
        {
            _createPackageButton.onClick.AddListener(_controller.CreatePackageClicked);
            _playButton.onClick.AddListener(_controller.PlayClicked);
            _settingsButton.onClick.AddListener(_controller.SettingsClicked);
            _controller.Setup(this);
            base.Show(navArguments);
        }

        public override void Resume()
        {
            base.Resume();
            _controller.ScreenResumed();
        }

        public override void Hide()
        {
            base.Hide();
            _controller.ScreenHidden();
        }

        public override void Close()
        {
            base.Close();
            _createPackageButton.onClick.RemoveListener(_controller.CreatePackageClicked);
            _playButton.onClick.RemoveListener(_controller.PlayClicked);
            _settingsButton.onClick.RemoveListener(_controller.SettingsClicked);
        }

        public void SetMessage(bool isVisible, string message)
        {
            _message.gameObject.SetActive(isVisible);
            if (isVisible)
            {
                _message.text = message;
            }
        }

        public void SetPlayButton(bool isVisible)
        {
            _playButton.gameObject.SetActive(isVisible);
        }
    }
}