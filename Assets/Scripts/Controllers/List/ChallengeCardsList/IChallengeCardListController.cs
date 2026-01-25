using System.Collections.Generic;
using UserInterface.Views;

public interface IChallengeCardListController
{
    void Setup(IListView listView, IPackageModel packageModel);
    void Clear();
    void SetCards(List<ChallengeCardModel> challengeCards);
}