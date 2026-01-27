using PetrushevskiApps.WhosGame.Scripts.Views.Screens;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.Screens
{
    public interface IMainScreenController
    {
        void CreatePackageClicked();
        void PlayClicked();
        void SettingsClicked();
        void Setup(IMainScreenView view);
        void ScreenResumed();
        void ScreenHidden();
    }
}