using Services.EventQueue.Classes;
using UnityEngine;

namespace Services.EventQueue.Events.ScriptableObjects
{
    [CreateAssetMenu(fileName = "integerEventSO", menuName = "ScriptableObjects/Events/Create IntegerEventSO", order = 5)]
    public class IntegerEventSO : EventSO
    {
        public IntegerEventSO()
        {
            _eventSender = new IntegerEvent();
        }
    }
}