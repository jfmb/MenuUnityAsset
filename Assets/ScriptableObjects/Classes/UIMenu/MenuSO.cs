using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MenuSO", menuName = "ScriptableObjects/Menu/Create MenuSO", order = 0)]
    public class MenuSO : ScriptableObject
    {
        [SerializeField] private List<OptionSO> allOptions;

        public List<OptionSO> AllOptions => allOptions;
    }
}