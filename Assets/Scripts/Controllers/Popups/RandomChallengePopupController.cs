using System;
using Localization;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UserInterface.Popups;

namespace DefaultNamespace.Controllers
{
    public class RandomChallengePopupController: IRandomChallengePopupController
    {
        // Internal
        private int _challengesCount;
        private IRandomChallengePopupView _view;
        private Action<int> _onPopupResult;

        // Injected
        private readonly ILocalizationService _localizationService;
        private readonly INavigationManager _navigationManager;

        public RandomChallengePopupController(
            ILocalizationService localizationService,
            INavigationManager navigationManager)
        {
            _localizationService = localizationService;
            _navigationManager = navigationManager;
        }
        
        public void Setup(IRandomChallengePopupView view, Action<int> onPopupResult)
        {
            _view = view;
            _onPopupResult = onPopupResult;
        }

        public void ScreenShown()
        {
            _localizationService.LanguageChanged += OnLanguageChanged;
            SetLabels();
        }

        public void ScreenClosed()
        {
            _localizationService.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged()
        {
            SetLabels();
        }

        public void GenerateClicked()
        {
            _navigationManager.GoBack();
            _onPopupResult?.Invoke(_challengesCount);
        }

        public void CountSelected(int count)
        {
            _challengesCount = count;
        }

        private void SetLabels()
        {
            _view.SetTitle(_localizationService.GetLocalizedString(LocalizationKeys.RandomChallenge));
            _view.SetDescription(_localizationService.GetLocalizedString(LocalizationKeys.GenerateCountPrompt));
            _view.SetButtonTitle(_localizationService.GetLocalizedString(LocalizationKeys.GenerateNow));
        }
    }
}