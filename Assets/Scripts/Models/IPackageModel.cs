using System;
using System.Collections.Generic;

public interface IPackageModel
{
    IReadOnlyList<IChallengeCardModel> ChallengeCards { get; }
    event Action<IChallengeCardModel> CardAdded;
    event Action<IChallengeCardModel> CardRemoved;
    bool AddChallengeCardModel(IChallengeCardModel card);
    bool RemoveChallengeCardModel(IChallengeCardModel card);
    void UpdateTitle(string title);
}