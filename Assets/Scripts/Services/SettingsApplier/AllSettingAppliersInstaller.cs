using System.Collections.Generic;
using UnityEngine;

namespace Services.SettingsApplier
{
    public class AllSettingAppliersInstaller : GameService
    {
        [SerializeField] private List<SingleSettingApplier> allSingleSettingsAppliers;

        private Dictionary<string, SingleSettingApplier> _allSettings = new ();
        
        public override void Install()
        {
            CreateAllSettingAppliers();
            
            DontDestroyOnLoad(this);
            ServiceLocator.RegisterService(this);
        }

        private void CreateAllSettingAppliers()
        {
            foreach (var settingApplier in allSingleSettingsAppliers)
            {
                _allSettings.Add(settingApplier.SettingsId, settingApplier);
                ApplySettingWith(settingApplier.SettingsId);
            }
        }

        public void ApplySettingWith(string key)
        {
            if (!_allSettings.ContainsKey(key))
            {
                Debug.Log("Settings with key not present in dictionary");
                return;
            }
            
            _allSettings[key].Apply();
        }
    }
}