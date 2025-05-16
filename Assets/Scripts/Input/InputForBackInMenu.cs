using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputForBackInMenu : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    [SerializeField] private EventId backInMenuEventId;
   
    private void OnEnable()
    {
        var actionMap = inputActions.FindActionMap("UI");

        var cancelActionMap = actionMap.FindAction("Cancel");
        cancelActionMap.performed += OnCancelActionPerformed;
        cancelActionMap.Enable();
    }

    private void OnCancelActionPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Sending Back event from " + gameObject.name);
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(backInMenuEventId, EventArgs.Empty);
    }
    
    private void OnDisable()
    {
        var actionMap = inputActions.FindActionMap("UI");
                
        var cancelActionMap = actionMap.FindAction("Cancel");
        cancelActionMap.performed -= OnCancelActionPerformed;
        cancelActionMap.Disable();
    }
}
