using System;
using Services.EventQueue.Events.Interfaces;
using UnityEngine.SocialPlatforms;

namespace Services.EventQueue.Events.ScriptableObjects
{
    public class StringEvent: IEventSender
    {
        public delegate void StringEventHandler(object source, StringEventData args);
        public event StringEventHandler StringEventSender;
        
        public void SendEvent(EventArgs args)
        {
            StringEventSender?.Invoke(this, (StringEventData) args);
        }
    }
}