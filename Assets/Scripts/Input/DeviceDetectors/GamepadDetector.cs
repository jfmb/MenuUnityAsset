using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadDetector : MonoBehaviour
{
    [SerializeField] private EventId isGamepadConectedId;
    [SerializeField] private EventId usingGamepadEventId;

    private Gamepad _gamepad;

    void Awake()
    {
        _gamepad = InputSystem.GetDevice<Gamepad>();
        InputSystem.onDeviceChange += DeviceChange;
    }

    public bool IsGamepadConnected()
    {
        return _gamepad != null;
    }

    private void DeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Added when device is Gamepad:
            {
                var args = new BooleanEventData(true);
                _gamepad = InputSystem.GetDevice<Gamepad>();
                ServiceLocator.GetService<EventQueue>().EnqueueEvent(isGamepadConectedId, args);
                break;
            }
            case InputDeviceChange.Removed:
            {
                var args = new BooleanEventData(false);
                ServiceLocator.GetService<EventQueue>().EnqueueEvent(isGamepadConectedId, args);
                _gamepad = null;
                break;
            }
        }
    }
        
    public bool IsGamepadTouched()
    {
        if (_gamepad == null)
        {
            Debug.Log("Gamepad is null...");
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
    public void SendIsUsingGamepadEvent()
    {
        Debug.Log("Gamepad is detected, sending event...");
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(usingGamepadEventId, EventArgs.Empty);
    }

    public void OnDestroy()
    {
        InputSystem.onDeviceChange -= DeviceChange;
    }
}
