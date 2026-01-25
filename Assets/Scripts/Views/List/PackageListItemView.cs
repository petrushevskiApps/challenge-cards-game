using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Views
{
    public class PackageListItemView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _packageTitle;
        [SerializeField]
        private TextMeshProUGUI _packageInfo;
        [SerializeField]
        private Toggle _selectToggle;
        [SerializeField]
        private Button _clickButton;

        private IPackageItemViewController _controller;

        public void Setup(IPackageItemViewController controller)
        {
            _controller = controller;
            
            if (_clickButton != null)
            {
                _clickButton.onClick.AddListener(OnItemClicked);
            }
        }

        public void Cleanup()
        {
            _controller.Clear();
            if (_clickButton != null)
            {
                _clickButton.onClick.RemoveListener(OnItemClicked);
            }
        }

        public void SetTitle(string packageTitle)
        {
            if (_packageTitle != null)
            {
                _packageTitle.text = packageTitle;
            }
        }

        public void SetPackageInfo(string packageInfo)
        {
            if (_packageInfo != null)
            {
                _packageInfo.text = packageInfo;
            }
        }
        
        private void OnItemClicked()
        {
            _controller?.ItemClicked();
        }
        
        private void SetData(Transform parent)
        {
            transform.SetParent(parent);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
        }
        
        public class Pool : MonoMemoryPool<Transform, PackageListItemView>
        {
            protected override void Reinitialize(Transform parent, PackageListItemView view)
            {
                view.SetData(parent);
            }
        }
    }
}
