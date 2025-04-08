using System.Collections.Generic;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class UIMenuNavigator : MonoBehaviour
{
    [SerializeField] private EventId usingMouseEventId;

    [SerializeField] private UIMenuHorizontalNavigator uiHorizontalNavigator;
    
    [SerializeField] private UIShowMessage uiMessage;
    
    private List<GameObject> _allMenuElements = new();

    private bool _isUsingMouse;
    private bool _isUsingKeyboard;

    private bool _isUsingGampad;

    private bool _isUsingKeyboardOrGamepadAlready;
    
    [SerializeField] private EventId isUsingKeyboardEventId;
    [SerializeField] private EventId isUsingMouseEventId;
    [SerializeField] private EventId isUsingGamepadEventId;


    public void Setup()
    {
        _isUsingKeyboardOrGamepadAlready = false;
    }
    
    public void InjectMenuElements(List<GameObject> newMenuElements)
    {
        _allMenuElements = newMenuElements;

         Debug.Log("Menu elements count: " + _allMenuElements.Count);
    }

    private void Start()
    {
        var isUsingKeyboardEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isUsingKeyboardEventId);
        isUsingKeyboardEvent.SimpleEventSender += OnNewIsUsingKeyboardEvent;
        
        var isUsingMouseEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isUsingMouseEventId);
        isUsingMouseEvent.SimpleEventSender += OnNewIsUsingMouseEvent;
        
        var isUsingGamepadEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isUsingGamepadEventId);
        isUsingGamepadEvent.SimpleEventSender += OnNewIsUsingGamepadEvent;
    }

    private void OnNewIsUsingGamepadEvent()
    {
        _isUsingGampad = true;
        _isUsingKeyboard = false;
        _isUsingMouse = false;
    }

    private void OnNewIsUsingMouseEvent()
    {
        Debug.Log("Mouse is in use...");
        _isUsingMouse = true;
        _isUsingKeyboard = false;
        _isUsingGampad = false;
        
        _isUsingKeyboardOrGamepadAlready = false;
    }

    private void OnNewIsUsingKeyboardEvent()
    { 
        _isUsingMouse = false;
        _isUsingKeyboard = true;
        _isUsingGampad = false;
    }

    public void PointToFirstElement()
    {
        if (_isUsingMouse)
        {
//            Debug.Log("No point to first element, is using mouse");
            return;
        }

        if (_isUsingKeyboardOrGamepadAlready)
        {
//            Debug.Log("Keyboard or gamepad is in use already");
            return;
        }
        
        if (_allMenuElements.Count == 0)
        {
            Debug.Log("No elements to point to");
            return;
        }

        _isUsingKeyboardOrGamepadAlready = true;
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
        uiHorizontalNavigator.CheckIfCurrentObjectIsSubOption();
    }

    private void CheckKeyboard()
    {
        if (!_isUsingKeyboard)
        {
            return;
        }
        
        // _isUsingMouse = false;
        
        uiMessage.TextToShow = "Keyboard";
        
        PointToFirstElement();
    }
    
    private void CheckMouse()
    {
        if (!_isUsingMouse)
        {
            return;
        }
        
        EventSystem.current.SetSelectedGameObject(null);
        uiMessage.TextToShow = "Mouse";
        
        // _isUsingKeyboard = false;
        // _isUsingKeyboardAlready = false;
//        _isUsingMouse = true;
    }

    // private bool IsPlayerUsingMouse()
    // {
    //     return _currentMousePosition != _lastMousePosition;
    // }
    
    private void CheckGamepad()
    {
        if (!_isUsingGampad)
        {
            return;
        }
        _isUsingMouse = false;
        
        uiMessage.TextToShow = "Gamepad";

        PointToFirstElement();
    }

    private void OnDestroy()
    {
        var isUsingKeyboardEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isUsingKeyboardEventId);
        isUsingKeyboardEvent.SimpleEventSender -= OnNewIsUsingKeyboardEvent;
        
        var isUsingMouseEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isUsingMouseEventId);
        isUsingMouseEvent.SimpleEventSender -= OnNewIsUsingMouseEvent;
        
        var isUsingGamepadEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isUsingGamepadEventId);
        isUsingGamepadEvent.SimpleEventSender -= OnNewIsUsingGamepadEvent;
    }
}
