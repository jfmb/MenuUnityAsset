using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISubOptionConfigurator : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text value;

    private List<string> _allValues = new ();
    private int _defaultValueIndex;

    private int _currentValue;
    
    public void Setup(SubOptionSO subOptionSo)
    {
        title.text = subOptionSo.Title;
        _defaultValueIndex = subOptionSo.DefaultValueIndex;
        _currentValue = _defaultValueIndex;
        _allValues = subOptionSo.AllValues;
        
        value.text = _allValues[_currentValue];
    }

    public void IncreaseValueFromButton()
    {
        if (_currentValue == _allValues.Count-1)
        {
            return;
        }
        
        _currentValue++;
        value.text = _allValues[_currentValue];
    }

    public void DecreaseValueFromButton()
    {
        if (_currentValue == 0)
        {
            return;
        }
        
        _currentValue--;
        value.text = _allValues[_currentValue];        
    }
}
