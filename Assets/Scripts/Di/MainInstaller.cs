using DefaultNamespace;
using Zenject;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesTo<GameManager>()
            .FromComponentInHierarchy()
            .AsSingle();
        
        Container
            .Bind<IMainScreenController>()
            .To<MainScreenController>()
            .AsSingle();
    }
}