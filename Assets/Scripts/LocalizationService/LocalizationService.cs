using System;
using System.Collections.Generic;
using UnityEngine;

namespace PetrushevskiApps.WhosGame.Scripts.LocalizationService
{
    public class LocalizationService : ILocalizationService
    {
        private const string LOCALIZATION_FILE_NAME = "localization";
        private const string LANGUAGE_PREF_KEY = "SelectedLanguage";

        public event Action LanguageChanged;
        
        private Dictionary<string, Dictionary<Language, string>> _localizationData;
        private Language _currentLanguage;

        public void Initialize()
        {
            _localizationData = new Dictionary<string, Dictionary<Language, string>>();
            LoadLanguagePreference();
            LoadLocalizationData();
        }

        public void SetLanguage(Language language)
        {
            _currentLanguage = language;
            SaveLanguagePreference(language);
            LanguageChanged?.Invoke();
        }

        public string GetLocalizedString(string key)
        {
            return GetLocalizedString(key, _currentLanguage);
        }

        public string GetLocalizedString(string key, Language language)
        {
            if (_localizationData == null)
            {
                Debug.LogError("Localization data not initialized. Call Initialize() first.");
                return key;
            }

            if (_localizationData.TryGetValue(key, out Dictionary<Language, string> translations))
            {
                if (translations.TryGetValue(language, out string translation))
                {
                    return translation;
                }
                Debug.LogWarning($"Translation not found for key: {key}, language: {language}");
            }
            else
            {
                Debug.LogWarning($"Key not found in localization data: {key}");
            }

            return key;
        }

        public Language GetCurrentLanguage()
        {
            return _currentLanguage;
        }

        private void LoadLanguagePreference()
        {
            if (PlayerPrefs.HasKey(LANGUAGE_PREF_KEY))
            {
                SetLanguage((Language)PlayerPrefs.GetInt(LANGUAGE_PREF_KEY));
            }
            else
            {
                DetectSystemLanguage();
            }
        }

        private void SaveLanguagePreference(Language language)
        {
            PlayerPrefs.SetInt(LANGUAGE_PREF_KEY, (int)language);
            PlayerPrefs.Save();
        }

        private void DetectSystemLanguage()
        {
            SystemLanguage systemLanguage = Application.systemLanguage;
            
            if (systemLanguage == SystemLanguage.Russian)
            {
                SetLanguage(Language.Russian);
            }
            else
            {
                SetLanguage(Language.English);
            }
        }

        private void LoadLocalizationData()
        {
            TextAsset csvFile = Resources.Load<TextAsset>(LOCALIZATION_FILE_NAME);
            
            if (csvFile == null)
            {
                Debug.LogError($"Localization file not found: {LOCALIZATION_FILE_NAME}");
                return;
            }

            ParseCSV(csvFile.text);
        }

        private void ParseCSV(string csvText)
        {
            string[] lines = csvText.Split('\n');
            
            if (lines.Length < 2)
            {
                Debug.LogError("Invalid CSV format: Not enough lines");
                return;
            }

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                string[] columns = ParseCSVLine(line);
                
                if (columns.Length >= 3)
                {
                    string key = columns[0];
                    string englishText = columns[1];
                    string russianText = columns[2];

                    Dictionary<Language, string> translations = new Dictionary<Language, string>
                    {
                        { Language.English, englishText },
                        { Language.Russian, russianText }
                    };

                    _localizationData[key] = translations;
                }
            }

            Debug.Log($"Loaded {_localizationData.Count} localization entries");
        }

        private string[] ParseCSVLine(string line)
        {
            List<string> result = new List<string>();
            bool inQuotes = false;
            string current = "";

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(current);
                    current = "";
                }
                else if (c != '\r')
                {
                    current += c;
                }
            }

            result.Add(current);
            return result.ToArray();
        }
    }
}
