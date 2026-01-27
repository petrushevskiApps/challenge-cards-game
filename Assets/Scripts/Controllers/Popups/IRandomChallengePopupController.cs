using System;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.RandomChallengePopup;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.Popups
{
    public interface IRandomChallengePopupController
    {
        void GenerateClicked();
        void CountSelected(int i);
        void Setup(IRandomChallengePopupView view, Action<int> onPopupResult);
        void ScreenShown();
        void ScreenClosed();
    }
}