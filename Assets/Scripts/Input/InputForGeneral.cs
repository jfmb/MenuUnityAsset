using ScriptableObjects.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputForGeneral : MonoBehaviour
{
    [SerializeField] private EventId gameStartsEventId;
    [SerializeField] private EventId gameContinuesEventId;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private GameObject uiMenuInputListener;
    [SerializeField] private GameObject playerInputListener;
    [SerializeField] private EventId menuToEnableEventId;
    [SerializeField] private MenuId menuToEnableId;
    
    private InputActionMap _actionMap;

    private bool _areSettingsEnabled;

    private bool _isGameStarted;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += DeviceChange;
    }

    private void Start()
    {
        uiMenuInputListener.SetActive(true);
        playerInputListener.SetActive(false);

        var gameStartsEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameStartsEventId);
        gameStartsEvent.SimpleEventSender += OnNewGameStartsEvent;

        var gameContinuesEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameContinuesEventId);
        gameContinuesEvent.SimpleEventSender += OnNewGameContinuesEvent;
    }

    private void OnNewGameContinuesEvent()
    {
        playerInputListener.SetActive(true);
        uiMenuInputListener.SetActive(false);
    }

    private void OnNewGameStartsEvent()
    {
        SetupGeneralInput();

        playerInputListener.SetActive(true);
        uiMenuInputListener.SetActive(false);

        _isGameStarted = true;
        Debug.Log("Game starts!!!!");
    }
    
    private void SetupGeneralInput()
    {
        var generalActionMap = inputActions.FindActionMap("General");
        var navigateAction = generalActionMap.FindAction("Settings");

        navigateAction.performed += PerformSettings;

        navigateAction.Enable();
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
    

    private void PerformSettings(InputAction.CallbackContext obj)
    {
        if (!_isGameStarted)
        {
            return;
        }
        OpenMenuInGame();
    }

    private void OpenMenuInGame()
    {
        // mainMenuInputListener.SetActive(!mainMenuInputListener.activeSelf);
        // playerInputListener.SetActive(!playerInputListener.activeSelf);

        if (IsUIAlreadyActive())
        {
            return;
        }
        
        uiMenuInputListener.SetActive(true);
        playerInputListener.SetActive(false);

        SendEventToEnableMenuInGame();
        // if (mainMenuInputListener.activeSelf)
        // {
        //     SendEventToEnableMenuInGame();
        // }
        // else
        // {
        //     SendEventToDisableMainMenu();
        // }
    }

    private bool IsUIAlreadyActive()
    {
        return uiMenuInputListener.activeSelf;
    }
    
    private void SendEventToEnableMenuInGame()
    {
        var args = new StringEventData(menuToEnableId.Id);
        Debug.Log("My Debug: sending menu to enable event id: " + menuToEnableId.Id);
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(menuToEnableEventId, args);
    }

    private void SendEventToDisableMainMenu()
    {
        // var args = new StringEventData("");
        // ServiceLocator.GetService<EventQueue>().EnqueueEvent(menuToEnableEventId, args);
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

        var gameContinuesEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameContinuesEventId);
        gameContinuesEvent.SimpleEventSender -= OnNewGameContinuesEvent;
    }
}
