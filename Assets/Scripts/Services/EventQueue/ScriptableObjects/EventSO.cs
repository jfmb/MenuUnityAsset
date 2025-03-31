using Services.EventQueue.Events.Interfaces;
using UnityEngine;

namespace Services.EventQueue.Events.ScriptableObjects
{
    public abstract class EventSO : ScriptableObject
    {
        [SerializeField] protected EventId id;

        protected IEventSender _eventSender;

        public EventId Id => id;

        public IEventSender EventSender => _eventSender;
    }
}
