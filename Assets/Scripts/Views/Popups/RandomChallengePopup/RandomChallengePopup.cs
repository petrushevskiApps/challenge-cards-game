using PetrushevskiApps.WhosGame.Scripts.Controllers.Popups;
using PetrushevskiApps.WhosGame.Scripts.NavigationService.Window;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PetrushevskiApps.WhosGame.Scripts.Views.Popups.RandomChallengePopup
{
    public class RandomChallengePopup: UIPopup, IRandomChallengePopupView
    {
        [Header("Random Challenge Popup properties")]
        [SerializeField]
        private TextMeshProUGUI _title;
        [SerializeField]
        private TextMeshProUGUI _description;
        [SerializeField]
        private Button _generateButton;
        [SerializeField]
        private TextMeshProUGUI _generateButtonTitle;
        [SerializeField]
        private Toggle _fiveElementsToggle;
        [SerializeField]
        private Toggle _tenElementsToggle;
        [SerializeField]
        private Toggle _fifteenElementsToggle;

        // Injected
        private IRandomChallengePopupController _controller;

        [Inject]
        public void Initialize(IRandomChallengePopupController controller)
        {
            _controller = controller;
        }
        
        public override void Show<TArguments>(TArguments navArguments)
        {
            base.Show(navArguments);

            if (navArguments is RandomChallengePopupNavigationArguments args)
            {
                _controller.Setup(this, args.OnPopupResult);
            }
            _generateButton.onClick.AddListener(_controller.GenerateClicked);
            _fiveElementsToggle.onValueChanged.AddListener(FiveToggleSelected);
            _tenElementsToggle.onValueChanged.AddListener(TenToggleSelected);
            _fifteenElementsToggle.onValueChanged.AddListener(FifteenToggleSelected);
            FiveToggleSelected(true);
            _controller.ScreenShown();
        }

        public override void Close()
        {
            base.Close();
            
            _generateButton.onClick.RemoveListener(_controller.GenerateClicked);
            _fiveElementsToggle.onValueChanged.RemoveListener(FiveToggleSelected);
            _tenElementsToggle.onValueChanged.RemoveListener(TenToggleSelected);
            _fifteenElementsToggle.onValueChanged.RemoveListener(FifteenToggleSelected);
            _controller.ScreenClosed();
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }
        
        public void SetDescription(string description)
        {
            _description.text = description;
        }

        public void SetButtonTitle(string buttonTitle)
        {
            _generateButtonTitle.text = buttonTitle;
        }

        private void FiveToggleSelected(bool isSelected)
        {
            if (isSelected)
            {
                _controller.CountSelected(5);
            }
        }
        private void TenToggleSelected(bool isSelected)
        {
            if (isSelected)
            {
                _controller.CountSelected(10);
            }
        }
        private void FifteenToggleSelected(bool isSelected)
        {
            if (isSelected)
            {
                _controller.CountSelected(15);
            }
        }
    }
}