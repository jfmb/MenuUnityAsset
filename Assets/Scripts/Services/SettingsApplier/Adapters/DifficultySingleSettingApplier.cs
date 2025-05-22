using UnityEngine;

namespace Services.SettingsApplier.Adapters
{
    public class DifficultySingleSettingApplier: SingleSettingApplier
    {
        public override void Apply()
        {
            var valueSaved = ServiceLocator.GetService<GameInfoFacade>()
                .GetLastSettingsValueSelectedFromKey(SettingsId);
            Debug.Log("My Debug: Difficulty changed to " + valueSaved);
        }
    }
}