using UnityEngine;

namespace PetrushevskiApps.WhosGame.Scripts.Views.List
{
    public class ListView : MonoBehaviour, IListView
    {
        [SerializeField]
        private Transform _contentContainer;

        public Transform ContentContainer => _contentContainer;
    }
}
