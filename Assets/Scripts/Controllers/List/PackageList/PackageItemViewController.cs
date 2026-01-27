using System;
using System.Text;
using PetrushevskiApps.WhosGame.Scripts.LocalizationService;
using PetrushevskiApps.WhosGame.Scripts.Models;
using PetrushevskiApps.WhosGame.Scripts.NavigationCoordinator;
using PetrushevskiApps.WhosGame.Scripts.Views.List;

namespace PetrushevskiApps.WhosGame.Scripts.Controllers.List.PackageList
{
    public class PackageItemViewController: IPackageItemViewController
    {
        private readonly ILocalizationService _localizationService;
        private readonly IScreenNavigation _screenNavigation;
        private readonly IPackageModel _packageModel;
        private readonly PackageListItemView _view;
        private readonly Action<IPackageModel, bool> _onPackageSelected;

        public PackageItemViewController(
            ILocalizationService localizationService,
            IScreenNavigation screenNavigation,
            IPackageModel packageModel,
            PackageListItemView view, 
            Action<IPackageModel, bool> onPackageSelected)
        {
            _localizationService = localizationService;
            _screenNavigation = screenNavigation;
            _packageModel = packageModel;
            _view = view;
            _onPackageSelected = onPackageSelected;

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

        public void ToggleSelected(bool isOn)
        {
            _onPackageSelected?.Invoke(_packageModel, isOn);
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
            int activeCards = _packageModel.GetNumberOfActiveCards();

            StringBuilder packageInfo = new StringBuilder();
            packageInfo.Append(cardsCount);
            packageInfo.Append(' ');
            packageInfo.Append(_localizationService.GetLocalizedString(LocalizationKeys.Cards));
            packageInfo.Append(" ( ");
            packageInfo.Append(activeCards);
            packageInfo.Append(" ");
            packageInfo.Append(_localizationService.GetLocalizedString(LocalizationKeys.ActiveCards));
            packageInfo.Append(" ) ");
            _view.SetPackageInfo(packageInfo.ToString());
        }

        private void SetTitle(string title)
        {
            _view.SetTitle(title);
        }
    }
}