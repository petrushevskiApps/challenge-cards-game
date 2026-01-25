using System;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UserInterface.Popups;

namespace DefaultNamespace.Controllers
{
    public class RandomChallengePopupController: IRandomChallengePopupController
    {
        private int _challengesCount;
        private IRandomChallengePopupView _view;
        private Action<int> _onPopupResult;
        
        private readonly INavigationManager _navigationManager;

        public RandomChallengePopupController(INavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        
        public void Setup(IRandomChallengePopupView view, Action<int> onPopupResult)
        {
            _view = view;
            _onPopupResult = onPopupResult;
            
            _view.SetTitle("Random Challenge");
            _view.SetDescription("How many challenges do you want to generate ?");
            _view.SetButtonTitle("Generate It Now");
        }
        
        public void GenerateClicked()
        {
            _navigationManager.GoBack();
            _onPopupResult?.Invoke(_challengesCount);
        }

        public void CountSelected(int count)
        {
            _challengesCount = count;
        }
    }
}