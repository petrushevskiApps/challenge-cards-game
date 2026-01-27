using PetrushevskiApps.WhosGame.Scripts.Views.Popups.ConfirmationPopup;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.CustomChallenge;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.RandomChallengePopup;

namespace PetrushevskiApps.WhosGame.Scripts.NavigationCoordinator
{
    public interface IPopupNavigation
    {
        public void ShowConfirmationPopup(ConfirmationPopupNavigationArguments navArguments);
        public void ShowCustomChallengePopup(CustomChallengeNavigationArguments navArguments);
        public void ShowRandomChallengePopup(RandomChallengePopupNavigationArguments navArguments);
        public void ShowSettingsPopup();
        void ShowExitGamePopup();
    }
}