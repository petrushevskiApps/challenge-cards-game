using DefaultNamespace.Controllers;
using TMPro;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Popups
{
    public class AddEditChallengePopupView: UIPopup, IEditChallengePopupView
    {
        [SerializeField]
        private Button _actionButton;

        [SerializeField]
        private TextMeshProUGUI _actionButtonTitle;

        [SerializeField]
        private TMP_InputField _inputField;

        [SerializeField]
        private TextMeshProUGUI _characterCount;
        
        // Injected
        private IAddEditChallengePopupController _controller;

        [Inject]
        public void Initialize(IAddEditChallengePopupController controller)
        {
            _controller = controller;
        }
        
        public override void Show<TArguments>(TArguments navArguments)
        {
            base.Show(navArguments);
            _actionButton.onClick.AddListener(_controller.ActionButtonClicked);
            _inputField.onValueChanged.AddListener(_controller.InputTextUpdated);
            if (navArguments is EditChallengeNavigationArguments args)
            {
                _controller.SetView(this, args.PackageModel,args.ChallengeCardModel, args.ChallengeDescription);
            }
        }

        public override void Close()
        {
            base.Close();
            _actionButton.onClick.RemoveListener(_controller.ActionButtonClicked);
            _inputField.onValueChanged.RemoveListener(_controller.InputTextUpdated);
        }


        public void SetChallengeDescription(string description)
        {
            _inputField.text = description;
        }

        public void SetInputFieldLimit(int limit)
        {
            _inputField.characterLimit = limit;
        }
        
        public void SetCharacterCountText(string characterCounterText)
        {
            _characterCount.text = characterCounterText;
        }

        public void SetAddEditButtonInteractivity(bool isInteractable)
        {
            _actionButton.interactable = isInteractable;
        }

        public void SetAddEditButtonTitle(string buttonTitle)
        {
            _actionButtonTitle.text = buttonTitle;
        }
    }
}