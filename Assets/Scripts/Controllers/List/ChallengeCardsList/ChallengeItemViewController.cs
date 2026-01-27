using Cysharp.Threading.Tasks;
using Localization;
using TwoOneTwoGames.UIManager.ScreenNavigation;
using UserInterface.Popups;

namespace UserInterface.Views
{
    public class ChallengeItemViewController : IChallengeItemViewController
    {
        private IChallengeCardModel _cardModel;
        private IPackageModel _packageModel;
        private readonly IPackageRepository _packageRepository;
        private readonly ChallengeItemView _view;
        private readonly IPopupNavigation _popupNavigation;

        public ChallengeItemViewController(
            IPackageRepository packageRepository,
            ChallengeItemView view,
            IPopupNavigation popupNavigation)
        {
            _packageRepository = packageRepository;
            _view = view;
            _popupNavigation = popupNavigation;
        }

        public void Setup(
            IChallengeCardModel cardModel, 
            IPackageModel packageModel)
        {
            _cardModel = cardModel;
            _packageModel = packageModel;
            
            _view.SetTitle(cardModel.Title);
            _view.SetDescription(cardModel.Description);
            _view.SetSelection(cardModel.IsSelected);

            SubscribeToEvents();
        }

        public void ViewHidden()
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

        private void DeleteCard()
        {
            _packageModel.RemoveChallengeCardModel(_cardModel);
        }
    }
}