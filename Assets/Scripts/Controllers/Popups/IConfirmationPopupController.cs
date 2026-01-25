using System;
using UserInterface.Popups;

namespace DefaultNamespace.Controllers
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