using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SubOptionSO", menuName = "ScriptableObjects/Menu/Create SubOptionSO", order = 2)]
public class SubOptionSO : ScriptableObject
{
    [SerializeField] private MenuSubOptionId subOptionId;
    [SerializeField] private string title;
    [SerializeField] private List<string> allValues;
    [SerializeField] private int defaultValueIndex;

    public MenuSubOptionId SubOptionId => subOptionId;

    public string Title => title;

    public List<string> AllValues => allValues;

    public int DefaultValueIndex => defaultValueIndex;
}
