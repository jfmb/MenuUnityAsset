using System.Collections.Generic;
using ScriptableObjects.Ids;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MenuSO", menuName = "ScriptableObjects/Menu/Create MenuSO", order = 0)]
    public class MenuSO : ScriptableObject
    {
        [SerializeField] private MenuId menuId;
        [SerializeField] private List<OptionSO> allOptions;
        [SerializeField] private List<SubOptionSO> allSubOptions;

        public MenuId Id => menuId;
        public List<OptionSO> AllOptions => allOptions;

        public List<SubOptionSO> AllSubOptions => allSubOptions;
    }
}