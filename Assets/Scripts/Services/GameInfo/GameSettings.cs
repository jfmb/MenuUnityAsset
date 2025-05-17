using System.Collections.Generic;

public class GameSettings
{
    public Dictionary<string, int> Settings { get; set; } = new();

   
    public void SetupSettings(List<SubOptionSO> allSettings)
    {
        FillWithDefaultValue(allSettings);
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
}
