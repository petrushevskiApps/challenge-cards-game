using PetrushevskiApps.WhosGame.Scripts;
using PetrushevskiApps.WhosGame.Scripts.Controllers.List.ChallengeCardsList;
using PetrushevskiApps.WhosGame.Scripts.Controllers.List.PackageList;
using PetrushevskiApps.WhosGame.Scripts.Controllers.Popups;
using PetrushevskiApps.WhosGame.Scripts.Controllers.Screens;
using PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService.ViewPool;
using PetrushevskiApps.WhosGame.Scripts.NavigationCoordinator;
using PetrushevskiApps.WhosGame.Scripts.Repositories.ChallengeRepositoryService;
using PetrushevskiApps.WhosGame.Scripts.Repositories.PackageRepositoryService;
using PetrushevskiApps.WhosGame.Scripts.Views.List;
using UnityEngine;
using Zenject;
using LocalizationServiceComponent = PetrushevskiApps.WhosGame.Scripts.LocalizationService.LocalizationService;

namespace PetrushevskiApps.WhosGame.Scripts.Di
{
    public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject _challengeCardViewPrefab;
    [SerializeField]
    private GameObject _packageViewPrefab;
    
    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<GameManager>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container
            .Bind<IPackageRepository>()
            .To<PackageRepository>()
            .AsSingle();
        
        Container
            .Bind<IRandomChallengeRepository>()
            .To<RandomChallengeRepository>()
            .AsSingle();
        
        Container
            .Bind<IMainScreenController>()
            .To<MainScreenController>()
            .AsSingle();
        Container
            .Bind<IChallengeScreenController>()
            .To<ChallengeScreenController>()
            .AsSingle();
        Container
            .Bind<IAddEditChallengePopupController>()
            .To<AddEditChallengePopupController>()
            .AsSingle();
        Container
            .Bind<IRandomChallengePopupController>()
            .To<RandomChallengePopupController>()
            .AsSingle();
        Container
            .Bind<ISettingsPopupController>()
            .To<SettingsPopupController>()
            .AsSingle();
        Container
            .Bind<IConfirmationPopupController>()
            .To<ConfirmationPopupController>()
            .AsSingle();
        Container
            .Bind<IPackageListController>()
            .To<PackageListController>()
            .AsSingle();
        Container
            .Bind<IChallengesListController>()
            .To<ChallengesListController>()
            .AsSingle();
        Container
            .Bind<PetrushevskiApps.WhosGame.Scripts.LocalizationService.ILocalizationService>()
            .To<LocalizationServiceComponent>()
            .AsSingle();
        Container
            .Bind<IItemViewPool>()
            .WithId("ChallengeItemViewPool")
            .To<ItemViewPool>()
            .AsTransient();
        
        Container
            .BindMemoryPool<PackageListItemView, PackageListItemView.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_packageViewPrefab)
            .UnderTransformGroup("PackageListItemViewPool");
    }
    }
}