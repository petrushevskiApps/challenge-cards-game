using Localization;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UserInterface.Screens;

public class MainScreenController : IMainScreenController
{
    // Internal
    private IMainScreenView _view;
    
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

        _packageRepository.LoadPackages();
    }

    public void Setup(IMainScreenView view)
    {
        _view = view;
    }

    public void ScreenResumed()
    {
        _localizationService.LanguageChanged += OnLanguageChanged;
        if (_view.ListView != null)
        {
            _packageListController.Setup(_view.ListView, _packageRepository.Packages);
        }
        
        _view.SetMessageVisibility(true);
        _view.SetPlayButton(false);
        SetTexts();
    }

    public void ScreenHidden()
    {
        _packageListController.Clear();
        _localizationService.LanguageChanged -= OnLanguageChanged;
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
}


