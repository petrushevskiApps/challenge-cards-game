using PetrushevskiApps.WhosGame.Scripts.NavigationService.Window;
using System;

namespace PetrushevskiApps.WhosGame.Scripts.NavigationService.Navigation
{
    public interface INavigationManager
    {
        event EventHandler AllScreensClosedEvent;

        /// <summary>
        ///     Hides or Closes the currently active screen on backstack
        ///     and shows screen of type T passing the navigational arguments
        ///     provided.
        /// </summary>
        /// <typeparam name="T">Type of screen to be shown.</typeparam>
        /// <typeparam name="TArguments">Arguments to be passed in Show method of the screen.</typeparam>
        void ShowScreen<T, TArguments>(TArguments navArguments) where T : IWindow;

        /// <summary>
        ///  Hides or Closes the currently active screen on backstack
        ///  and shows screen of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void ShowScreen<T>() where T : IWindow;

        /// <summary>
        /// Shows popup of type T passing the navigational arguments provided.
        /// </summary>
        /// <typeparam name="T">Type of popup to be shown.</typeparam>
        /// <typeparam name="TArguments">Arguments to be passed in Show method of the popup.</typeparam>
        void ShowPopup<T, TArguments>(TArguments navArguments) where T : IWindow;

        /// <summary>
        /// Shows popup of type T passing the navigational arguments provided.
        /// </summary>
        /// <typeparam name="T">Type of popup to be shown.</typeparam>
        void ShowPopup<T>() where T : IWindow;

        /// <summary>
        /// Closes and removes currently active screen (top-stack screen) from the backstack,
        /// and Resumes the next screen available on the stack.
        /// </summary>
        void GoBack();

        IBackHandler GetActiveBackHandler();
        void ClearAllStackScreens();
    }
}