using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISubOptionConfigurator : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text value;
    [SerializeField] private Image icon;

    private List<string> _allValues = new ();
    private int _defaultValueIndex;

    private int _currentValue;
    
    public void Setup(SubOptionSO subOptionSo)
    {
        title.text = subOptionSo.Title;

        icon.sprite = subOptionSo.Icon;
        
        _defaultValueIndex = subOptionSo.DefaultValueIndex;
        _currentValue = _defaultValueIndex;
        _allValues = subOptionSo.AllValues;
        
        value.text = _allValues[_currentValue];
    }

    public void IncreaseValueFromButton()
    {
        _currentValue++;
        if (_currentValue == _allValues.Count)
        {
            _currentValue = 0;
        }
        
        value.text = _allValues[_currentValue];
    }

    public void DecreaseValueFromButton()
    {
        _currentValue--;
        if (_currentValue < 0)
        {
            _currentValue = _allValues.Count-1;
        }
        
        value.text = _allValues[_currentValue];        
    }
}
