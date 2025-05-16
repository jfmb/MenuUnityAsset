using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardChecker : MonoBehaviour
{
    [SerializeField] private EventId isusingKeyboardEventId;

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
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(isusingKeyboardEventId, EventArgs.Empty);
    }
}
