using ScriptableObjects.Scripts.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.Services.Consumers
{
    public class SceneLoaderConsumer : MonoBehaviour
    {
        [SerializeField] private EventId sceneLoaderEventId;
        private SceneId _sceneId;

        private void Start()
        {
            var sceneLoaderEvent =
                (StringEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(sceneLoaderEventId);
            sceneLoaderEvent.StringEventSender += OnNewSceneLoaderEvent;
        }

        private void OnNewSceneLoaderEvent(object source, StringEventData args)
        {
            SceneManager.LoadScene(args.Value);
        }

        private void OnDisable()
        {
            var sceneLoaderEvent =
                (StringEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(sceneLoaderEventId);
            sceneLoaderEvent.StringEventSender -= OnNewSceneLoaderEvent;
        }
    }
}