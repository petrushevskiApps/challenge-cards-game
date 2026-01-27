using UnityEngine;
using UserInterface.Screens;

public interface IChallengeScreenController
{
    void Setup(IChallengeScreenView view, IPackageModel argumentsPackageModel);
    void ScreenShown();
    void ScreenHidden();
    void BackClicked();
    void EditTitleClicked();
    void DeletePackageClicked();
    void CreateCustomChallengeClicked();
    void CreateRandomChallengeClicked();
    void SelectAllCardsToggled(bool isOn);
    void SearchInputChanged(string searchText, MonoBehaviour view);
}