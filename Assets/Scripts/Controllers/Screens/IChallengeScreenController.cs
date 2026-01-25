using UserInterface.Screens;

public interface IChallengeScreenController
{
    void BackClicked();
    void EditTitleClicked();
    void DeletePackageClicked();
    void CreateCustomChallengeClicked();
    void CreateRandomChallengeClicked();
    void SelectAllCardsToggled(bool isOn);
    void SearchInputChanged(string searchText);
    void Setup(IChallengeScreenView view, IPackageModel argumentsPackageModel);
    void ScreenResumed();
    void ScreenHidden();
}