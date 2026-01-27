using PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService.ViewPool;

namespace PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService
{
    public interface IListDataSource
    {
        void SetItemViewData(IItemView rowView);
    }
}