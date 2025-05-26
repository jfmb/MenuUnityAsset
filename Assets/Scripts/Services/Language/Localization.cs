using System.Collections.Generic;
using UnityEngine;

namespace Services.Languages
{
    public class Localization
    {
        private Dictionary<string, Dictionary<string, string>> _localizationData;

        public Dictionary<string, Dictionary<string, string>> LocalizationData
        {
            get => _localizationData;
            set => _localizationData = value;
        }
        
        public string GetLocalizedText(string key)
        {
            var languageCode = ServiceLocator.GetService<Languages>().TwoLettersCodeForCurrentLanguage;
            
            Debug.Log("My debug: two letters current language saved: " + languageCode + " ------ key: " + key);

            if (_localizationData.TryGetValue(key, out var translations))
            {
                if (translations.TryGetValue(languageCode, out var localizedText))
                {
                    return localizedText;
                }
            }

            return $"Missing {key} in {languageCode}";
        }
    }
}