using PetrushevskiApps.WhosGame.Scripts.Models;

namespace PetrushevskiApps.WhosGame.Scripts.Views.Screens
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