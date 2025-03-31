using Services.EventQueue.Events.Classes;
using UnityEngine;

namespace Services.EventQueue.Events.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SendGameObjectEventSO", menuName = "ScriptableObjects/Events/Create SendGameObjectEventSO", order = 4)]
    public class SendGameObjectEventSO : EventSO
    {
        public SendGameObjectEventSO()
        {
            _eventSender = new SendGameObjectEvent();
        }
    }
}