using UserInterface.Screens;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class ScreenNavigation : IScreenNavigation
    {
        private readonly INavigationManager _navigationManager;

        public ScreenNavigation(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void ShowMainScreen()
        {
            _navigationManager.ClearAllStackScreens();
            _navigationManager.ShowScreen<MainScreen>();
        }

        public void ShowChallengeScreen()
        {
            _navigationManager.ShowScreen<ChallengeScreen>();
        }

        public void NavigateBack()
        {
            _navigationManager.GoBack();
        }
    }
}