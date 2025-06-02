using UnityEngine;

namespace Services.SettingsApplier
{
    public abstract class SingleSettingApplier: MonoBehaviour
    {
        [SerializeField] private MenuSubOptionId settingsId;

        public string SettingsId => settingsId.Id;

        public abstract void Apply();
    }
}