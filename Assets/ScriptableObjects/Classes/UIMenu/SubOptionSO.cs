using System.Collections.Generic;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SubOptionSO", menuName = "ScriptableObjects/Menu/Create SubOptionSO", order = 2)]
public class SubOptionSO : ScriptableObject
{
    [SerializeField] private MenuSubOptionId subOptionId;
    [SerializeField] private string title;
    [SerializeField] private Sprite icon;
    [SerializeField] private List<string> allValueLocalizationKeys;
    [SerializeField] private int defaultValueIndex;
    [SerializeField] private EventId simpleEventWhenValueChangesId;

    public MenuSubOptionId SubOptionId => subOptionId;

    public string TitleLocalizationKey => title;

    public List<string> AllValueLocalizationKeysLocalizationKeys => allValueLocalizationKeys;

    public int DefaultValueIndex => defaultValueIndex;

    public Sprite Icon => icon;

    public EventId SimpleEventWhenValueChangesId => simpleEventWhenValueChangesId;
}
