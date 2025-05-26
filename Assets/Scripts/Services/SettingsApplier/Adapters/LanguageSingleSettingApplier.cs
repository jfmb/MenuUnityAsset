using Services.Languages;
using Services.SettingsApplier;
using UnityEngine;

public class LanguageSingleSettingApplier : SingleSettingApplier
{
    [SerializeField] private SubOptionSO languageSubOptionSO;
    
    public override void Apply()
    {            
        var valueSaved = ServiceLocator.GetService<GameInfoFacade>()
            .GetLastSettingsValueSelectedFromKey(SettingsId);

        var twoCodeLanguage = languageSubOptionSO.AllValues[valueSaved];
        ServiceLocator.GetService<Languages>().SetCurrentLanguage(twoCodeLanguage);
        //TODO: send event to inform the new language saved
    }
}
