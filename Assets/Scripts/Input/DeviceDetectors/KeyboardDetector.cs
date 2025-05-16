using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardDetector : MonoBehaviour
{
    [SerializeField] private EventId usingKeyboardEventId;

    private Keyboard _keyboard;

    private void Awake()
    {
        _keyboard = InputSystem.GetDevice<Keyboard>();
    }

    public bool IsUsingKeyboard()
    {
        return _keyboard.anyKey.wasPressedThisFrame;
    }

    public void SendIsUsingKeyboardEvent()
    {
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(usingKeyboardEventId, EventArgs.Empty);
    }
}
