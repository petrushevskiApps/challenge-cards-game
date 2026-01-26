using System;
using Localization;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UserInterface.Popups;

namespace DefaultNamespace.Controllers
{
    public class AddEditChallengePopupController : IAddEditChallengePopupController
    {
        private const int DESCRIPTION_MAX_CHARACTERS = 200;

        private IEditChallengePopupView _view;
        private Action<string> _onPopupResult;
        private string _challengeDescription;
        private bool _isEdit;
        
        // Injected
        private readonly ILocalizationService _localizationService;
        private readonly INavigationManager _navigationManager;

        public AddEditChallengePopupController(
            ILocalizationService localizationService,
            INavigationManager navigationManager)
        {
            _localizationService = localizationService;
            _navigationManager = navigationManager;
        }
        
        public void Setup(
            IEditChallengePopupView view, 
            Action<string> onPopupResult, 
            string challengeDescriptionText = null)
        {
            _view = view;
            _onPopupResult = onPopupResult;
            
            _view.SetInputFieldLimit(DESCRIPTION_MAX_CHARACTERS);
            _isEdit = challengeDescriptionText != null;
            
            InputTextUpdated(challengeDescriptionText);
            SetButtonText();
            SetTitle();
            SetInputFieldPlaceholder();
        }

        public void PopupShown()
        {
            _localizationService.LanguageChanged += OnLanguageChanged;
        }

        public void PopupClosed()
        {
            _localizationService.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged()
        {
            SetButtonText();
            SetTitle();
            SetInputFieldPlaceholder();
        }

        public void ActionButtonClicked()
        {
            _onPopupResult?.Invoke(_challengeDescription);
            _navigationManager.GoBack();
        }

        public void InputTextUpdated(string challengeDescriptionText)
        {
            _challengeDescription = challengeDescriptionText;
            _view?.SetChallengeDescription(_challengeDescription);
            int challengeDescriptionLength = _challengeDescription.Length;

            _view?.SetCharacterCountText($"{challengeDescriptionLength} / {DESCRIPTION_MAX_CHARACTERS}");
            _view?.SetAddEditButtonInteractivity(!string.IsNullOrWhiteSpace(challengeDescriptionText));
        }

        private void SetButtonText()
        {
            _view?.SetAddEditButtonTitle(_isEdit
                ? _localizationService.GetLocalizedString(LocalizationKeys.Update)
                : _localizationService.GetLocalizedString(LocalizationKeys.Add));
        }

        private void SetInputFieldPlaceholder()
        {
            _view?.SetInputFieldPlaceholder(_localizationService.GetLocalizedString(LocalizationKeys.WriteCustomChallenge));
        }

        private void SetTitle()
        {
            _view?.SetTitle(_localizationService.GetLocalizedString(LocalizationKeys.WhosMostLikely));
        }
    }
}