using Localization;
using TwoOneTwoGames.UIManager.ScreenNavigation;

namespace UserInterface.Views
{
    public class PackageItemViewController: IPackageItemViewController
    {
        private readonly ILocalizationService _localizationService;
        private readonly IScreenNavigation _screenNavigation;
        private readonly IPackageModel _packageModel;
        private readonly PackageListItemView _view;

        public PackageItemViewController(
            ILocalizationService localizationService,
            IScreenNavigation screenNavigation,
            IPackageModel packageModel,
            PackageListItemView view)
        {
            _localizationService = localizationService;
            _screenNavigation = screenNavigation;
            _packageModel = packageModel;
            _view = view;

            SubscribeToEvents();
            
            SetTitle(_packageModel.Title);
            SetPackageInfo();
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
            _localizationService.LanguageChanged += OnLanguageChanged;
        }

        private void UnsubscribeFromEvents()
        {
            _packageModel.TitleChanged -= SetTitle;
            _packageModel.CardsNumberChanged -= SetPackageInfo;
            _localizationService.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged()
        {
            SetPackageInfo();
        }

        private void SetPackageInfo()
        {
            int cardsCount = _packageModel.ChallengeCards?.Count ?? 0;
            _view.SetPackageInfo($"{cardsCount} {_localizationService.GetLocalizedString(LocalizationKeys.Cards)}");
        }

        private void SetTitle(string title)
        {
            _view.SetTitle(title);
        }
    }
}