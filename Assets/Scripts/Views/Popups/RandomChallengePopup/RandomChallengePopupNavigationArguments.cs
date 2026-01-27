using System;

namespace PetrushevskiApps.WhosGame.Scripts.Views.Popups.RandomChallengePopup
{
    public class RandomChallengePopupNavigationArguments
    {
        public Action<int> OnPopupResult { get; }

        public RandomChallengePopupNavigationArguments(Action<int> onPopupResult)
        {
            OnPopupResult = onPopupResult;
        }
    }
}