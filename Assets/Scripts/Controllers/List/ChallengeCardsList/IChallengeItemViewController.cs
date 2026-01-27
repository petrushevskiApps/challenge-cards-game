namespace UserInterface.Views
{
    public interface IChallengeItemViewController
    {
        void SelectionToggled(bool isOn);
        void ItemClicked();
        void DeleteClicked();

        void Setup(
            IChallengeCardModel cardModel, 
            IPackageModel packageModel);

        void ViewHidden();
    }
}