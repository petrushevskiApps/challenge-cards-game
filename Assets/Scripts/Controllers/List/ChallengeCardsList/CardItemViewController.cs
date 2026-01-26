using Cysharp.Threading.Tasks;
using Localization;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UserInterface.Popups;

namespace UserInterface.Views
{
    public class CardItemViewController : ICardItemViewController
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IChallengeCardModel _cardModel;
        private readonly IPackageModel _packageModel;
        private readonly ChallengeCardListItemView _view;
        private readonly IPopupNavigation _popupNavigation;

        public CardItemViewController(
            IPackageRepository packageRepository,
            IChallengeCardModel cardModel, 
            IPackageModel packageModel,
            ChallengeCardListItemView view,
            IPopupNavigation popupNavigation)
        {
            _packageRepository = packageRepository;
            _cardModel = cardModel;
            _packageModel = packageModel;
            _view = view;
            _popupNavigation = popupNavigation;

            SubscribeToEvents();

            view.SetTitle(cardModel.Title);
            view.SetDescription(cardModel.Description);
            view.SetSelection(cardModel.IsSelected);
        }

        public void Clear()
        {
            UnsubscribeFromEvents();
        }

        public void SelectionToggled(bool isOn)
        {
            _cardModel?.SetSelected(isOn);
            _packageRepository.SavePackagesAsync().Forget();
        }

        public void ItemClicked()
        {
            _popupNavigation.ShowEditChallengePopup(
                new EditChallengeNavigationArguments(OnEditChallengePopupResult, _cardModel.Description));
        }

        private void OnEditChallengePopupResult(string description)
        {
            _cardModel.UpdateDescription(description);
        }
        
        public void DeleteClicked()
        {
            _popupNavigation.ShowConfirmationPopup(new ConfirmationPopupNavigationArguments(
                DeleteCard,
                () => { },
                LocalizationKeys.ConfirmRemoveChallenge,
                LocalizationKeys.CannotBeUndone));
        }

        private void DeleteCard()
        {
            _packageModel.RemoveChallengeCardModel(_cardModel);
        }
        
        private void SubscribeToEvents()
        {
            _cardModel.TitleChanged += _view.SetTitle;
            _cardModel.DescriptionChanged += _view.SetDescription;
            _cardModel.SelectionChanged += _view.SetSelection;
        }

        private void UnsubscribeFromEvents()
        {
            _cardModel.TitleChanged -= _view.SetTitle;
            _cardModel.DescriptionChanged -= _view.SetDescription;
            _cardModel.SelectionChanged -= _view.SetSelection;
        }
    }
}