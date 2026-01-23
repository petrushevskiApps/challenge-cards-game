namespace UserInterface.Screens
{
    public interface IMainScreenView
    {
        void SetMessage(bool isVisible, string message);
        void SetPlayButton(bool isVisible);
    }
}