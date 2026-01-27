using System.Collections.Generic;
using PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService;
using PetrushevskiApps.WhosGame.Scripts.Models;
using PetrushevskiApps.WhosGame.Scripts.Views.Screens;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.List.ChallengeCardsList
{
    public interface IChallengesListController
{
    void Setup(
        IChallengeScreenView challengeScreenView, 
        IPackageModel packageModel,
        InfiniteScrollController infiniteScrollController);
    void SetCards(IEnumerable<IChallengeModel> challengeCards);
    void Clear();
    }
}