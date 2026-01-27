using PetrushevskiApps.WhosGame.Scripts.NavigationService.Window;
using PetrushevskiApps.WhosGame.Scripts.NavigationService.WindowProvider;
using System;
using System.Collections.Generic;
using ModestTree;
using Zenject;

namespace PetrushevskiApps.WhosGame.Scripts.NavigationService.Navigation
{
    public class NavigationManager : 
        INavigationManager,
        IActiveScreenInfoProvider
    {
        public string Title => _screenBackStack.Peek().ScreenTitle;
        
        private readonly IWindowProvider _popupWindowProvider;

        private readonly Stack<IWindow> _screenBackStack = new();

        private readonly IWindowProvider _windowProvider;
        
        public NavigationManager(
            [Inject(Id = "Screen")] IWindowProvider windowProvider,
            [Inject(Id = "Popup")] IWindowProvider popupWindowProvider)
        {
            _windowProvider = windowProvider;
            _popupWindowProvider = popupWindowProvider;
        }

        public event EventHandler AllScreensClosedEvent;

        public void ShowScreen<T, TArguments>(TArguments navArguments) where T : IWindow
        {
            Show<T, TArguments>(_windowProvider, navArguments);
        }

        public void ShowScreen<T>() where T : IWindow
        {
            ShowScreen<T, NavigationArguments>(new NavigationArguments());
        }

        public void ShowPopup<T>() where T : IWindow
        {
            ShowPopup<T, NavigationArguments>(new NavigationArguments());
        }

        public void ShowPopup<T, TArguments>(TArguments navArguments) where T : IWindow
        {
            Show<T, TArguments>(_popupWindowProvider, navArguments);
        }

        public void GoBack()
        {
            if (!_screenBackStack.IsEmpty())
            {
                _screenBackStack.Pop().Close();
                if (!_screenBackStack.IsEmpty())
                {
                    _screenBackStack.Peek().Resume();
                }
                else
                {
                    AllScreensClosedEvent?.Invoke(this, EventArgs.Empty);
                }
            }
            else
            {
                AllScreensClosedEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public IBackHandler GetActiveBackHandler()
        {
            if (!_screenBackStack.IsEmpty())
            {
                return _screenBackStack.Peek();
            }

            return null;
        }

        public void ClearAllStackScreens()
        {
            while (!_screenBackStack.IsEmpty())
            {
                _screenBackStack.Pop().Close();
            }
        }

        private void Show<T, TArguments>(
            IWindowProvider windowProvider,
            TArguments navArguments) where T : IWindow
        {
            var screen = windowProvider.GetScreen<T>();

            if (screen != null)
            {
                if (!_screenBackStack.IsEmpty() && (_screenBackStack.Peek() is IUiPopup || screen is not IUiPopup))
                {
                    // We skip hiding the current screen only if
                    // there is no active screen on the stack or
                    // the current screen is Screen and the new is Popup.
                    HideCurrentScreenIn(_screenBackStack);
                }
                
                if (_screenBackStack.Contains(screen))
                {
                    ClearStackToScreen(_screenBackStack, screen);
                    screen.Resume();
                }
                else
                {
                    _screenBackStack.Push(screen);
                    screen.Show(navArguments);
                }
            }
            else
            {
                throw new Exception($"Screen of type {typeof(T)} not found in screens list!");
            }
        }

        private void HideCurrentScreenIn(Stack<IWindow> stack)
        {
            if (stack.Count == 0)
            {
                return;
            }

            if (stack.Peek() is IUiPopup)
            {
                if (stack.Peek().IsBackStackable)
                {
                    stack.Peek().Hide();
                }
                else
                {
                    stack.Pop().Close();
                    HideCurrentScreenIn(stack);
                }
            }
            else
            {
                if (stack.Peek().IsBackStackable)
                {
                    stack.Peek().Hide();
                }
                else
                {
                    stack.Pop().Close();
                }
            }
            
        }

        private void ClearStackToScreen(Stack<IWindow> stack, IWindow window)
        {
            while (!stack.IsEmpty())
            {
                if (!stack.Peek().Equals(window))
                {
                    stack.Pop().Close();
                }
                else
                {
                    break;
                }
            }
        }
    }
}