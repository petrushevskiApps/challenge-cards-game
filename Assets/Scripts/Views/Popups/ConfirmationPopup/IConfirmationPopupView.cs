namespace PetrushevskiApps.WhosGame.Scripts.Views.Popups.ConfirmationPopup
{
    public interface IConfirmationPopupView
    {
        void SetTitle(string title);
        void SetDescription(string description);
        void SetPositiveButtonLabel(string label);
        void SetNegativeButtonLabel(string label);
    }
}