using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISubOptionConfigurator : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text value;
    [SerializeField] private Image icon;

    private string _subOptionId = "";
    private List<string> _allValues = new ();
    private int _defaultValueIndex;

    private int _currentValue;

    public int CurrentValue => _currentValue;

    public string SubOptionId => _subOptionId;

    public void Setup(SubOptionSO subOptionSo)
    {
        _subOptionId = subOptionSo.SubOptionId.Id;
        title.text = subOptionSo.Title;

        icon.sprite = subOptionSo.Icon;
        
        _defaultValueIndex = subOptionSo.DefaultValueIndex;

        SetCurrentValue();
        _allValues = subOptionSo.AllValues;
        
        value.text = _allValues[CurrentValue];
    }

    private void SetCurrentValue()
    {
        var lastValueSaved = ServiceLocator.GetService<GameInfoFacade>()
            .GetLastSettingsValueSelectedFromKey(SubOptionId);
        if (lastValueSaved < 0)
        {
            _currentValue = _defaultValueIndex;
            return;
        }

        _currentValue = lastValueSaved;
    }

    public void IncreaseValueFromButton()
    {
        _currentValue = CurrentValue + 1;
        if (CurrentValue == _allValues.Count)
        {
            _currentValue = 0;
        }
        
        value.text = _allValues[CurrentValue];
    }

    public void DecreaseValueFromButton()
    {
        _currentValue = CurrentValue - 1;
        if (CurrentValue < 0)
        {
            _currentValue = _allValues.Count-1;
        }
        
        value.text = _allValues[CurrentValue];        
    }

    public void SaveCurrentValuePermanent()
    {
        ServiceLocator.GetService<GameInfoFacade>().SaveSettingNewValueWithKey(_subOptionId, _currentValue);
    }
}
