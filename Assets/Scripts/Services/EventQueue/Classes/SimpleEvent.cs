using System;
using Services.EventQueue.Events.Interfaces;

namespace Services.EventQueue
{
//    public abstract class SimpleEvent: IEventSender
    public class SimpleEvent: IEventSender
    {
        public delegate void GenericSimpleEventHandler();
        public event GenericSimpleEventHandler SimpleEventSender;

        public void SendEvent(EventArgs args)
        {
            SimpleEventSender?.Invoke();
        }
        
    }
}