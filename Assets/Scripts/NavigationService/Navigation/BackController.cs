using UnityEngine;
using Zenject;

namespace TwoOneTwoGames.UIManager.ScreenNavigation
{
    public class BackController : MonoBehaviour
    {
        [Inject]
        private INavigationManager _navigationManager;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _navigationManager.GetActiveBackHandler()?.OnBackTriggered();
            }
        }
    }
}