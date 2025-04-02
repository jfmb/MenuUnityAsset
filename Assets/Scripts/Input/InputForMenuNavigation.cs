using System;
using System.Runtime.CompilerServices;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputForMenuNavigation : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    
    [SerializeField] private EventId uiUpEventId;
    [SerializeField] private EventId uiDownEventId;
    [SerializeField] private EventId uiSelectEventId;

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

    private void PerformClick(InputAction.CallbackContext obj)
    {
            DoThingsWhenClic();
    }

    private void DoThingsWhenClic()
    {
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(uiSelectEventId, EventArgs.Empty);
        Debug.Log("Select event received");
    }

    private void PerformNavigation(InputAction.CallbackContext ctx)
    {
        var direction = ctx.ReadValue<Vector2>();
        if (direction.y == 1)
        {
            DoThingsWhenInputIsUp();
        }

        if (direction.y == -1)
        {
            DoThingsWhenInputIsDown();
        }
    }

    private void DoThingsWhenInputIsUp()
    {
//        ServiceLocator.GetService<EventQueue>().EnqueueEvent(uiUpEventId, EventArgs.Empty);
    }

    private void DoThingsWhenInputIsDown()
    {
//        ServiceLocator.GetService<EventQueue>().EnqueueEvent(uiDownEventId, EventArgs.Empty);
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
