using System;
using Cysharp.Threading.Tasks;
using Localization;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GameManager: MonoBehaviour, IPauseGameController
    {
        private const int UNLIMITED_FPS = -1;
        private const int MOBILE_FPS = 120;
        
        public event Action<bool> GamePausedEvent;
        
        private bool _isGamePaused;
        
        // Injected
        private IScreenNavigation _screenNavigation;
        private ILocalizationService _localizationService;
        private IPackageRepository _packageRepository;

        [Inject]
        public void Initialize(
            IPackageRepository packageRepository,
            IScreenNavigation screenNavigation,
            ILocalizationService localizationService)
        {
            _screenNavigation = screenNavigation;
            _localizationService = localizationService;
            _packageRepository = packageRepository;
        }
        
        private void Awake()
        {
            SetupGame();
        }
        
        private void SetupGame()
        {
            Input.multiTouchEnabled = false;
            SetupFrameRate();
            _localizationService.Initialize();
            _packageRepository.LoadPackagesAsync().Forget();
            _screenNavigation.ShowMainScreen();
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void TogglePauseGame(bool pause)
        {
            if (pause)
            {
                Time.timeScale = 0;
                _isGamePaused = true;
            }
            else
            {
                Time.timeScale = 1;
                _isGamePaused = false;
            }

            GamePausedEvent?.Invoke(_isGamePaused);
        }

        private void SetupFrameRate()
        {
            Application.targetFrameRate = Application.isEditor ? UNLIMITED_FPS : MOBILE_FPS;
        }
    }
}