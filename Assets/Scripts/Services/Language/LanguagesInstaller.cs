using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Services.Languages
{
    public class LanguagesInstaller : GameService
    {
        [SerializeField] private SubOptionSO languageSettings;

        private const string LocalizationFile = "localization.csv";

        private List<string> _languagesAvailable = new();
        
        private Languages _languagesInGame = new ();

        private Localization _localization = new();        
        
        
        private LocalizationDataParser _localizationDataParser = new LocalizationDataParser();
        public override void Install()
        {
            InstallLocalization();

        }

        private void InstallLocalization()
        {
            StartCoroutine(LoadLocalizationFile(LocalizationFile));
        }

        private IEnumerator LoadLocalizationFile(string fileName)
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);

            UnityWebRequest www = UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
//                Debug.Log(www.error);
                Debug.Log("Localization file not found");
            }
            else
            {
                ParseLocalizationData(www.downloadHandler.text);
            }
        }

        private void ParseLocalizationData(string csvData)
        {
            var lines = csvData.Replace("\r", "").Split('\n');
            var headers = lines[0].Split(',').Skip(1).ToArray();
            _localization.LocalizationData = _localizationDataParser.Parse(lines, headers);

            AddLanguagesAvailable(headers);
            
//            AddLocalizations(lines, headers);
            
            ServiceLocator.RegisterService(_localization);
            InstallLanguages();
        }

        private void AddLanguagesAvailable(string[] headers)
        {
            foreach (var field in headers)
            {
                var newLanguage = field.ToLower();
                _languagesAvailable.Add(newLanguage);
            }
        }
        
        private void InstallLanguages()
        {
            for (var i = 0; i < _languagesAvailable.Count; i++)
            {
                var language = _languagesAvailable[i];
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

            InstallLanguagesInServiceLocator();
        }
        
        private bool IsTwoLetterNameIsoValid(string language)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            return cultures.Any(culture => culture.TwoLetterISOLanguageName == language);
        }

        private void InstallLanguagesInServiceLocator()
        {
            ServiceLocator.RegisterService(_languagesInGame);
        }
    }
}