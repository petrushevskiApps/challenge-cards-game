using System;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UserInterface.Popups;

namespace DefaultNamespace.Controllers
{
    public class AddEditChallengePopupController : IAddEditChallengePopupController
    {
        private const int DESCRIPTION_MAX_CHARACTERS = 200;

        private IEditChallengePopupView _view;
        private IPackageModel _packageModel;
        private IChallengeCardModel _challengeCardModel;
        private string _challengeDescription;
        
        private readonly INavigationManager _navigationManager;

        public AddEditChallengePopupController(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        
        public void SetView(
            IEditChallengePopupView view, 
            IPackageModel packageModel,
            IChallengeCardModel challengeCardModel = null, 
            string challengeDescriptionText = null)
        {
            _view = view;
            _packageModel = packageModel;
            _challengeCardModel = challengeCardModel;
            _view.SetInputFieldLimit(DESCRIPTION_MAX_CHARACTERS);

            InputTextUpdated(challengeDescriptionText);
            SetButtonText(challengeDescriptionText);
        }

        public void ActionButtonClicked()
        {
            if (_challengeCardModel != null)
            {
                _challengeCardModel.UpdateDescription(_challengeDescription);
                _navigationManager.GoBack();
            }
            else
            {
                _challengeCardModel = new ChallengeCardModel("Who’s most likely to", _challengeDescription);
                _packageModel.AddChallengeCardModel(_challengeCardModel);
                _navigationManager.GoBack();
            }
        }

        public void InputTextUpdated(string challengeDescriptionText)
        {
            _challengeDescription = challengeDescriptionText;
            _view.SetChallengeDescription(_challengeDescription);
            int challengeDescriptionLength = _challengeDescription.Length;

            _view.SetCharacterCountText($"{challengeDescriptionLength} / {DESCRIPTION_MAX_CHARACTERS}");
            _view.SetAddEditButtonInteractivity(!string.IsNullOrWhiteSpace(challengeDescriptionText));
        }

        private void SetButtonText(string descriptionText)
        {
            _view.SetAddEditButtonTitle(string.IsNullOrWhiteSpace(descriptionText)
                ? "Add"
                : "Update");
        }
    }
}