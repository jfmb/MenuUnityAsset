using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputForBackInMenu : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;

    [SerializeField] private EventId gameStartsEventId;
    
    [SerializeField] private EventId backInMenuEventId;

    private bool _isGameStarted;
    
    private void OnEnable()
    {
        var actionMap = inputActions.FindActionMap("UI");

        var cancelActionMap = actionMap.FindAction("Cancel");
        cancelActionMap.performed += OnCancelActionPerformed;
        cancelActionMap.Enable();
        
        var gameStartsEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameStartsEventId);
        gameStartsEvent.SimpleEventSender += OnNewGameStartsEvent;
    }

    private void OnCancelActionPerformed(InputAction.CallbackContext obj)
    {
        Debug.Log("Sending Back event from " + gameObject.name);
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(backInMenuEventId, EventArgs.Empty);
    }

    private void OnNewGameStartsEvent()
    {
        _isGameStarted = true;
    }
    
    private void OnDisable()
    {
        var actionMap = inputActions.FindActionMap("UI");
                
        var cancelActionMap = actionMap.FindAction("Cancel");
        cancelActionMap.performed -= OnCancelActionPerformed;
        cancelActionMap.Disable();
        
        var gameStartsEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameStartsEventId);
        gameStartsEvent.SimpleEventSender -= OnNewGameStartsEvent;
    }
}
