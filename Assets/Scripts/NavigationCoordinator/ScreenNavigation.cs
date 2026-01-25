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

        public void ShowChallengeScreen(IPackageModel packageModel)
        {
            _navigationManager.ShowScreen<ChallengeScreen, ChallengeScreenNavigationArguments>(
                new ChallengeScreenNavigationArguments(packageModel));
        }

        public void NavigateBack()
        {
            _navigationManager.GoBack();
        }
    }
}