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
    List<ChallengeCardModel> ChallengeCards { get; }
    bool AddChallengeCardModel(IChallengeCardModel card);
    bool RemoveChallengeCardModel(IChallengeCardModel card);
    void UpdateTitle(string title);
    int GetNumberOfActiveCards();
}