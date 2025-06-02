using System;
using System.Collections.Generic;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Services.Languages;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UISubOptionCreator : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text value;
    [SerializeField] private Image icon;

    private List<string> _allValuesLocalized = new ();

    private int _currentValue;
    private SubOptionSO _subOptionSo;

    public int CurrentValue => _currentValue;

    public string SubOptionId => _subOptionSo.SubOptionId.Id;
    
    public void Setup(SubOptionSO subOptionSo)
    {
        _subOptionSo = subOptionSo;

        icon.sprite = subOptionSo.Icon;
        
        LocalizeTitle();
        LocalizeValues();
        SetCurrentValue();
        
        value.text = _allValuesLocalized[CurrentValue];
    }

    public void LocalizeTitle()
    {
        if (!_subOptionSo)
        {
            return;
        }
        
        var titleKey = _subOptionSo.TitleLocalizationKey;
        var localizedText = ServiceLocator.GetService<Localization>().GetLocalizedText(titleKey);
        title.text = localizedText;
    }

    public void LocalizeValues()
    {
        if (!_subOptionSo)
        {
            return;
        }

        _allValuesLocalized.Clear();
        
        foreach (var key in _subOptionSo.AllValueLocalizationKeysLocalizationKeys)
        {
            var valueLocalized = ServiceLocator.GetService<Localization>().GetLocalizedText(key);

            _allValuesLocalized.Add(valueLocalized);
        }
        value.text = _allValuesLocalized[CurrentValue];
    }

    private void SetCurrentValue()
    {
        var lastValueSaved = ServiceLocator.GetService<GameInfoFacade>()
            .GetLastSettingsValueSelectedFromKey(SubOptionId);
        if (lastValueSaved < 0)
        {
            _currentValue = _subOptionSo.DefaultValueIndex;
            return;
        }

        _currentValue = lastValueSaved;
    }

    public void IncreaseValueFromButton()
    {
        _currentValue = CurrentValue + 1;
        if (CurrentValue == _allValuesLocalized.Count)
        {
            _currentValue = 0;
        }
        
        value.text = _allValuesLocalized[CurrentValue];
        SendEventIfNecessary();
    }

    private void SendEventIfNecessary()
    {
        var simpleEventWhenValueChangesId = _subOptionSo.SimpleEventWhenValueChangesId;
        
        if (!simpleEventWhenValueChangesId)
        {
            return;
        }
        
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(simpleEventWhenValueChangesId, EventArgs.Empty);
    }

    public void DecreaseValueFromButton()
    {
        _currentValue = CurrentValue - 1;
        if (CurrentValue < 0)
        {
            _currentValue = _allValuesLocalized.Count-1;
        }
        
        value.text = _allValuesLocalized[CurrentValue];
        SendEventIfNecessary();
    }

    public void SaveCurrentValuePermanent()
    {
        ServiceLocator.GetService<GameInfoFacade>().SaveSettingNewValueWithKey(SubOptionId, _currentValue);
    }
}
