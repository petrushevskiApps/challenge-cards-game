using PetrushevskiApps.WhosGame.Scripts.NavigationCoordinator;
using PetrushevskiApps.WhosGame.Scripts.NavigationService.Navigation;
using PetrushevskiApps.WhosGame.Scripts.NavigationService.Window;
using PetrushevskiApps.WhosGame.Scripts.NavigationService.WindowProvider;
using Zenject;

namespace PetrushevskiApps.WhosGame.Scripts.NavigationService
{
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
}