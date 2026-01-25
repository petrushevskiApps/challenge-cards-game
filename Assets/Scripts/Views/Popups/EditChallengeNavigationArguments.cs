namespace UserInterface.Popups
{
    public sealed class EditChallengeNavigationArguments
    {
        public IPackageModel PackageModel { get; }
        public IChallengeCardModel ChallengeCardModel { get; }
        public string ChallengeDescription { get; }

        public EditChallengeNavigationArguments(
            IPackageModel packageModel, 
            IChallengeCardModel challengeCardModel = null, 
            string description = null)
        {
            ChallengeDescription = description;
            ChallengeCardModel = challengeCardModel;
            PackageModel = packageModel;
        }
    }
}