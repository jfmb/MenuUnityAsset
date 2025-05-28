using System.Collections.Generic;
using UnityEngine;

namespace Services.Languages
{
    public class Localization
    {
        //Format: [key[two letters language code, localized text]]
        private Dictionary<string, Dictionary<string, string>> _localizationData;

        public Dictionary<string, Dictionary<string, string>> LocalizationData
        {
            get => _localizationData;
            set => _localizationData = value;
        }
        
        public string GetLocalizedText(string key)
        {
            var translations = _localizationData[key];

            var languageCode = ServiceLocator.GetService<Languages>().TwoLettersCodeForCurrentLanguage;
            return translations.TryGetValue(languageCode, out var localizedText) ?
                localizedText :
                $"Missing {key} in {languageCode}";

            // if (!_localizationData.TryGetValue(key, out var translations)
            //     || translations == null)
            // {
            //     return $"Missing value for {key}";
            // }
            //
            // foreach (var VARIABLE in translations)
            // {
            //     Debug.Log("!!!!Key: " + VARIABLE.Key + " -> " + VARIABLE.Value);
            // }
            //
            // return GetLocalizedTextInCurrentTranslations(key, translations);
        }

        // private static string GetLocalizedTextInCurrentTranslations(string key, Dictionary<string, string> translations)
        // {
        //     var languageCode = ServiceLocator.GetService<Languages>().TwoLettersCodeForCurrentLanguage;
        //
        //     return translations.TryGetValue(languageCode, out var localizedText) ?
        //         localizedText :
        //         $"Missing {key} in {languageCode}";
        // }
    }
}