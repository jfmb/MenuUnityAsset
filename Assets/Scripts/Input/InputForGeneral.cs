using System;
using ScriptableObjects.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputForGeneral : MonoBehaviour
{
    [SerializeField] private EventId gameStartsEventId;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private EventId settingsInGameClosedEventId;
    [SerializeField] private GameObject mainMenuInputListener;
    [SerializeField] private GameObject playerInputListener;
    [SerializeField] private EventId menuToEnableEventId;
    [SerializeField] private MenuId menuToEnableId;
    
    private InputActionMap _actionMap;

    private bool _areSettingsEnabled;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += DeviceChange;
    }

    private void Start()
    {
        SetupGeneralInput();
        
        mainMenuInputListener.SetActive(true);
        playerInputListener.SetActive(false);

        var gameStartsEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameStartsEventId);
        gameStartsEvent.SimpleEventSender += OnNewGameStartsEvent;

        var settingsInGameClosedEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(settingsInGameClosedEventId);
        settingsInGameClosedEvent.SimpleEventSender += OnNewSettingsInGameClosedEvent;
    }

    private void OnNewSettingsInGameClosedEvent()
    {
        OpenOrCloseSettings();
    }

    private void OnNewGameStartsEvent()
    {
        SetupGeneralInput();
        playerInputListener.SetActive(true);
        mainMenuInputListener.SetActive(false);
        
        Debug.Log("Game starts!!!!");
    }


    private void DeviceChange(InputDevice device, InputDeviceChange change)
    {
//         if (change == InputDeviceChange.Added)
//         {
// //            SubscribeToEvents();
//             Debug.Log("Device Connected: " + device);
//         }
//         else if (change == InputDeviceChange.Removed)
//         {
// //            UnsubscribeToEvents();
//             Debug.Log("Device Disconnected: " + device);
//         }
    }
    
    private void SetupGeneralInput()
    {
        var generalActionMap = inputActions.FindActionMap("General");
        var navigateAction = generalActionMap.FindAction("Settings");

        navigateAction.performed += PerformSettings;

        navigateAction.Enable();
    }
    private void PerformSettings(InputAction.CallbackContext obj)
    {
        OpenOrCloseSettings();
    }

    private void OpenOrCloseSettings()
    {
        mainMenuInputListener.SetActive(!mainMenuInputListener.activeSelf);
        playerInputListener.SetActive(!playerInputListener.activeSelf);

        if (mainMenuInputListener.activeSelf)
        {
            var args = new StringEventData(menuToEnableId.Id);
            ServiceLocator.GetService<EventQueue>().EnqueueEvent(menuToEnableEventId, args);
        }
        else
        {
            var args = new StringEventData("");
            ServiceLocator.GetService<EventQueue>().EnqueueEvent(menuToEnableEventId, args);
        }

    }
    
    private void OnDisable()
    {
        InputSystem.onDeviceChange -= DeviceChange;
        
        var generalActionMap = inputActions.FindActionMap("General");
        var navigateAction = generalActionMap.FindAction("Settings");

        navigateAction.performed -= PerformSettings;

        navigateAction.Disable();
    }

    private void OnDestroy()
    {
        var gameStartsEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameStartsEventId);
        gameStartsEvent.SimpleEventSender -= OnNewGameStartsEvent;
    }
}
