using UserInterface.Popups;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public interface IPopupNavigation
    {
        public void ShowConfirmationPopup();
        public void ShowEditChallengePopup(EditChallengeNavigationArguments navArguments);
        public void ShowRandomChallengePopup(RandomChallengePopupNavigationArguments navArguments);
        public void ShowSettingsPopup();
        void ShowExitGamePopup();
    }
}