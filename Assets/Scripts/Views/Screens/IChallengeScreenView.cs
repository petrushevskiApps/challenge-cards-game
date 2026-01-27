using InfiniteScrollControllerComponent = PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService.InfiniteScrollController;

namespace PetrushevskiApps.WhosGame.Scripts.Views.Screens
{
    public interface IChallengeScreenView
    {
        InfiniteScrollControllerComponent InfiniteListScrollController { get; }
        void SetSelectAllLabel(string label);
        void SetSearchInputLabel(string label);
        void SetCustomChallengeButtonLabel(string label);
        void SetRandomChallengeButtonLabel(string label);
        void SetPackageTitle(string title);
        void ScrollToBottom();
        void ScrollTo(int elementIndex);
    }
}
