using PetrushevskiApps.WhosGame.Scripts.Controllers.Screens;
using PetrushevskiApps.WhosGame.Scripts.NavigationService.Window;
using PetrushevskiApps.WhosGame.Scripts.Views.List;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PetrushevskiApps.WhosGame.Scripts.Views.Screens
{
    public class MainScreen: UIScreen, IMainScreenView
    {
        [SerializeField]
        private TextMeshProUGUI _packageListLabel;
        [SerializeField]
        private Button _createPackageButton;
        [SerializeField]
        private TextMeshProUGUI _createPackageButtonLabel;
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private TextMeshProUGUI _playButtonLabel;
        [SerializeField]
        private Button _settingsButton;
        [SerializeField]
        private GameObject _messageGroup;
        [SerializeField]
        private TextMeshProUGUI _message;
        [SerializeField]
        private ListView _listView;
        [SerializeField]
        private ToggleGroup _toggleGroup;
        public IListView ListView => _listView;
        public ToggleGroup PackagesToggleGroup => _toggleGroup;
        
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

        public void SetMessageGroupVisibility(bool isVisible)
        {
            _messageGroup.SetActive(isVisible);
        }
        public void SetMessage(string message)
        {
            _message.text = message;
        }

        public void SetPlayButton(bool isVisible)
        {
            _playButton.gameObject.SetActive(isVisible);
        }

        public void SetPackageListLabel(string label)
        {
            _packageListLabel.text = label;
        }
        public void SetCreatePackageButtonLabel(string label)
        {
            _createPackageButtonLabel.text = label;
        }
        public void SetPlayButtonLabel(string label)
        {
            _playButtonLabel.text = label;
        }
    }
}