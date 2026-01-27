namespace PetrushevskiApps.WhosGame.Scripts.Views.Popups.Settings
{
    public interface ISettingsPopupView
    {
        void SetEnglishToggleState(bool isOn);
        void SetRussianToggleState(bool isOn);
        void SetTitle(string title);
        void SetEnglishToggleLabel(string label);
        void SetRussianToggleLabel(string label);
    }
}