using System.Collections.Generic;
using TwoOneTwoGames.UIManager.InfiniteScrollList;
using UserInterface.Views;

public interface IChallengesListController
{
    void Setup(IPackageModel packageModel, InfiniteScrollController infiniteScrollController);
    void SetCards(IEnumerable<IChallengeCardModel> challengeCards);
    void Clear();
}