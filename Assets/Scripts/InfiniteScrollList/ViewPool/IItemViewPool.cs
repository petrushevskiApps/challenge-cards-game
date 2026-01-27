using UnityEngine;

namespace PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService.ViewPool
{
    public interface IItemViewPool
    {
        public void SetPrefab(GameObject prefab, int poolSize = 1);
        public IItemView Spawn(Transform parent, bool activateOnSpawn = true);
        public void Despawn(GameObject item);
        public void Clear();
    }
}