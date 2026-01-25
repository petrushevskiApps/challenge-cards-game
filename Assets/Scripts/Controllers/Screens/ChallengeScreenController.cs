using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UserInterface.Popups;
using UserInterface.Screens;

public class ChallengeScreenController : IChallengeScreenController
{
    // Internal
    private IChallengeScreenView _view;
    private IPackageModel _packageModel;
    
    // Injected
    private readonly IScreenNavigation _screenNavigation;
    private readonly IPopupNavigation _popupNavigation;
    private readonly IChallengeCardListController _challengeCardListController;
    private readonly IRandomChallengeRepository _randomChallengeRepository;

    public ChallengeScreenController(
        IScreenNavigation screenNavigation,
        IPopupNavigation popupNavigation,
        IChallengeCardListController challengeCardListController,
        IRandomChallengeRepository randomChallengeRepository)
    {
        _screenNavigation = screenNavigation;
        _popupNavigation = popupNavigation;
        _challengeCardListController = challengeCardListController;
        _randomChallengeRepository = randomChallengeRepository;
    }

    public void Setup(IChallengeScreenView view, IPackageModel packageModel)
    {
        _view = view;
        _packageModel = packageModel;
    }

    public void ScreenResumed()
    {
        if (_packageModel != null)
        {
            _challengeCardListController?.Setup(_view.ListView, _packageModel);
        }
    }

    public void ScreenHidden()
    {
        _challengeCardListController?.Clear();
    }

    public void BackClicked()
    {
        _screenNavigation.NavigateBack();
    }

    public void EditTitleClicked()
    {
        
    }

    public void DeletePackageClicked()
    {
        _popupNavigation.ShowConfirmationPopup();
    }

    public void CreateCustomChallengeClicked()
    {
        _popupNavigation.ShowEditChallengePopup(new EditChallengeNavigationArguments(_packageModel));
    }

    public void CreateRandomChallengeClicked()
    {
        _popupNavigation.ShowRandomChallengePopup(new RandomChallengePopupNavigationArguments(OnRandomChallengeResult));
    }

    private void OnRandomChallengeResult(int challengesCount)
    {
        var challenges = _randomChallengeRepository.GetRandomChallenges(challengesCount, "english");
        foreach (string challenge in challenges)
        {
            var challengeModel = new ChallengeCardModel("Who’s most likely to", challenge);
            _packageModel.AddChallengeCardModel(challengeModel);
        }
    }

    public void SelectAllCardsToggled(bool isOn)
    {
        _packageModel.ChallengeCards.ForEach(card => card.SetSelected(isOn));
    }

    public void SearchInputChanged(string searchText)
    {
        Debug.Log($"Search input changed: {searchText}");
    }
}