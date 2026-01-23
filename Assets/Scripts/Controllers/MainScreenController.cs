using System.Collections;
using System.Collections.Generic;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UserInterface.Screens;

public interface IMainScreenController
{
    void CreatePackageClicked();
    void PlayClicked();
    void SettingsClicked();
    void SetView(IMainScreenView view);
}
public class MainScreenController : IMainScreenController
{
    // Internal

    // Injected
    private IMainScreenView _view;
    private readonly IScreenNavigation _screenNavigation;
    private readonly IPopupNavigation _popupNavigation;

    public MainScreenController(
        IScreenNavigation screenNavigation,
        IPopupNavigation popupNavigation)
    {
        _screenNavigation = screenNavigation;
        _popupNavigation = popupNavigation;
    }

    public void SetView(IMainScreenView view)
    {
        _view = view;
        SetupView();
    }

    private void SetupView()
    {
        _view.SetMessage(true, "Needs more than 10 cards to start... or select a different package to play!");
        _view.SetPlayButton(false);
    }

    public void CreatePackageClicked()
    {
        _screenNavigation.ShowChallengeScreen();
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


