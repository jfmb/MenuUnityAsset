using System;
using System.Collections.Generic;
using Services.EventQueue.Events.Interfaces;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;

namespace Services.EventQueue
{
    public class EventQueue: MonoBehaviour
    {
//        private readonly Dictionary<EventId, IEventSender> _fromEventIdToEventSender = new Dictionary<EventId, IEventSender>();
        private readonly Dictionary<string, IEventSender> _fromEventIdToEventSender = new Dictionary<string, IEventSender>();

        private Queue<QueuedEvent> _currentEvents = new Queue<QueuedEvent>();
        private Queue<QueuedEvent> _nextEvents = new Queue<QueuedEvent>();

        public void AddNewEventSender(EventId eventId, IEventSender eventSender)
        {
//            Debug.Log("New event added with id: " + eventId);
            if (_fromEventIdToEventSender.ContainsKey(eventId.Id))
            {
                return;
            }
            _fromEventIdToEventSender.Add(eventId.Id, eventSender);
        }

        public void RemoveEventSenderWithId(EventId eventId)
        {
            _fromEventIdToEventSender.Remove(eventId.Id);
        }
        
        public IEventSender GetEventWithEventId(EventId eventId)
        {
            // if (!_fromEventIdToEventSender.ContainsKey(eventId.Id))
            // {
            //     Debug.Log(eventId.Id + " is not installed");
            //     return null;
            // }
            // return _fromEventIdToEventSender[eventId.Id];

            return GetEventWithString(eventId.Id);
        }

        public IEventSender GetEventWithString(string id)
        {
            if (!_fromEventIdToEventSender.ContainsKey(id))
            {
                Debug.Log(id + " is not installed");
                return null;
            }
            return _fromEventIdToEventSender[id];            
        }
        
        public void EnqueueEvent(EventId eventId, EventArgs eventArgs)
        {
            // var eventSender = _fromEventIdToEventSender[eventId.Id];
            //
            // var queuedEvent = new QueuedEvent(eventSender, eventArgs);
            // _nextEvents.Enqueue(queuedEvent);
            EnqueueEvent(eventId.Id, eventArgs);
        }

        public void EnqueueEvent(string id, EventArgs eventArgs)
        {
            var eventSender = _fromEventIdToEventSender[id];
            
            var queuedEvent = new QueuedEvent(eventSender, eventArgs);
            _nextEvents.Enqueue(queuedEvent);
        }
        
        private void LateUpdate()
        {
            ProcessEvents();
        }

        private void ProcessEvents()
        {
            (_currentEvents, _nextEvents) = (_nextEvents, _currentEvents);

            foreach (var currentEvent in _currentEvents)
            {
                Debug.Log("Sending current event... " + currentEvent.EventSender);
                currentEvent.EventSender.SendEvent(currentEvent.EventArgs);
            }
            
            _currentEvents.Clear();
        }
    }
}