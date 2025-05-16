using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputForGamepadStartInGameMenu : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private EventId startButtonPressedMenuInGameEventId;
    
    private void OnEnable()
    {
        var actionMap = inputActions.FindActionMap("UI");

        var cancelActionMap = actionMap.FindAction("GamepadStart");
        cancelActionMap.performed += OnGamepadStartActionPerformed;
        cancelActionMap.Enable();
    }

    private void OnGamepadStartActionPerformed(InputAction.CallbackContext obj)
    {
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(startButtonPressedMenuInGameEventId, EventArgs.Empty);
    }

    private void OnDisable()
    {
        var actionMap = inputActions.FindActionMap("UI");
                
        var cancelActionMap = actionMap.FindAction("GamepadStart");
        cancelActionMap.performed -= OnGamepadStartActionPerformed;
        cancelActionMap.Disable();
    }
}
