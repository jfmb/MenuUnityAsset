using System.Collections.Generic;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class UIMenuNavigator : MonoBehaviour
{
    [SerializeField] private EventId UILeftEventId;
    [SerializeField] private EventId UIRIghtEventId;
    [SerializeField] private EventId usingMouseEventId;
    
    private List<GameObject> _allMenuElements = new();

    private bool _isUsingMouse;
    private bool _isUsingKeyboardAlready;
    
    private Keyboard _keyboard;
    private Mouse _mouse;
    private Gamepad _gamepad;
    
    private GameObject _lastSelectedGameObject;
    
    void Awake()
    {
        _keyboard = InputSystem.GetDevice<Keyboard>();
        _mouse = InputSystem.GetDevice<Mouse>();
        _gamepad = InputSystem.GetDevice<Gamepad>();
    }
    
    public void Setup()
    {
        _isUsingKeyboardAlready = false;
    }
    
    public void SubscribeToHorizontalInputEvents()
    {
        var uiLeftEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UILeftEventId);
        uiLeftEvent.SimpleEventSender += OnNewUILeftEvent;
        
        var uiRightEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIRIghtEventId);
        uiRightEvent.SimpleEventSender += OnNewUIRightEvent;
    }

    public void InjectMenuElements(List<GameObject> newMenuElements)
    {
        _allMenuElements = newMenuElements;

         Debug.Log("Menu elements count: " + _allMenuElements.Count);
    }
    
    private void OnNewUIRightEvent()
    {
        var currentObject = EventSystem.current.currentSelectedGameObject;
        if (!currentObject.GetComponent<UIMenuElement>().IsSubOption)
        {
            return;
        }
        currentObject.GetComponent<UISubOptionConfigurator>().IncreaseValueFromButton();
    }

    private void OnNewUILeftEvent()
    {
        var currentObject = EventSystem.current.currentSelectedGameObject;
        if (!currentObject.GetComponent<UIMenuElement>().IsSubOption)
        {
            return;
        }
        
        currentObject.GetComponent<UISubOptionConfigurator>().DecreaseValueFromButton();
    }

    public void PointToFirstElement()
    {
        if (_isUsingMouse)
        {
            Debug.Log("No point to first element, is using mouse");
            return;
        }

        if (_isUsingKeyboardAlready)
        {
            Debug.Log("Keyboard is in use already");
            return;
        }
        
        if (_allMenuElements.Count == 0)
        {
            Debug.Log("No elements to point to");
            return;
        }

        _isUsingKeyboardAlready = true;
        Debug.Log(("Pointing first element..."));
        EventSystem.current.firstSelectedGameObject = _allMenuElements[0];
        EventSystem.current.SetSelectedGameObject(_allMenuElements[0]);
    }
    
    private void Update()
    {
        CheckKeyboard();

        CheckMouse();

        CheckGamepad();
        
        CheckIfCurrentObjectIsSubOption();
    }

    private void CheckIfCurrentObjectIsSubOption()
    {
        if (_lastSelectedGameObject != EventSystem.current.currentSelectedGameObject)
        {
            if (_lastSelectedGameObject && _lastSelectedGameObject.GetComponent<UIMenuElement>().IsSubOption)
            {
                UnsubscribeToHorizonatalInputEvents();
            }
            
            _lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;

            if (_lastSelectedGameObject)
            {
                if (_lastSelectedGameObject.GetComponent<UIMenuElement>().IsSubOption)
                {                
                    SubscribeToHorizontalInputEvents();
                }
            }
        } 
    }
    
    private void CheckKeyboard()
    {
        if (!_keyboard.anyKey.wasPressedThisFrame)
        {
            return;
        }
        
        _isUsingMouse = false;
        Debug.Log("User is using the keyboard");
        PointToFirstElement();
    }
    
    private void CheckMouse()
    {
        if (!_mouse.leftButton.wasPressedThisFrame && !_mouse.rightButton.wasPressedThisFrame)
        {
            return;
        }
        Debug.Log("User is using the mouse.");
        _isUsingKeyboardAlready = false;
        _isUsingMouse = true;
    }

    private void CheckGamepad()
    {
        if (!IsGamepadTouched())
        {
            return;
        }
        _isUsingMouse = false;
        Debug.Log("User is using the gamepad");
        PointToFirstElement();
    }
    
    private bool IsGamepadTouched()
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

    
    public void UnsubscribeToHorizonatalInputEvents()
    {
        var uiLeftEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UILeftEventId);
        uiLeftEvent.SimpleEventSender -= OnNewUILeftEvent;
        
        var uiRightEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIRIghtEventId);
        uiRightEvent.SimpleEventSender -= OnNewUIRightEvent;
    }

    private void OnDestroy()
    {
        UnsubscribeToHorizonatalInputEvents();
    }
}
