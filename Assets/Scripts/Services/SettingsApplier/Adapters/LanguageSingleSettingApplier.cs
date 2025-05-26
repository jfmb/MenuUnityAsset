using System;
using System.Globalization;
using System.Linq;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Services.Languages;
using Services.SettingsApplier;
using UnityEditor.Rendering;
using UnityEngine;

public class LanguageSingleSettingApplier : SingleSettingApplier
{
    [SerializeField] private SubOptionSO languageSubOptionSO;
    [SerializeField] private EventId localizeNowEventId;

    private string _currentTwoLettersCultureCode;

    public override void Apply()
    {
        var currentTwoCodeLanguage = ServiceLocator.GetService<Languages>().TwoLettersCodeForCurrentLanguage;
//        var currentCulture = new CultureInfo(currentTwoCodeLanguage);
        
        var valueSaved = ServiceLocator.GetService<GameInfoFacade>()
            .GetLastSettingsValueSelectedFromKey(SettingsId);

        var newLanguageName = languageSubOptionSO.AllValueLocalizationKeysLocalizationKeys[valueSaved];
        Debug.Log("Index: " + valueSaved  +"Native language name: " + newLanguageName);
        var culture = CultureInfo.GetCultures(CultureTypes.AllCultures)
            .FirstOrDefault(c => c.EnglishName.ToLower() == newLanguageName.ToLower());
        
//        var twoCodeLanguage = languageSubOptionSO.AllValues[valueSaved];
        var newTwoCodeLanguage = culture.TwoLetterISOLanguageName;
            ServiceLocator.GetService<Languages>().SetCurrentLanguage(newTwoCodeLanguage);
        
        Debug.Log("Current TwoCodeLanguage loaded in settings: " + newTwoCodeLanguage);
        
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(localizeNowEventId.Id, EventArgs.Empty);
    }
}
