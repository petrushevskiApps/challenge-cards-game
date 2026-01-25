using Localization;
using UserInterface.Popups;

namespace DefaultNamespace.Controllers
{
    public class SettingsPopupController: ISettingsPopupController
    {
        // Internal
        private ISettingsPopupView _view;
        
        // Injected
        private readonly ILocalizationService _localizationService;

        public SettingsPopupController(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            _localizationService.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged()
        {
            SetupLanguageSelection();
            SetTitle();
        }

        public void Setup(ISettingsPopupView view)
        {
            _view = view;
            SetupLanguageSelection();
            SetTitle();
        }

        public void EnglishToggleChanged(bool isOn)
        {
            if (isOn)
            {
                _localizationService.SetLanguage(Language.English);
            }
        }

        public void RussianToggleChanged(bool isOn)
        {
            if (isOn)
            {
                _localizationService.SetLanguage(Language.Russian);
            }
        }

        private void SetupLanguageSelection()
        {
            Language lang = _localizationService.GetCurrentLanguage();
            switch (lang)
            {
                case Language.English:
                    _view?.SetEnglishToggleState(true);
                    _view?.SetRussianToggleState(false);
                    break;
                case Language.Russian:
                    _view?.SetEnglishToggleState(false);
                    _view?.SetRussianToggleState(true);
                    break;
                default:
                    _view?.SetEnglishToggleState(true);
                    _view?.SetRussianToggleState(false);
                    break;
            }
        }

        private void SetTitle()
        {
            _view?.SetTitle(_localizationService.GetLocalizedString(LocalizationKeys.SelectLanguage));
        }
    }
}