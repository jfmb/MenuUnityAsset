using System;
using Services.EventQueue.Events.Interfaces;

namespace Services.EventQueue
{
    public class BooleanEvent: IEventSender
    {
        public delegate void BooleanEventHandler(object source, BooleanEventData args);
        public event BooleanEventHandler BooleanEventSender;
        
        public void SendEvent(EventArgs args)
        {
            BooleanEventSender?.Invoke(this, (BooleanEventData) args);
        }
    }
}