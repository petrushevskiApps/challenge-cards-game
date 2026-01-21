using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class ScreenProvider : MonoBehaviour, IWindowProvider
    {
        [SerializeField]
        [Tooltip("List of screens to be provided to Navigation Manager")]
        private List<UIScreen> _screens = new();

        public IWindow GetScreen<T>() where T : IWindow
        {
            return _screens.OfType<T>().FirstOrDefault();
        }
    }
}