using System;

namespace PetrushevskiApps.WhosGame.Scripts.NavigationService.Window
{
    public interface IScreenEvents
    {
        event EventHandler ScreenShownEvent;
        event EventHandler ScreenResumedEvent;
        event EventHandler ScreenHiddenEvent;
        event EventHandler ScreenClosedEvent;
    }
}