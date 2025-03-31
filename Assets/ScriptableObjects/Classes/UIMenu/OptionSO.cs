using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "OptionSO", menuName = "ScriptableObjects/Menu/Create new Option", order = 1)]
    public class OptionSO : ScriptableObject
    {
        [SerializeField] private string text;
        [SerializeField] private string eventName;

        public string Text => text;

        public string EventName => eventName;
    }
}