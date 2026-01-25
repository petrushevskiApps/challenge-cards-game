using TwoOneTwoGames.UIManager.ScreenNavigation;

namespace UserInterface.Views
{
    public class PackageItemViewController: IPackageItemViewController
    {
        private readonly IScreenNavigation _screenNavigation;
        private readonly IPackageModel _packageModel;
        private readonly PackageListItemView _view;

        public PackageItemViewController(
            IScreenNavigation screenNavigation,
            IPackageModel packageModel,
            PackageListItemView view)
        {
            _screenNavigation = screenNavigation;
            _packageModel = packageModel;
            _view = view;

            SubscribeToEvents();
            
            SetTitle(_packageModel.Title);
            SetPackageInfo(_packageModel.ChallengeCards?.Count ?? 0);
        }
        
        public void Clear()
        {
            UnsubscribeFromEvents();
        }

        public void ItemClicked()
        {
            _screenNavigation?.ShowChallengeScreen(_packageModel);
        }
        
        private void SubscribeToEvents()
        {
            _packageModel.TitleChanged += SetTitle;
            _packageModel.CardsNumberChanged += SetPackageInfo;
        }

        private void UnsubscribeFromEvents()
        {
            _packageModel.TitleChanged -= SetTitle;
            _packageModel.CardsNumberChanged -= SetPackageInfo;
        }

        private void SetPackageInfo(int cardsCount)
        {
            _view.SetPackageInfo($"{cardsCount} cards");
        }

        private void SetTitle(string title)
        {
            _view.SetTitle(title);
        }
    }
}