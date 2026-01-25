using System;
using UserInterface.Popups;

namespace DefaultNamespace.Controllers
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