using DefaultNamespace.Controllers;
using TMPro;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Popups
{
    public class ConfirmationPopup: UIPopup, IConfirmationPopupView
    {
        [SerializeField]
        private TextMeshProUGUI _title;
        [SerializeField]
        private TextMeshProUGUI _description;
        [SerializeField]
        private Button _positiveButton;
        [SerializeField]
        private TextMeshProUGUI _positiveButtonLabel;
        [SerializeField]
        private Button _negativeButton;
        [SerializeField]
        private TextMeshProUGUI _negativeButtonLabel;
        
        // Injected
        private IConfirmationPopupController _controller;

        [Inject]
        public void Initialize(IConfirmationPopupController controller)
        {
            _controller = controller;
        }
        
        public override void Show<TArguments>(TArguments navArguments)
        {
            base.Show(navArguments);

            if (navArguments is ConfirmationPopupNavigationArguments args)
            {
                _controller.Setup(
                    this, 
                    args.OnPositivePopupResult, 
                    args.OnNegativePopupResult,
                    args.TitleLocalizationKey,
                    args.MessageLocalizationKey);
            }
            _positiveButton.onClick.AddListener(_controller.PositiveButtonClicked);
            _negativeButton.onClick.AddListener(_controller.NegativeButtonClicked);
            _controller.ScreenShown();
        }

        public override void Close()
        {
            base.Close();
            
            _positiveButton.onClick.RemoveListener(_controller.PositiveButtonClicked);
            _negativeButton.onClick.RemoveListener(_controller.NegativeButtonClicked);
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

        public void SetPositiveButtonLabel(string label)
        {
            _positiveButtonLabel.text = label;
        }

        public void SetNegativeButtonLabel(string label)
        {
            _negativeButtonLabel.text = label;
        }
    }
}