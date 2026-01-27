using System;

namespace TwoOneTwoGames.UIManager.InfiniteScrollList
{
    public interface IScrollableList
    {
        /// <summary>
        ///     Used for scrolling the list up.
        /// </summary>
        void ScrollUp();

        /// <summary>
        ///     Used for scrolling the list down.
        /// </summary>
        void ScrollDown();

        /// <summary>
        ///     Used for moving the list to top.
        /// </summary>
        void ToTop();

        /// <summary>
        ///     Used for moving the list to bottom.
        /// </summary>
        void ToBottom();

        /// <summary>
        ///     Used for moving the list to element index.
        /// </summary>
        /// <param name="rowIndex">Index of an element to which we want the scroll view scrolled to.</param>
        /// <param name="elementAlignment">Where should the element with rowIndex be aligned in the viewport.</param>
        void ToElement(int rowIndex, AlignAt elementAlignment = AlignAt.Top);

        event EventHandler ScrolledToTopEvent;
        event EventHandler ScrolledToBottomEvent;
        event EventHandler ScrollingCompletedEvent;
    }
}