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

    public MainScreenController(
        IScreenNavigation screenNavigation,
        IPopupNavigation popupNavigation,
        IPackageRepository packageRepository,
        IPackageListController packageListController)
    {
        _screenNavigation = screenNavigation;
        _popupNavigation = popupNavigation;
        _packageRepository = packageRepository;
        _packageListController = packageListController;

        _packageRepository.LoadPackages();
    }

    public void Setup(IMainScreenView view)
    {
        _view = view;
    }

    public void ScreenResumed()
    {
        _view.SetMessage(true, "Needs more than 10 cards to start... or select a different package to play!");
        _view.SetPlayButton(false);
        
        if (_view.ListView != null)
        {
            _packageListController.Setup(_view.ListView, _packageRepository.Packages);
        }
    }

    public void ScreenHidden()
    {
        _packageListController.Clear();
    }

    public void CreatePackageClicked()
    {
        var newPackage = _packageRepository.CreatePackage("New Package");
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


