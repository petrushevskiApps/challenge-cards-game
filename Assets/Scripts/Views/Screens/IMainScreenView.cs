using UserInterface.Views;

namespace UserInterface.Screens
{
    public interface IMainScreenView
    {
        void SetMessage(string message);
        void SetMessageVisibility(bool isVisible);
        void SetPlayButton(bool isVisible);
        IListView ListView { get; }
        void SetPackageListLabel(string label);
        void SetCreatePackageButtonLabel(string label);
        void SetPlayButtonLabel(string label);
    }
}