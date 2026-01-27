namespace PetrushevskiApps.WhosGame.Scripts.NavigationService.Window
{
    /// <summary>
    ///     This interface represents window which can be
    ///     controlled by the <see cref="NavigationManager" />,
    ///     and defines the lifecycle methods of the window.
    /// </summary>
    public interface IWindow : IBackHandler
    {
        string ScreenTitle { get; }

        /// <summary>
        ///     <c>True</c>: if this screen should be added on backstack,
        ///     <c>False</c>: if this screen should not be added on backstack
        /// </summary>
        bool IsBackStackable { get; }

        /// <summary>
        ///     Screen Lifecycle method, Invoked by <see cref="NavigationManager" />
        ///     and used for setting and showing the screen.
        /// </summary>
        /// <param name="navArguments">Navigational arguments passed by invoking screens.</param>
        void Show<TArguments>(TArguments navArguments);

        /// <summary>
        ///     Screen Lifecycle method, invoked by <see cref="NavigationManager" />
        ///     when another screen is pushed on the backstack.
        ///     <remarks>
        ///         Use this screen to hide / disable the visuals / view of this screen.
        ///     </remarks>
        /// </summary>
        void Hide();

        /// <summary>
        ///     Screen Lifecycle method, invoked by <see cref="NavigationManager" />
        ///     when the screen is re-shown from the backstack.
        ///     <remarks>
        ///         Use this method to show / enable the visuals / view of this screen.
        ///     </remarks>
        /// </summary>
        void Resume();

        /// <summary>
        ///     Screen Lifecycle method, invoked by <see cref="NavigationManager" />
        ///     when the screen is removed from the backstack.
        ///     <remarks>
        ///         Use this method to clear all data which requires cleaning when this
        ///         screen is removed from backstack.
        ///     </remarks>
        /// </summary>
        void Close();
    }
}