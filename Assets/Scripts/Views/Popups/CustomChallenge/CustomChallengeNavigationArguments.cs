using System;

namespace PetrushevskiApps.WhosGame.Scripts.Views.Popups.CustomChallenge
{
    public sealed class CustomChallengeNavigationArguments
    {
        public Action<string> OnPopupResult  { get; }
        public string ChallengeDescription { get; }

        public CustomChallengeNavigationArguments(
            Action<string> onPopupResult,
            string description = null)
        {
            OnPopupResult = onPopupResult;
            ChallengeDescription = description;
        }
    }
}