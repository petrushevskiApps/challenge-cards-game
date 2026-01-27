using PetrushevskiApps.WhosGame.Scripts.NavigationService.Navigation;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.ConfirmationPopup;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.CustomChallenge;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.RandomChallengePopup;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.Settings;

namespace PetrushevskiApps.WhosGame.Scripts.NavigationCoordinator
{
    public class PopupNavigation : IPopupNavigation
    {
        private readonly INavigationManager _navigationManager;

        public PopupNavigation(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void ShowConfirmationPopup(ConfirmationPopupNavigationArguments navArguments)
        {
            _navigationManager.ShowPopup<ConfirmationPopup, ConfirmationPopupNavigationArguments>(navArguments);
        }

        public void ShowCustomChallengePopup(CustomChallengeNavigationArguments navArguments)
        {
            _navigationManager.ShowPopup<CustomChallengePopupView, CustomChallengeNavigationArguments>(navArguments);
        }

        public void ShowRandomChallengePopup(RandomChallengePopupNavigationArguments navArguments)
        {
            _navigationManager.ShowPopup<RandomChallengePopup, RandomChallengePopupNavigationArguments>(navArguments);
        }

        public void ShowSettingsPopup()
        {
            _navigationManager.ShowPopup<SettingsPopupView>();
        }

        public void ShowExitGamePopup()
        {
        }
    }
}