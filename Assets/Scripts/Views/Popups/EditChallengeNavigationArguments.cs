using System;

namespace UserInterface.Popups
{
    public sealed class EditChallengeNavigationArguments
    {
        public Action<string> OnPopupResult  { get; }
        public string ChallengeDescription { get; }

        public EditChallengeNavigationArguments(
            Action<string> onPopupResult,
            string description = null)
        {
            OnPopupResult = onPopupResult;
            ChallengeDescription = description;
        }
    }
}