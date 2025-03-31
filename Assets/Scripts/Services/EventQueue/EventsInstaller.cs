
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;

public class EventsInstaller : MonoBehaviour
{
    [SerializeField] private EventSO[] eventsToInstall;

    public void Install()
    {
        foreach (var newEvent in eventsToInstall)
        {
            ServiceLocator.GetService<EventQueue>().AddNewEventSender(newEvent.Id, newEvent.EventSender);
        }
    }
}
