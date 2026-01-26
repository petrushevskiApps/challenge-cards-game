using System;
using UserInterface.Popups;

namespace DefaultNamespace.Controllers
{
    public interface IAddEditChallengePopupController
    {
        void InputTextUpdated(string inputFieldValue);

        void Setup(
            IEditChallengePopupView view,
            Action<string> onPopupResult,
            string challengeDescriptionText = null);
        
        void ActionButtonClicked();
        void PopupShown();
        void PopupClosed();
    }
}