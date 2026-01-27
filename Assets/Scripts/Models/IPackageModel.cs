using System;
using System.Collections.Generic;

namespace PetrushevskiApps.WhosGame.Scripts.Models
{
    public interface IPackageModel
{
    event Action<IChallengeModel> CardAdded;
    event Action<IChallengeModel> CardRemoved;
    event Action<string> TitleChanged;
    event Action CardsNumberChanged;
    event Action<List<IChallengeModel>> CardsAdded;

    string Id { get; }
    string Title { get; }
    
    IReadOnlyList<IChallengeModel> ChallengeCards { get; }
    bool AddChallengeCardModel(IChallengeModel card);
    bool RemoveChallengeCardModel(IChallengeModel card);
    void UpdateTitle(string title);
    int GetNumberOfActiveCards();
    bool AddChallengeModelsInBulk(List<IChallengeModel> cards);
    }
}