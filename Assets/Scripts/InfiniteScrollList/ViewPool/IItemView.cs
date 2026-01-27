using UnityEngine;

namespace TwoOneTwoGames.UIManager.InfiniteScrollList
{
    public interface IItemView
    {
        public int Index { get; set; }
        public GameObject View { get; }
    }
}