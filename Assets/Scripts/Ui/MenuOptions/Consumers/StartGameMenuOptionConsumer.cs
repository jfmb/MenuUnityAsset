using System;
using ScriptableObjects.Scripts.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Ui.MenuOptions.Interfaces;
using UnityEngine;

namespace Ui.MenuOptions.Consumers
{
    public class StartGameMenuOptionConsumer : MenuOption
    {
        [SerializeField] private EventId loadSceneEventId;
        [SerializeField] private SceneId nextSceneId;
        
        public override void Execute()
        {
            StartNextScene();
        }
        
        private void StartNextScene()
        {
            var args = new StringEventData(nextSceneId.Id);
            Debug.Log("before enqueue");
            ServiceLocator.GetService<EventQueue>().EnqueueEvent(loadSceneEventId, args);
        }
    }
}