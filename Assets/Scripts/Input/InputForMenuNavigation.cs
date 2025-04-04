using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputForMenuNavigation : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    
    [SerializeField] private EventId uiLeftEventId;
    [SerializeField] private EventId uiRightEventId;
    [SerializeField] private EventId usingMouseEventId;

    private InputActionMap _actionMap;

    private void Start()
    {
        _actionMap = inputActions.FindActionMap("UI");

        var navigateActionMap = _actionMap.FindAction("Navigate");
        navigateActionMap.performed += PerformNavigation;
        navigateActionMap.Enable();

        // var clickAction = _actionMap.FindAction("Click");
        // clickAction.performed += PerformClick;
        // clickAction.Enable();
    }
    //
    // private void PerformClick(InputAction.CallbackContext obj)
    // {
    //     if (obj.control.device is Mouse)
    //     {
    //         SendEventIsUsingMouse(true);
    //         return;
    //     }
    //
    //     SendEventIsUsingMouse(false);
    // }

    private void SendEventIsUsingMouse(bool isUsingMouse)
    {
        var args = new BooleanEventData(isUsingMouse);
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(usingMouseEventId, args);
        Debug.Log("IsUsingMouse event sent");
    }

    private void PerformNavigation(InputAction.CallbackContext ctx)
    {
        var direction = ctx.ReadValue<Vector2>();
        
        if (direction.x == -1)
        {
            Debug.Log("Joystick Left");
            DoThingsWhenInputIsLeft();
        }
        
        if (direction.x == 1)
        {
            Debug.Log("Joystick Right");
            DoThingsWhenInputIsRight();
        }
    }

    private void DoThingsWhenInputIsLeft()
    {
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(uiLeftEventId, EventArgs.Empty);
    }

    private void DoThingsWhenInputIsRight()
    {
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(uiRightEventId, EventArgs.Empty);
    }
    
    private void OnDisable()
    {
        var navigateAction = _actionMap.FindAction("Navigate");
        navigateAction.performed -= PerformNavigation;
        navigateAction.Disable();
        
        // var clickAction = _actionMap.FindAction("Click");
        // clickAction.performed -= PerformClick;
        // clickAction.Disable();
    }
}
