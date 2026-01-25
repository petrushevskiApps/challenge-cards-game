namespace UserInterface.Views
{
    public interface IPackageItemViewController
    {
        void Clear();
        void ItemClicked();
        void ToggleSelected(bool isOn);
    }
}