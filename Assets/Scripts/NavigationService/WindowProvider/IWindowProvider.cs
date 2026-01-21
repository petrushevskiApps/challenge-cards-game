namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public interface IWindowProvider
    {
        /// <summary>
        ///     Find and return the screen of type T from list.
        /// </summary>
        /// <typeparam name="T">Inherited type of interface <see cref="IWindow" /></typeparam>
        /// <returns>Screen matching the provided type T inheriting interface <see cref="IWindow" /></returns>
        IWindow GetScreen<T>() where T : IWindow;
    }
}