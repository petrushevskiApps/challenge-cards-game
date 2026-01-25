using UserInterface.Popups;

namespace DefaultNamespace.Controllers
{
    public interface IAddEditChallengePopupController
    {
        void InputTextUpdated(string inputFieldValue);

        void SetView(
            IEditChallengePopupView view,
            IPackageModel packageModel,
            IChallengeCardModel challengeCardModel = null,
            string challengeDescriptionText = null);
        void ActionButtonClicked();
    }
}