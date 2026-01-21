using System;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public interface IPopupEvents
    {
        event EventHandler PopupShownEvent;
        event EventHandler PopupResumedEvent;
        event EventHandler PopupHiddenEvent;
        event EventHandler PopupClosedEvent;
    }
}