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
        
        private Languages _languagesInGame = new Languages();
        
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
                        Debug.LogError(language + " should be the default language but is not a valid language");
                    }
                    continue;
                }
                var newCultureInfo = new CultureInfo(twoLettersName);
                _languagesInGame.Add(twoLettersName, newCultureInfo);
                
                Debug.Log("My Debug: Language installed: " + twoLettersName);
                
                if (languageSettings.DefaultValueIndex == i)
                {
                    _languagesInGame.SetDefaultLanguage(twoLettersName);
                }
            }

//            DontDestroyOnLoad(languagesInGame);
            InstallLanguagesInServiceLocator();
        }

        private void InstallLanguagesInServiceLocator()
        {
            ServiceLocator.RegisterService(_languagesInGame);
        }

        private bool IsTwoLetterNameIsoValid(string language)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            return cultures.Any(culture => culture.TwoLetterISOLanguageName == language);
        }
    }
}