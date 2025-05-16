using System;
using Services.EventQueue.Classes.EventData;
using Services.EventQueue.Events.Interfaces;

namespace Services.EventQueue.Classes
{
    public class IntegerEvent: IEventSender
    {
        public delegate void IntegerEventHandler(object source, IntegerEventData args);
        public event IntegerEventHandler IntegerEventSender;
    
        public void SendEvent(EventArgs args)
        {
            IntegerEventSender?.Invoke(this, (IntegerEventData) args);
        }
    }
}