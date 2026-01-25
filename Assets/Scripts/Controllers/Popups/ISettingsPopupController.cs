using UserInterface.Popups;

namespace DefaultNamespace.Controllers
{
    public interface ISettingsPopupController
    {
        void Setup(ISettingsPopupView settingsPopupView);
        void EnglishToggleChanged(bool isOn);
        void RussianToggleChanged(bool isOn);
    }
}