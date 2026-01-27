using TwoOneTwoGames.UIManager.InfiniteScrollList;

namespace UserInterface.Screens
{
    public interface IChallengeScreenView
    {
        InfiniteScrollController InfiniteListScrollController { get; }
        void SetSelectAllLabel(string label);
        void SetSearchInputLabel(string label);
        void SetCustomChallengeButtonLabel(string label);
        void SetRandomChallengeButtonLabel(string label);
        void SetPackageTitle(string title);
        void ScrollToBottom();
        void ScrollTo(int elementIndex);
    }
}
