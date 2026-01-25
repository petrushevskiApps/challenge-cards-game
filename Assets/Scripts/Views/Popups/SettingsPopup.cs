using DefaultNamespace.Controllers;
using DefaultNamespace.Views;
using Localization;
using TMPro;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserInterface.Popups
{
    public class SettingsPopupView: UIPopup, ISettingsPopupView
    {
        [SerializeField]
        private TextMeshProUGUI _title;
        
        [SerializeField]
        private ToggleView _englishToggle;
        
        [SerializeField]
        private ToggleView _russianToggle;
        
        // Injected
        private ISettingsPopupController _controller;

        [Inject]
        public void Initialize(
            ISettingsPopupController controller,
            ILocalizationService localizationService)
        {
            _controller = controller;
        }
        
        public override void Show<TArguments>(TArguments navArguments)
        {
            base.Show(navArguments);

            _englishToggle.Toggle.onValueChanged.AddListener(EnglishToggleValueChanged);
            _russianToggle.Toggle.onValueChanged.AddListener(RussianToggleValueChanged);
            _controller.Setup(this);
        }

        public override void Close()
        {
            base.Close();
            
            _englishToggle.Toggle.onValueChanged.RemoveListener(EnglishToggleValueChanged);
            _russianToggle.Toggle.onValueChanged.RemoveListener(RussianToggleValueChanged);
        }

        public void SetEnglishToggleState(bool isOn)
        {
            _englishToggle.UpdateToggleState(isOn);
        }
        public void SetRussianToggleState(bool isOn)
        {
            _russianToggle.UpdateToggleState(isOn);
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }
        
        private void EnglishToggleValueChanged(bool isOn)
        {
            _controller.EnglishToggleChanged(isOn);
        }
        
        private void RussianToggleValueChanged(bool isOn)
        {
            _controller.RussianToggleChanged(isOn);
        }
    }
}