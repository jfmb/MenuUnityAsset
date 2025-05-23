using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace Services.Languages
{
    public class LanguagesInstaller : GameService
    {
        [SerializeField] private SubOptionSO languageSettings;
        [SerializeField] private List<string> languagesAvailable;
        
        private Dictionary<string, CultureInfo> _allLanguagesInstalled = new();
        public Dictionary<string, CultureInfo> AllLanguagesInstalled => _allLanguagesInstalled;

        private string _twoLettersCodeForDefaultLanguage;
        
        public override void Install()
        {
            languagesAvailable = languageSettings.AllValues;

            for (var i = 0; i < languagesAvailable.Count; i++)
            {
                var language = languagesAvailable[i];
                var twoLettersName = CultureInfo.GetCultureInfoByIetfLanguageTag(language).TwoLetterISOLanguageName;
                if (!IsTwoLetterNameIsoValid(twoLettersName))
                {
                    Debug.LogError(language + " is not a valid language");

                    if (languageSettings.DefaultValueIndex == i)
                    {
                        Debug.LogError(language + " should be de default language but is not a valid language");
                    }
                    continue;
                }
                var newCultureInfo = new CultureInfo(twoLettersName);
                AllLanguagesInstalled.Add(twoLettersName, newCultureInfo);
                
                Debug.Log("My Debug: Language installed: " + twoLettersName);
                
                if (languageSettings.DefaultValueIndex == i)
                {
                    _twoLettersCodeForDefaultLanguage = twoLettersName;
                }
                
                Debug.Log("My debug: default language: " + _twoLettersCodeForDefaultLanguage);
            }
            // foreach (var language in languagesAvailable)
            // {
            //     var twoLettersName = CultureInfo.GetCultureInfoByIetfLanguageTag(language).TwoLetterISOLanguageName;
            //     if (!IsTwoLetterNameIsoValid(twoLettersName))
            //     {
            //         Debug.LogError(language + " is not a valid language");
            //         continue;
            //     }
            //     var newCultureInfo = new CultureInfo(twoLettersName);
            //
            //     AllLanguagesInstalled.Add(twoLettersName, newCultureInfo);
            // }

            DontDestroyOnLoad(this);
        }
        
        private bool IsTwoLetterNameIsoValid(string language)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            return cultures.Any(culture => culture.TwoLetterISOLanguageName == language);
        }

        public string GetDefaultLanguage()
        {
            return _twoLettersCodeForDefaultLanguage;
        }
    }
}