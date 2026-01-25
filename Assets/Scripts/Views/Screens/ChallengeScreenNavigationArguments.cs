namespace UserInterface.Screens
{
    public struct ChallengeScreenNavigationArguments
    {
        public IPackageModel PackageModel { get; }

        public ChallengeScreenNavigationArguments(IPackageModel packageModel)
        {
            PackageModel = packageModel;
        }
    }
}