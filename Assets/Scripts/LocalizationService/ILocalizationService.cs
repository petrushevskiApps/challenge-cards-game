using System;

namespace PetrushevskiApps.WhosGame.Scripts.LocalizationService
{
    public interface ILocalizationService
    {
        event Action LanguageChanged;
        string GetLocalizedString(string key, Language language);
        string GetLocalizedString(string key);
        void Initialize();
        void SetLanguage(Language language);
        Language GetCurrentLanguage();
    }
}
