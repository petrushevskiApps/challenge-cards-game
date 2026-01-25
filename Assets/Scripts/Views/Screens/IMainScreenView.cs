using UserInterface.Views;

namespace UserInterface.Screens
{
    public interface IMainScreenView
    {
        void SetMessage(bool isVisible, string message);
        void SetPlayButton(bool isVisible);
        IListView ListView { get; }
    }
}