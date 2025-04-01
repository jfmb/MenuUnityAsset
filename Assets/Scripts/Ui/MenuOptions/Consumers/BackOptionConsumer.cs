using ScriptableObjects.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Ui.MenuOptions.Interfaces;
using UnityEngine;

namespace Ui.MenuOptions.Consumers
{
    public class BackOptionConsumer: MenuOption
    {
        [SerializeField] private MenuId menuOptionToEnableId;
        [SerializeField] private EventId menuOptionToEnableEventId;

        public override void Execute()
        {
            var args = new StringEventData(menuOptionToEnableId.Id);
            ServiceLocator.GetService<EventQueue>().EnqueueEvent(menuOptionToEnableEventId, args);
        }
    }
}