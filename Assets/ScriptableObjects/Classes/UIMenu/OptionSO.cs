using ScriptableObjects.Classes.Ids;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "OptionSO", menuName = "ScriptableObjects/Menu/Create new Option", order = 1)]
    public class OptionSO : ScriptableObject
    {
        [SerializeField] private MenuOptionId optionId;
        [SerializeField] private EventId eventId;
        [SerializeField] private string text;
        [SerializeField] private Sprite icon;

        public string Text => text;

        public EventId EventId => eventId;

        public MenuOptionId OptionId => optionId;

        public Sprite Icon => icon;
    }
}