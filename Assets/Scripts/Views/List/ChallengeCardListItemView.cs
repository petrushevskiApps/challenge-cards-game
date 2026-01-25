using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Views
{
    public class ChallengeCardListItemView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _cardTitle;
        [SerializeField]
        private TextMeshProUGUI _cardDescription;
        [SerializeField]
        private Toggle _selectToggle;
        [SerializeField]
        private ToggleView _toggleView;
        [SerializeField]
        private Button _editButton;
        [SerializeField]
        private Button _deleteButton;

        private ICardItemViewController _controller;

        public void Setup(ICardItemViewController controller)
        {
            _controller = controller;
            
            if (_selectToggle != null)
            {
                _selectToggle.onValueChanged.AddListener(OnToggleValueChanged);
            }
            
            if (_editButton != null)
            {
                _editButton.onClick.AddListener(OnEditClicked);
            }
            
            if (_deleteButton != null)
            {
                _deleteButton.onClick.AddListener(OnDeleteClicked);
            }
        }

        public void Cleanup()
        {
            _controller?.Clear();
            
            if (_selectToggle != null)
            {
                _selectToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
            }
            
            if (_editButton != null)
            {
                _editButton.onClick.RemoveListener(OnEditClicked);
            }
            
            if (_deleteButton != null)
            {
                _deleteButton.onClick.RemoveListener(OnDeleteClicked);
            }
        }

        public void SetTitle(string newTitle)
        {
            if (_cardTitle != null)
            {
                _cardTitle.text = newTitle;
            }
        }

        public void SetDescription(string newDescription)
        {
            if (_cardDescription != null)
            {
                _cardDescription.text = newDescription;
            }
        }

        public void SetSelection(bool isSelected)
        {
            if (_selectToggle != null)
            {
                _selectToggle.SetIsOnWithoutNotify(isSelected);
                _toggleView.UpdateToggleState(isSelected);
            }
        }

        private void OnToggleValueChanged(bool isOn)
        {
            _controller.SelectionToggled(isOn);
        }

        private void OnEditClicked()
        {
            _controller?.ItemClicked();
        }

        private void OnDeleteClicked()
        {
            _controller?.DeleteClicked();
        }

        private void OnDestroy()
        {
            Cleanup();
        }
        
        private void SetData(Transform parent)
        {
            transform.SetParent(parent);
            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
        }
        
        public class Pool : MonoMemoryPool<Transform, ChallengeCardListItemView>
        {
            protected override void Reinitialize(Transform parent, ChallengeCardListItemView view)
            {
                view.SetData(parent);
            }
        }
    }
}
