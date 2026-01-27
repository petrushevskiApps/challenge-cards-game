using UnityEngine;

namespace PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService.ViewPool
{
    public interface IItemView
    {
        public int Index { get; set; }
        public GameObject View { get; }
    }
}