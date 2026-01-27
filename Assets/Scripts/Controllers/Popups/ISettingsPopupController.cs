using PetrushevskiApps.WhosGame.Scripts.Views.Popups.Settings;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.Popups
{
    public interface ISettingsPopupController
    {
        void Setup(ISettingsPopupView settingsPopupView);
        void EnglishToggleChanged(bool isOn);
        void RussianToggleChanged(bool isOn);
    }
}