using System;
using System.Collections.Generic;

public interface IPackageModel
{
    event Action<IChallengeCardModel> CardAdded;
    event Action<IChallengeCardModel> CardRemoved;
    event Action<string> TitleChanged;
    event Action CardsNumberChanged;
    
    string Id { get; }
    string Title { get; }
    IReadOnlyList<IChallengeCardModel> ChallengeCards { get; }
    bool AddChallengeCardModel(IChallengeCardModel card);
    bool RemoveChallengeCardModel(IChallengeCardModel card);
    void UpdateTitle(string title);
    int GetNumberOfActiveCards();
    event Action<List<IChallengeCardModel>> CardsAdded;
    bool AddChallengeModelsInBulk(List<IChallengeCardModel> cards);
}