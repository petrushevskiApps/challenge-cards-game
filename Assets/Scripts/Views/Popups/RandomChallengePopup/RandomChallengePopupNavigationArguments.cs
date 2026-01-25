using System;

namespace UserInterface.Popups
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