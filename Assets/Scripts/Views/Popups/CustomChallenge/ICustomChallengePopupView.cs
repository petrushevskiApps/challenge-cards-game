namespace PetrushevskiApps.WhosGame.Scripts.Views.Popups.CustomChallenge
{
    public interface ICustomChallengePopupView
    {
        void SetAddEditButtonTitle(string buttonTitle);
        void SetChallengeDescription(string description);
        void SetCharacterCountText(string characterCounterText);
        void SetAddEditButtonInteractivity(bool isCharacterCountOverLimit);
        void SetInputFieldLimit(int limit);
        void SetInputFieldPlaceholder(string placeholder);
        void SetTitle(string title);
    }
}