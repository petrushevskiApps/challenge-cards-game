using Zenject;

namespace TwoOneTwoGames.UIManager.InfiniteScrollList
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