using ScriptableObjects.Scripts.Ids;
using Services.EventQueue;
using Services.EventQueue.Classes;
using Services.EventQueue.Classes.EventData;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace.Services.Consumers
{
    public class SceneLoaderConsumer : MonoBehaviour
    {
        [SerializeField] private EventId sceneLoaderEventId;

        // private void Start()
        // {
        //     var sceneLoaderEvent =
        //         (IntegerEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(sceneLoaderEventId);
        //     sceneLoaderEvent.IntegerEventSender += OnNewSceneLoaderEvent;
        // }
        //
        // private void OnNewSceneLoaderEvent(object source, IntegerEventData args)
        // {
        //     SceneManager.LoadScene(args.Value);
        // }
        //
        // private void OnDisable()
        // {
        //     var sceneLoaderEvent =
        //         (IntegerEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(sceneLoaderEventId);
        //     sceneLoaderEvent.IntegerEventSender -= OnNewSceneLoaderEvent;
        // }
    }
}