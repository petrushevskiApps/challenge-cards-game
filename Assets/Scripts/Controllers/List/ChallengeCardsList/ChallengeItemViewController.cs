using Cysharp.Threading.Tasks;
using PetrushevskiApps.WhosGame.Scripts.LocalizationService;
using PetrushevskiApps.WhosGame.Scripts.Models;
using PetrushevskiApps.WhosGame.Scripts.NavigationCoordinator;
using PetrushevskiApps.WhosGame.Scripts.Repositories.PackageRepositoryService;
using PetrushevskiApps.WhosGame.Scripts.Views.List;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.ConfirmationPopup;
using PetrushevskiApps.WhosGame.Scripts.Views.Popups.CustomChallenge;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.List.ChallengeCardsList
{
    public class ChallengeItemViewController : IChallengeItemViewController
    {
        private IChallengeModel _challengeModel;
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
            IChallengeModel challengeModel, 
            IPackageModel packageModel)
        {
            _challengeModel = challengeModel;
            _packageModel = packageModel;
            
            _view.SetTitle(challengeModel.Title);
            _view.SetDescription(challengeModel.Description);
            _view.SetSelection(challengeModel.IsSelected);

            SubscribeToEvents();
        }

        public void ViewHidden()
        {
            UnsubscribeFromEvents();
        }

        public void SelectionToggled(bool isOn)
        {
            _challengeModel?.SetSelected(isOn);
            _packageRepository.SavePackagesAsync().Forget();
        }

        public void ItemClicked()
        {
            _popupNavigation.ShowCustomChallengePopup(
                new CustomChallengeNavigationArguments(OnEditChallengePopupResult, _challengeModel.Description));
        }

        private void OnEditChallengePopupResult(string description)
        {
            _challengeModel.UpdateDescription(description);
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
            _challengeModel.TitleChanged += _view.SetTitle;
            _challengeModel.DescriptionChanged += _view.SetDescription;
            _challengeModel.SelectionChanged += _view.SetSelection;
        }

        private void UnsubscribeFromEvents()
        {
            _challengeModel.TitleChanged -= _view.SetTitle;
            _challengeModel.DescriptionChanged -= _view.SetDescription;
            _challengeModel.SelectionChanged -= _view.SetSelection;
        }

        private void DeleteCard()
        {
            _packageModel.RemoveChallengeCardModel(_challengeModel);
        }
    }
}