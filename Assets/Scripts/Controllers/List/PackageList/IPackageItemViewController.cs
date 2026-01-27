namespace PetrushevskiApps.WhosGame.Scripts.Controllers.List.PackageList
{
    public interface IPackageItemViewController
    {
        void Clear();
        void ItemClicked();
        void ToggleSelected(bool isOn);
    }
}