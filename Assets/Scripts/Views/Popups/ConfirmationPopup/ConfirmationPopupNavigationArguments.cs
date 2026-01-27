using System;

namespace PetrushevskiApps.WhosGame.Scripts.Views.Popups.ConfirmationPopup
{
    public class ConfirmationPopupNavigationArguments
    {
        public Action OnPositivePopupResult { get; }
        public Action OnNegativePopupResult { get; }
        
        public string TitleLocalizationKey { get; }
        public string MessageLocalizationKey { get; }

        public ConfirmationPopupNavigationArguments(
            Action onPositivePopupResult, 
            Action onNegativePopupResult, 
            string titleLocalizationKey, 
            string messageLocalizationKey)
        {
            OnPositivePopupResult = onPositivePopupResult;
            OnNegativePopupResult = onNegativePopupResult;
            TitleLocalizationKey = titleLocalizationKey;
            MessageLocalizationKey = messageLocalizationKey;
        }
    }
}