using System;
using NUnit.Framework;
using ScriptableObjects.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Ui.MenuOptions.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.MenuOptions.Consumers
{
    public class BackOptionConsumer: MenuOption
    {
        [SerializeField] private EventId backInMenuEventId;
        
        public override void Execute()
        {

            Assert.IsNotNull(backInMenuEventId, "backInMenuEventId can't be null");
            ServiceLocator.GetService<EventQueue>().EnqueueEvent(backInMenuEventId, EventArgs.Empty);
        }
    }
}