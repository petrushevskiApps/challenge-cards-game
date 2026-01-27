using System;

namespace PetrushevskiApps.WhosGame.Scripts.NavigationService.Window
{
    public interface IPopupEvents
    {
        event EventHandler PopupShownEvent;
        event EventHandler PopupResumedEvent;
        event EventHandler PopupHiddenEvent;
        event EventHandler PopupClosedEvent;
    }
}