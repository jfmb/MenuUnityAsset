using System;
using Services.EventQueue.Events.EventData;
using Services.EventQueue.Events.Interfaces;

namespace Services.EventQueue.Events.Classes
{
    public class SendGameObjectEvent: IEventSender
    {
        public delegate void SendGameObjectEventHandler(object source, GameObjectEventData args);

        public event SendGameObjectEventHandler SendGameObjectEventSender;
        public void SendEvent(EventArgs args)
        {
            SendGameObjectEventSender?.Invoke(this, (GameObjectEventData) args);
        }
    }
}