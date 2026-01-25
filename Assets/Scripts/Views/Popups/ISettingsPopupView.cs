namespace UserInterface.Popups
{
    public interface ISettingsPopupView
    {
        void SetEnglishToggleState(bool isOn);
        void SetRussianToggleState(bool isOn);
        void SetTitle(string title);
    }
}