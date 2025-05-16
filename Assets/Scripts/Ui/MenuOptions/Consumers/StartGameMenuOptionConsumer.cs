using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Ui.MenuOptions.Interfaces;
using UnityEngine;

namespace Ui.MenuOptions.Consumers
{
    public class StartGameMenuOptionConsumer : MenuOption
    {
        [SerializeField] private EventId gameStartsEventId;
        
        public override void Execute()
        {
            StartGame();
        }
        
        private void StartGame()
        {
            ServiceLocator.GetService<EventQueue>().EnqueueEvent(gameStartsEventId, EventArgs.Empty);
        }
    }
}