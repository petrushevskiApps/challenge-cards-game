using System.Threading;
using Cysharp.Threading.Tasks;
using PetrushevskiApps.WhosGame.Scripts.Controllers.List.PackageList;
using PetrushevskiApps.WhosGame.Scripts.LocalizationService;
using PetrushevskiApps.WhosGame.Scripts.Models;
using PetrushevskiApps.WhosGame.Scripts.NavigationCoordinator;
using PetrushevskiApps.WhosGame.Scripts.Repositories.PackageRepositoryService;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.ConfirmationPopup;
using PetrushevskiApps.WhosGame.Scripts.Views.Screens;
using UnityEngine;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.Screens
{
    public class MainScreenController : IMainScreenController
{
    private const int MIN_ACTIVE_CARDS_REQUIRED = 10;

    // Internal
    private IMainScreenView _view;
    private UniTask _initializationTask;
    private CancellationTokenSource _cancellationToken;

    // Injected
    private readonly IScreenNavigation _screenNavigation;
    private readonly IPopupNavigation _popupNavigation;
    private readonly IPackageRepository _packageRepository;
    private readonly IPackageListController _packageListController;
    private readonly ILocalizationService _localizationService;

    public MainScreenController(
        IScreenNavigation screenNavigation,
        IPopupNavigation popupNavigation,
        IPackageRepository packageRepository,
        IPackageListController packageListController,
        ILocalizationService localizationService)
    {
        _screenNavigation = screenNavigation;
        _popupNavigation = popupNavigation;
        _packageRepository = packageRepository;
        _packageListController = packageListController;
        _localizationService = localizationService;
    }
    
    public void Setup(IMainScreenView view)
    {
        _view = view;
    }

    public void ScreenResumed()
    {
        _localizationService.LanguageChanged += OnLanguageChanged;

        _cancellationToken = new CancellationTokenSource();
        UniTask.Create(async () =>
        {
            await UniTask.WaitUntil(
                () => _packageRepository.State == RepositoryState.Loaded, 
                cancellationToken: _cancellationToken.Token);
            if (_view.ListView != null && _packageRepository.State == RepositoryState.Loaded)
            {
                _packageListController.Setup(_view.ListView, _packageRepository.Packages, PackageSelected);
            }
        });
        
        ToggleFooter(false);
        SetTexts();
    }

    public void ScreenHidden()
    {
        _packageListController.Clear();
        _localizationService.LanguageChanged -= OnLanguageChanged;
        
        if (_cancellationToken != null)
        {
            _cancellationToken.Cancel();
            _cancellationToken.Dispose();
            _cancellationToken = null;
        }
    }

    private void SetTexts()
    {
        _view.SetMessage(_localizationService.GetLocalizedString(LocalizationKeys.NeedsMoreCards));
        _view.SetPackageListLabel(_localizationService.GetLocalizedString(LocalizationKeys.PackageList));
        _view.SetCreatePackageButtonLabel(_localizationService.GetLocalizedString(LocalizationKeys.CreatePackage));
        _view.SetPlayButtonLabel(_localizationService.GetLocalizedString(LocalizationKeys.Play));
    }
    private void OnLanguageChanged()
    {
        SetTexts();
    }

    public void CreatePackageClicked()
    {
        var newPackage = _packageRepository.CreatePackage(
            _localizationService.GetLocalizedString(LocalizationKeys.NewPackage));
        _screenNavigation.ShowChallengeScreen(newPackage);
    }

    public void PlayClicked()
    {
        Debug.Log("Play button clicked");
    }

    public void SettingsClicked()
    {
        _popupNavigation.ShowSettingsPopup();
    }

    private void PackageSelected(IPackageModel package, bool isOn)
    {
        if (!isOn && !_view.PackagesToggleGroup.AnyTogglesOn())
        {
            // Package was deselected.
            ToggleFooter(false);
        }
        else if (isOn)
        {
            ToggleFooter(package.GetNumberOfActiveCards() >= MIN_ACTIVE_CARDS_REQUIRED);
        }
    }

    private void ToggleFooter(bool isValidPackageSelected)
    {
        _view.SetMessageGroupVisibility(!isValidPackageSelected);
        _view.SetPlayButton(isValidPackageSelected);
    }
    }
}


