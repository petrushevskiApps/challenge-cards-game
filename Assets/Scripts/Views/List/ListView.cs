using UnityEngine;

namespace UserInterface.Views
{
    public class ListView : MonoBehaviour, IListView
    {
        [SerializeField]
        private Transform _contentContainer;

        public Transform ContentContainer => _contentContainer;
    }
}
