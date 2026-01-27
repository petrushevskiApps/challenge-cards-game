using PetrushevskiApps.WhosGame.Scripts.Models;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.List.ChallengeCardsList
{
    public interface IChallengeItemViewController
    {
        void SelectionToggled(bool isOn);
        void ItemClicked();
        void DeleteClicked();

        void Setup(
            IChallengeModel challengeModel, 
            IPackageModel packageModel);

        void ViewHidden();
    }
}