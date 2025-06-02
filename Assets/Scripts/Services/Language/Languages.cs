using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Services.Languages
{
    public class Languages
    {
        public Dictionary<string, CultureInfo> LanguagesInGame { get; } = new();

        public string TwoLettersCodeForDefaultLanguage { get; private set; }

        public string TwoLettersCodeForCurrentLanguage { get; private set; }


        public void Add(string key, CultureInfo language)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.Log("Key for language is null or empty. Language not installed: " + language.TwoLetterISOLanguageName);
                return;
            }

            if (LanguagesInGame.ContainsKey(key))
            {
                Debug.Log(key + " is already in the dictionary. Check if it's ok for " + language.TwoLetterISOLanguageName);
                return;
            }
            
            LanguagesInGame[key] = language;
        }
        
        public void SetDefaultLanguage(string twoLettersCode)
        {
            if (string.IsNullOrEmpty(twoLettersCode))
            {
                Debug.Log("Two letters code is null or empty");
                return;
            }
            TwoLettersCodeForDefaultLanguage = twoLettersCode;
        }
        
        public void SetCurrentLanguage(string twoLettersCode)
        {
            if (string.IsNullOrEmpty(twoLettersCode))
            {
                Debug.Log("Two letters code is null or empty");
                return;
            }
            
            TwoLettersCodeForCurrentLanguage = twoLettersCode;
        }
    }
}