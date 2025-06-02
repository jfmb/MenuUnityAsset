using System.Collections.Generic;
using Services.SettingsApplier;
using UnityEngine;

public class GameSettings
{
    public Dictionary<string, int> Settings { get; set; } = new();
   
    public void SetupSettings(List<SubOptionSO> allSettings)
    {
        FillWithDefaultValue(allSettings);
        Debug.Log("My Debug: Dictionary filled with default values");
        FillWithPermanentDataSavedValue();
    }

    private void FillWithDefaultValue(List<SubOptionSO> allSettings)
    {
        foreach (var element in allSettings)
        {
            Settings.Add(element.SubOptionId.Id, element.DefaultValueIndex);
        }
    }

    private void FillWithPermanentDataSavedValue()
    {
        ServiceLocator.GetService<IPermanentData>().LoadGroupOfData(Settings);
    }

    public void SaveNewSettingsValueWithKey(string key, int newValue)
    {
        if (Settings.ContainsKey(key))
        {
            if (Settings[key] == newValue)
            {
                return;
            }
            Settings[key] = newValue;
            SavePermanentValueWithKey(key);    
            ApplySettingWithKey(key);
            return;
        }
        
        Settings.Add(key, newValue);
        SavePermanentValueWithKey(key);
        ApplySettingWithKey(key);
    }
    
    private void SavePermanentValueWithKey(string key)
    {
        var value = Settings[key];
        ServiceLocator.GetService<IPermanentData>().SaveSingleData(key, value);
    }

    private static void ApplySettingWithKey(string key)
    {
        ServiceLocator.GetService<AllSettingAppliersInstaller>().ApplySettingWith(key);
    }
}
