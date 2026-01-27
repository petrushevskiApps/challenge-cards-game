using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TwoOneTwoGames.UIManager.InfiniteScrollList
{
    public class ItemViewPool : IItemViewPool, IDisposable
    {
        private readonly List<GameObject> _assignedObjects = new();
        private readonly Queue<GameObject> _pooledObjects = new();

        private GameObject _prefab;

        public void Dispose()
        {
            Clear();
            _pooledObjects.Clear();
        }

        public void SetPrefab(GameObject prefab, int poolSize = 1)
        {
            _prefab = prefab;
            Create(poolSize);
        }

        public IItemView Spawn(Transform parent, bool activateOnSpawn = true)
        {
            if (_pooledObjects.TryDequeue(out var result))
            {
                result.transform.SetParent(parent, false);
                result.SetActive(activateOnSpawn);
                _assignedObjects.Add(result);
                return result.GetComponent<IItemView>();
            }

            Create();
            return Spawn(parent, activateOnSpawn);
        }

        public void Despawn(GameObject item)
        {
            DespawnWithoutListModification(item);
            _assignedObjects.Remove(item);
        }

        public void Clear()
        {
            _assignedObjects.ForEach(DespawnWithoutListModification);
            _assignedObjects.Clear();
        }

        private void Create(int count = 1)
        {
            for (var i = 0; i < count; i++)
            {
                _pooledObjects.Enqueue(Object.Instantiate(_prefab));
            }
        }

        private void DespawnWithoutListModification(GameObject item)
        {
            item.GetComponent<ISpawnable>()?.OnDespawnInitiated();
            item.SetActive(false);
            item.transform.SetParent(null, false);
            _pooledObjects.Enqueue(item);
        }
    }
}