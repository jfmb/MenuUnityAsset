using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ScriptableObjects.Languages;
using UnityEngine;

namespace Services.Languages
{
    public class LanguagesInstaller : MonoBehaviour
    {
        [SerializeField] private LanguagesAvailableSO languagesAvailable;
        
        private Dictionary<string, CultureInfo> _allLanguagesInstalled = new();
        public Dictionary<string, CultureInfo> AllLanguagesInstalled => _allLanguagesInstalled;

        public void Install()
        {
            foreach (var element in languagesAvailable.TwoLettersIsoLanguages)
            {
                var twoLettersName = element.twoLetterValidIsoLanguage;
                var newCultureInfo = new CultureInfo(twoLettersName);
                if (!IsTwoLetterIsoLanguageName(twoLettersName))
                {
                    Debug.LogError(element + " is not a valid language");
                    continue;
                }

                AllLanguagesInstalled.Add(twoLettersName, newCultureInfo);
            }

            if (!IsValidDefaultLanguage())
            {
                Debug.LogWarning("Default Language is not included in Languages available");
            }
            
            DontDestroyOnLoad(this);
        }

        private bool IsValidDefaultLanguage()
        {
            return _allLanguagesInstalled.ContainsKey(languagesAvailable.DefaultLanguage.twoLetterValidIsoLanguage);
        }
        
        private bool IsTwoLetterIsoLanguageName(string element)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            return cultures.Any(culture => culture.TwoLetterISOLanguageName == element);
        }

        public LanguageSO GetDefaultLanguage()
        {
            return languagesAvailable.DefaultLanguage;
        }
    }
}