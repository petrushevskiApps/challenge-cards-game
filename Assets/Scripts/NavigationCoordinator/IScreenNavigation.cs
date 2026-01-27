using PetrushevskiApps.WhosGame.Scripts.Models;

namespace PetrushevskiApps.WhosGame.Scripts.NavigationCoordinator
{
    public interface IScreenNavigation
    {
        public void ShowMainScreen();
        public void ShowChallengeScreen(IPackageModel packageModel);
        void NavigateBack();
    }
}