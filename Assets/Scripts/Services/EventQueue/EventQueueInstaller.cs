using Services.EventQueue;
using UnityEngine;

public class EventQueueInstaller : GameService
{
    [SerializeField] private EventQueue eventQueue;

    public override void Install()
    {
        DontDestroyOnLoad(eventQueue.gameObject);
        ServiceLocator.RegisterService(eventQueue);
    }
}
