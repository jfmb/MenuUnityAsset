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
        InputSystem.onDeviceChange += DeviceChange;
    }

    private void DeviceChange(InputDevice device, InputDeviceChange change)
    {
        
        if (change == InputDeviceChange.Added)
        {
            SendIsUsingGamepadEvent();
        }
        else if (change == InputDeviceChange.Removed)
        {
//            UnsubscribeToEvents();
        }
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
    public void SendIsUsingGamepadEvent()
    {
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(isusingGamepadEventId, EventArgs.Empty);
    }

    public void OnDestroy()
    {
        InputSystem.onDeviceChange -= DeviceChange;
    }
}
