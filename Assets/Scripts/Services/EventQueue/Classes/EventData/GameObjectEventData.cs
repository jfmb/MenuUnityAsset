using System;
using UnityEngine;

namespace Services.EventQueue.Events.EventData
{
    public class GameObjectEventData: EventArgs
    {
        public GameObject _gameObject { get; }

        public GameObjectEventData(GameObject newGameObject)
        {
            _gameObject = newGameObject;
        }
    }
}