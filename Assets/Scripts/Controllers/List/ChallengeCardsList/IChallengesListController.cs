using System.Collections.Generic;
using TwoOneTwoGames.UIManager.InfiniteScrollList;
using UserInterface.Screens;
using UserInterface.Views;

public interface IChallengesListController
{
    void Setup(
        IChallengeScreenView challengeScreenView, 
        IPackageModel packageModel,
        InfiniteScrollController infiniteScrollController);
    void SetCards(IEnumerable<IChallengeCardModel> challengeCards);
    void Clear();
}