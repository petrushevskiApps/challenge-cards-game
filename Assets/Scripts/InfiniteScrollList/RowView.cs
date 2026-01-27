using System.Collections.Generic;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.InfiniteScrollList
{
    public class RowView : MonoBehaviour, IItemView, ISpawnable
    {
        private readonly List<GameObject> _rowElements = new();
        private IItemViewPool _itemViewPool;
        public int Index { get; set; }
        public GameObject View => gameObject;

        public void OnDespawnInitiated()
        {
            _rowElements.ForEach(rowElement => _itemViewPool.Despawn(rowElement));
            _rowElements.Clear();
        }

        public void SetupRow(
            IItemViewPool itemViewPool,
            int rowElementsCount)
        {
            _itemViewPool = itemViewPool;

            for (var i = 0; i < rowElementsCount; i++)
            {
                _rowElements.Add(_itemViewPool.Spawn(gameObject.transform, false).View);
            }
        }

        public List<GameObject> GetRowViews()
        {
            return _rowElements;
        }
    }
}