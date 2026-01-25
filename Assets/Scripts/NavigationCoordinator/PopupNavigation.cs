using UserInterface.Popups;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class PopupNavigation : IPopupNavigation
    {
        private readonly INavigationManager _navigationManager;

        public PopupNavigation(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void ShowConfirmationPopup()
        {
            _navigationManager.ShowPopup<ConfirmationPopup>();
        }

        public void ShowEditChallengePopup(EditChallengeNavigationArguments navArguments)
        {
            _navigationManager.ShowPopup<AddEditChallengePopupView, EditChallengeNavigationArguments>(navArguments);
        }

        public void ShowRandomChallengePopup()
        {
            _navigationManager.ShowPopup<RandomChallengePopup>();
        }

        public void ShowSettingsPopup()
        {
            _navigationManager.ShowPopup<SettingsPopup>();
        }

        public void ShowExitGamePopup()
        {
        }
    }
}