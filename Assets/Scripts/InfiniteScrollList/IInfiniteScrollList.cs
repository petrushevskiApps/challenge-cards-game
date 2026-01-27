using System;
using UnityEngine;

namespace PetrushevskiApps.WhosGame.Scripts.InfiniteScrollListService
{
    public interface IInfiniteScrollList
        : IScrollableList
    {
        /// <summary>
        ///     Event invoked when the list is scrolled to the end.
        /// </summary>
        event EventHandler OnListEndEvent;

        /// <summary>
        ///     Event invoked when the center of the row (+- some tolerance) enters the viewport.
        /// </summary>
        event EventHandler<ListRow> RowVisibleEvent;

        /// <summary>
        ///     Event invoked when the center of the row (+- some tolerance) exits the viewport.
        /// </summary>
        event EventHandler<ListRow> RowHiddenEvent;

        event EventHandler RowsVisibilityUpdatedEvent;
        
        public void Initialize(
            float headerSize,
            float elementHeight);

        /// <summary>
        ///     Used for adding new data pages to the scroll list.
        /// </summary>
        /// <param name="rowsPerPage">Number of items in the page which need to be added.</param>
        /// <param name="scrollToRow">Once the new page is added the list should be scrolled to this row.</param>
        /// <returns>Returns a list of index and position tuples for elements in this page.</returns>
        void AddRows(int rowsPerPage, int scrollToRow = -1);

        /// <summary>
        ///     Removes all calculated rows and sizes and sets the list to clean slate.
        /// </summary>
        void ClearList();

        /// <summary>
        ///     Checks if all 4 corners of the Rect passed as parameter
        ///     are visible, inside the viewport. With some minor tolerance
        ///     for top and bottom of the list.
        /// </summary>
        /// <param name="rect">Rect to be checked for visibility.</param>
        /// <returns>
        ///     <c>true</c>If all 4 corners of the rect are inside the viewport.
        ///     <c>false</c>If any of the corners is outside the viewport.
        /// </returns>
        bool IsViewFullyVisible(RectTransform rect);

        event EventHandler RowsAddedEvent;
    }
}