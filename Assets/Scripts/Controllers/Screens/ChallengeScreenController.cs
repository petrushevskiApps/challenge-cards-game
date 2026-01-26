using System;
using System.Collections.Generic;
using System.Linq;
using Localization;
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
    private readonly IPackageRepository _packageRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IScreenNavigation _screenNavigation;
    private readonly IPopupNavigation _popupNavigation;
    private readonly IChallengeCardListController _challengeCardListController;
    private readonly IRandomChallengeRepository _randomChallengeRepository;

    public ChallengeScreenController(
        IPackageRepository packageRepository,
        ILocalizationService localizationService,
        IScreenNavigation screenNavigation,
        IPopupNavigation popupNavigation,
        IChallengeCardListController challengeCardListController,
        IRandomChallengeRepository randomChallengeRepository)
    {
        _packageRepository = packageRepository;
        _localizationService = localizationService;
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
        _localizationService.LanguageChanged += OnLanguageChanged;
        if (_packageModel != null && _challengeCardListController != null)
        {
            _challengeCardListController.Setup(_view.ListView, _packageModel);
            _challengeCardListController.SetCards(_packageModel.ChallengeCards);
            SetTitle();
        }

        SetLabels();
    }

    public void ScreenHidden()
    {
        _localizationService.LanguageChanged -= OnLanguageChanged;
        _challengeCardListController?.Clear();
    }

    private void OnLanguageChanged()
    {
        SetLabels();
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
        _popupNavigation.ShowConfirmationPopup(new ConfirmationPopupNavigationArguments(
            DeletePackage,
            () =>
            {
            },
            LocalizationKeys.ConfirmRemovePackage,
            LocalizationKeys.CannotBeUndone));
    }

    private void DeletePackage()
    {
        _packageRepository.DeletePackage(_packageModel);
        _screenNavigation.NavigateBack();
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
        var challenges = _randomChallengeRepository.GetRandomChallenges(
            challengesCount, 
            _localizationService.GetCurrentLanguage().ToString().ToLower());
        foreach (string challenge in challenges)
        {
            var challengeModel = new ChallengeCardModel("Who’s most likely to", challenge);
            _packageModel.AddChallengeCardModel(challengeModel);
        }
    }

    public void SelectAllCardsToggled(bool isOn)
    {
        foreach (var card in _packageModel.ChallengeCards)
        {
            card.SetSelected(isOn);
        }
        _packageRepository.SavePackages();
    }

    public void SearchInputChanged(string searchText)
    {
        var filteredList = _packageModel.ChallengeCards
            .Where(card =>
                card.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                || card.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        _challengeCardListController?.SetCards(filteredList);
    }

    private void SetLabels()
    {
        _view.SetSelectAllLabel(_localizationService.GetLocalizedString(LocalizationKeys.FilterAll));
        _view.SetSearchInputLabel(_localizationService.GetLocalizedString(LocalizationKeys.Search));
        _view.SetCustomChallengeButtonLabel(_localizationService.GetLocalizedString(LocalizationKeys.CustomChallenge));
        _view.SetRandomChallengeButtonLabel(_localizationService.GetLocalizedString(LocalizationKeys.RandomChallenge));
    }

    private void SetTitle()
    {
        _view.SetPackageTitle(_packageModel.Title);
    }
}