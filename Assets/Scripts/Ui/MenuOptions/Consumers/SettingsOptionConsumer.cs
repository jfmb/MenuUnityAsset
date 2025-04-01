using ScriptableObjects.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Ui.MenuOptions.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

public class SettingsOptionConsumer : MenuOption
{
    [SerializeField] private EventId menuToEnableEventId;
    [SerializeField] private MenuId menuToEnableId;

    public override void Execute()
    {
        var args = new StringEventData(menuToEnableId.Id);
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(menuToEnableEventId, args);
    }
}
