using PetrushevskiApps.WhosGame.Scripts.Controllers.List.ChallengeCardsList;
using PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService.ViewPool;
using PetrushevskiApps.WhosGame.Scripts.Models;
using PetrushevskiApps.WhosGame.Scripts.NavigationCoordinator;
using PetrushevskiApps.WhosGame.Scripts.Repositories.PackageRepositoryService;
using PetrushevskiApps.WhosGame.Scripts.Views;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace PetrushevskiApps.WhosGame.Scripts.Views.List
{
    public class ChallengeItemView : MonoBehaviour, IItemView
    {
        [SerializeField]
        private TextMeshProUGUI _cardTitle;
        [SerializeField]
        private TextMeshProUGUI _cardDescription;
        [SerializeField]
        private ToggleView _selectToggle;
        [FormerlySerializedAs("_toggleView")]
        [SerializeField]
        private SwitchView _switchView;
        [SerializeField]
        private Button _editButton;
        [SerializeField]
        private Button _deleteButton;

        private IChallengeItemViewController _controller;

        private void Awake()
        {
            if (_selectToggle != null)
            {
                _selectToggle.Toggle.onValueChanged.AddListener(OnToggleValueChanged);
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
        
        private void OnDestroy()
        {
            if (_selectToggle != null)
            {
                _selectToggle.Toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
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

        private void OnDisable()
        {
            _controller.ViewHidden();
        }

        public void Setup(
            IPackageRepository packageRepository,
            IChallengeModel challengeModel, 
            IPackageModel packageModel,
            IPopupNavigation popupNavigation)
        {
            if (_controller == null)
            {
                _controller = new ChallengeItemViewController(
                    packageRepository, 
                    this,
                    popupNavigation);
                _controller.Setup(challengeModel, packageModel);
            }
            else
            {
                _controller.Setup(challengeModel, packageModel);
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
                _selectToggle.UpdateToggleState(isSelected);
                _switchView.UpdateToggleState(isSelected);
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

        public int Index { get; set; }
        public GameObject View => gameObject;
    }
}
