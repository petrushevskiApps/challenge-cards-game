using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Localization;
using TwoOneTwoGames.UIManager.InfiniteScrollList;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UserInterface.Popups;
using UserInterface.Screens;

public class ChallengeScreenController : IChallengeScreenController
{
    private const float DEBOUNCE_PERIOD = 0.3f;

    // Internal
    private IChallengeScreenView _view;
    private IPackageModel _packageModel;
    private Coroutine _debounceCoroutine;

    // Injected
    private readonly IPackageRepository _packageRepository;
    private readonly ILocalizationService _localizationService;
    private readonly IScreenNavigation _screenNavigation;
    private readonly IPopupNavigation _popupNavigation;
    private readonly IChallengesListController _challengesListController;
    private readonly IRandomChallengeRepository _randomChallengeRepository;

    public ChallengeScreenController(
        IPackageRepository packageRepository,
        ILocalizationService localizationService,
        IScreenNavigation screenNavigation,
        IPopupNavigation popupNavigation,
        IChallengesListController challengesListController,
        IRandomChallengeRepository randomChallengeRepository)
    {
        _packageRepository = packageRepository;
        _localizationService = localizationService;
        _screenNavigation = screenNavigation;
        _popupNavigation = popupNavigation;
        _challengesListController = challengesListController;
        _randomChallengeRepository = randomChallengeRepository;
    }

    public void Setup(IChallengeScreenView view, IPackageModel packageModel)
    {
        _view = view;
        _packageModel = packageModel;
    }

    public void ScreenShown()
    {
        _localizationService.LanguageChanged += OnLanguageChanged;
        if (_packageModel != null && _challengesListController != null)
        {
            _challengesListController.Setup(_view, _packageModel, _view.InfiniteListScrollController);
            SetTitle();
        }

        SetLabels();
    }

    public void ScreenHidden()
    {
        _localizationService.LanguageChanged -= OnLanguageChanged;
        _challengesListController?.Clear();
    }

    private void OnLanguageChanged()
    {
        SetLabels();
    }

    public void BackClicked()
    {
        _screenNavigation.NavigateBack();
    }

    public void DeletePackageClicked()
    {
        _popupNavigation.ShowConfirmationPopup(new ConfirmationPopupNavigationArguments(
            () =>
            {
                _packageRepository.DeletePackage(_packageModel);
                _screenNavigation.NavigateBack();
            },
            () => { },
            LocalizationKeys.ConfirmRemovePackage,
            LocalizationKeys.CannotBeUndone));
    }

    public void CreateCustomChallengeClicked()
    {
        _popupNavigation.ShowEditChallengePopup(
            new EditChallengeNavigationArguments(OnCreateCustomChallengePopupResult));
    }

    public void CreateRandomChallengeClicked()
    {
        _popupNavigation.ShowRandomChallengePopup(
            new RandomChallengePopupNavigationArguments(OnRandomChallengePopupResult));
    }

    private void OnCreateCustomChallengePopupResult(string challengeDescription)
    {
        var challengeCardModel = new ChallengeCardModel(
            _localizationService.GetLocalizedString(LocalizationKeys.WhosMostLikely), 
            challengeDescription);
        _packageModel.AddChallengeCardModel(challengeCardModel);
    }
    
    private void OnRandomChallengePopupResult(int challengesCount)
    {
        var randomChallenges = _randomChallengeRepository.GetRandomChallenges(
            challengesCount, 
            _localizationService.GetCurrentLanguage().ToString().ToLower());
        
        List<IChallengeCardModel> challengeModels = randomChallenges
            .Select(challenge => new ChallengeCardModel(
                _localizationService.GetLocalizedString(LocalizationKeys.WhosMostLikely), 
                challenge))
            .Cast<IChallengeCardModel>()
            .ToList();

        _packageModel.AddChallengeModelsInBulk(challengeModels);
    }

    public void SelectAllCardsToggled(bool isOn)
    {
        foreach (var card in _packageModel.ChallengeCards)
        {
            card.SetSelected(isOn);
        }
        _packageRepository.SavePackagesAsync().Forget();
    }

    private CancellationTokenSource _cancellationTokenSource;
    
    public void PackageTitleChanged(string newTitle)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
        _cancellationTokenSource = new CancellationTokenSource();

       DebounceTitleUpdateAsync(newTitle, _cancellationTokenSource.Token).Forget();
    }

    private async UniTaskVoid DebounceTitleUpdateAsync(string title, CancellationToken token)
    {
        try
        {
            await UniTask.Delay(TimeSpan.FromSeconds(DEBOUNCE_PERIOD), cancellationToken: token);

            string trimmed = title?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(trimmed))
                return;

            _packageModel.UpdateTitle(trimmed);
        }
        catch (OperationCanceledException)
        {
            // Ignore
        }
    }
    
    public void SearchInputChanged(string searchText, MonoBehaviour view)
    {
        if (!view.gameObject.activeInHierarchy)
        {
            return;
        }

        if (_debounceCoroutine != null)
        {
            view.StopCoroutine(_debounceCoroutine);
        }

        _debounceCoroutine = view.StartCoroutine(DebounceRoutine(searchText));
    }

    private IEnumerator DebounceRoutine(string text)
    {
        yield return new WaitForSeconds(DEBOUNCE_PERIOD);
        _debounceCoroutine = null;
        SearchCards(text);
    }
    
    private void SearchCards(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            _challengesListController?.SetCards(_packageModel.ChallengeCards);
            return;
        }
        
        searchText = searchText.Trim();

        var results = new List<IChallengeCardModel>();

        foreach (var card in _packageModel.ChallengeCards)
        {
            var title = card.Title;
            var description = card.Description;

            if ((!string.IsNullOrEmpty(title) &&
                 title.Contains(searchText, StringComparison.OrdinalIgnoreCase)) ||

                (!string.IsNullOrEmpty(description) &&
                 description.Contains(searchText, StringComparison.OrdinalIgnoreCase)))
            {
                results.Add(card);
            }
        }

        _challengesListController?.SetCards(results);
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