using DefaultNamespace;
using DefaultNamespace.Controllers;
using UnityEngine;
using UserInterface.Views;
using Zenject;

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
            .Bind<IPackageListController>()
            .To<PackageListController>()
            .AsSingle();
        Container
            .Bind<IChallengeCardListController>()
            .To<ChallengeCardListController>()
            .AsSingle();
        
        Container
            .BindMemoryPool<ChallengeCardListItemView, ChallengeCardListItemView.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_challengeCardViewPrefab)
            .UnderTransformGroup("CardListItemViewPool");
        Container
            .BindMemoryPool<PackageListItemView, PackageListItemView.Pool>()
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_packageViewPrefab)
            .UnderTransformGroup("PackageListItemViewPool");
    }
}