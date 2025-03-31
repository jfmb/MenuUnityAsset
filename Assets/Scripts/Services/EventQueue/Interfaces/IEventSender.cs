using System;

namespace Services.EventQueue.Events.Interfaces
{
    public interface IEventSender
    {
        public void SendEvent(EventArgs args);
    }
}