using TwoOneTwoGames.UIManager.ScreenNavigation;
using Zenject;

public class NavigationServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<NavigationManager>()
            .AsSingle();
        Container
            .Bind<IWindowProvider>()
            .WithId("Screen")
            .To<ScreenProvider>()
            .FromComponentInHierarchy()
            .AsSingle();
        Container
            .Bind<IWindowProvider>()
            .WithId("Popup")
            .To<PopupProvider>()
            .FromComponentInHierarchy()
            .AsSingle();
        Container
            .Bind<IScreenNavigation>()
            .To<ScreenNavigation>()
            .AsSingle();
        Container
            .Bind<IPopupNavigation>()
            .To<PopupNavigation>()
            .AsSingle();
    }
}