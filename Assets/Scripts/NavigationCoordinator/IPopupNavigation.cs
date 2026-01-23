namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public interface IPopupNavigation
    {
        public void ShowConfirmationPopup();
        public void ShowEditChallengePopup();
        public void ShowRandomChallengePopup();
        public void ShowSettingsPopup();
        void ShowExitGamePopup();
    }
}