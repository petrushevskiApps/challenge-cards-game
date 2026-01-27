using System;
using PetrushevskiApps.WhosGame.Scripts.LocalizationService;
using PetrushevskiApps.WhosGame.Scripts.NavigationService.Navigation;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.ConfirmationPopup;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.Popups
{
    public class ConfirmationPopupController: IConfirmationPopupController
    {
        private IConfirmationPopupView _view;
        private Action _onPositivePopupResult;
        private Action _onNegativePopupResult;
        private string _titleLocalizationKey;
        private string _messageLocalizationKey;
        
        // Injected
        private readonly ILocalizationService _localizationService;
        private readonly INavigationManager _navigationManager;

        public ConfirmationPopupController(
            ILocalizationService localizationService,
            INavigationManager navigationManager)
        {
            _localizationService = localizationService;
            _navigationManager = navigationManager;
        }
        
        public void Setup(
            IConfirmationPopupView confirmationPopup, 
            Action onPositivePopupResult, 
            Action onNegativePopupResult,
            string titleLocalizationKey, 
            string messageLocalizationKey)
        {
            _view = confirmationPopup;
            _onPositivePopupResult = onPositivePopupResult;
            _onNegativePopupResult = onNegativePopupResult;
            _titleLocalizationKey = titleLocalizationKey;
            _messageLocalizationKey = messageLocalizationKey;
        }

        public void ScreenShown()
        {
            _localizationService.LanguageChanged += OnLanguageChanged;
            SetLabels();
        }

        public void ScreenClosed()
        {
            _localizationService.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged()
        {
            SetLabels();
        }

        public void PositiveButtonClicked()
        {
            _navigationManager.GoBack();
            _onPositivePopupResult?.Invoke();
        }

        public void NegativeButtonClicked()
        {
            _navigationManager.GoBack();
            _onNegativePopupResult?.Invoke();
        }

        private void SetLabels()
        {
            _view.SetTitle(_localizationService.GetLocalizedString(_titleLocalizationKey));
            _view.SetDescription(_localizationService.GetLocalizedString(_messageLocalizationKey));
            _view.SetPositiveButtonLabel(_localizationService.GetLocalizedString(LocalizationKeys.Yes));
            _view.SetNegativeButtonLabel(_localizationService.GetLocalizedString(LocalizationKeys.No));
        }
    }
}