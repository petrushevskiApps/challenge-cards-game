using System;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.ConfirmationPopup;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.Popups
{
    public interface IConfirmationPopupController
    {
        void Setup(
            IConfirmationPopupView confirmationPopup, 
            Action argsOnPositivePopupResult,
            Action argsOnNegativePopupResult, 
            string argsTitleLocalizationKey, 
            string argsMessageLocalizationKey);
        void PositiveButtonClicked();
        void NegativeButtonClicked();
        void ScreenShown();
        void ScreenClosed();
    }
}