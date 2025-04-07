using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadChecker : MonoBehaviour
{
    private Gamepad _gamepad;
    [SerializeField] private EventId isusingGamepadEventId;

    void Awake()
    {
        _gamepad = InputSystem.GetDevice<Gamepad>();
    }

        
    public bool IsGamepadTouched()
    {
        if (_gamepad == null)
        {
            return false;
        }
        
        return (_gamepad.leftStick.value.y > 0 ||
                _gamepad.leftStick.value.y < 0 ||
                _gamepad.buttonSouth.wasPressedThisFrame ||
                _gamepad.leftTrigger.wasPressedThisFrame ||
                _gamepad.rightTrigger.wasPressedThisFrame ||
                _gamepad.startButton.wasPressedThisFrame ||
                _gamepad.selectButton.wasPressedThisFrame);
    }
    public void SendIsUsingMouseEvent()
    {
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(isusingGamepadEventId, EventArgs.Empty);
    }
}
