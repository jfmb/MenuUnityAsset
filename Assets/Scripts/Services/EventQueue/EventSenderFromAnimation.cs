using System;
using System.Collections;
using System.Collections.Generic;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;

public class EventSenderFromAnimation : MonoBehaviour
{
    [SerializeField] private EventId simpleEventId;

    public void SendSimpleEventFromAnimation()
    {
        Debug.Log("Sending start countdown event");
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(simpleEventId, EventArgs.Empty);
    }
}
