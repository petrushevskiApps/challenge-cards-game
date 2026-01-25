using UserInterface.Screens;

public interface IMainScreenController
{
    void CreatePackageClicked();
    void PlayClicked();
    void SettingsClicked();
    void Setup(IMainScreenView view);
    void ScreenResumed();
    void ScreenHidden();
}