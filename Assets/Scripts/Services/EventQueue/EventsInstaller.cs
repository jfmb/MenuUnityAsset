using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;

public class EventsInstaller : GameService
{
    [SerializeField] private EventSO[] eventsToInstall;

    public override void Install()
    {
        foreach (var newEvent in eventsToInstall)
        {
            ServiceLocator.GetService<EventQueue>().AddNewEventSender(newEvent.Id, newEvent.EventSender);
        }

        IsInstallationDone = true;
    }
}
