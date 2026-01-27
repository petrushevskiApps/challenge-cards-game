using System;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.CustomChallenge;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.Popups
{
    public interface IAddEditChallengePopupController
    {
        void InputTextUpdated(string inputFieldValue);

        void Setup(
            ICustomChallengePopupView view,
            Action<string> onPopupResult,
            string challengeDescriptionText = null);
        
        void ActionButtonClicked();
        void PopupShown();
        void PopupClosed();
    }
}