using Services.EventQueue;
using UnityEngine;

public class EventQueueInstaller : MonoBehaviour
{
    [SerializeField] private EventQueue eventQueue;

    public void Install()
    {
        DontDestroyOnLoad(eventQueue.gameObject);
        ServiceLocator.RegisterService(eventQueue);
    }
}
