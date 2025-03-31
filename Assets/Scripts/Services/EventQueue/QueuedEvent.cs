using System;
using Services.EventQueue.Events.Interfaces;

namespace Services.EventQueue
{
    public class QueuedEvent
    {
        public IEventSender EventSender { get; }
        public EventArgs EventArgs { get; }
        
        public QueuedEvent(IEventSender eventSender, EventArgs eventArgs)
        {
            EventSender = eventSender;
            EventArgs = eventArgs;
        }
    }
}