using PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService.ViewPool;
using Zenject;

namespace PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService
{
    public static class InfiniteScrollListDiExtensions
    {
        public static void BindInfiniteScrollListDependencies(this DiContainer container)
        {
            container
                .Bind<IItemViewPool>()
                .To<ItemViewPool>()
                .AsTransient();
        }
    }
}