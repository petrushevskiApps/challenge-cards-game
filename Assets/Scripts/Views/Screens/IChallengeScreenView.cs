using TwoOneTwoGames.UIManager.InfiniteScrollList;
using UnityEngine;
using UserInterface.Views;

namespace UserInterface.Screens
{
    public interface IChallengeScreenView
    {
        IListView ListView { get; }
        InfiniteScrollController InfiniteListScrollController { get; }
        void SetSelectAllLabel(string label);
        void SetSearchInputLabel(string label);
        void SetCustomChallengeButtonLabel(string label);
        void SetRandomChallengeButtonLabel(string label);
        void SetPackageTitle(string title);
    }
}
