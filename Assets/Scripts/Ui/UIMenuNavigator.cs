using System.Collections.Generic;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class UIMenuNavigator : MonoBehaviour
{
    [SerializeField] private EventId UIUpEventId;
    [SerializeField] private EventId UIDownEventId;
    [SerializeField] private EventId usingMouseEventId;
    
    private List<GameObject> _allMenuElements = new();
    
    private int _selectedIndex;
    private int _currentIndex;

    private bool _isUsingMouse;
    private bool _isUsingKeyboardAlready;
    
    private Keyboard _keyboard;
    private Mouse _mouse;
    private Gamepad _gamepad;
    

    
    void Awake()
    {
        _keyboard = InputSystem.GetDevice<Keyboard>();
        _mouse = InputSystem.GetDevice<Mouse>();
        _gamepad = InputSystem.GetDevice<Gamepad>();
    }

    private void Start()
    {
        SubscribeToEvents();
    }
    
    public void Setup()
    {
        _currentIndex = 0;
        _selectedIndex = _currentIndex;
        _isUsingKeyboardAlready = false;
        
        Debug.Log("Menu Navigator is reset");
    }
    
    public void SubscribeToEvents()
    {
        // var uiUpEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIUpEventId);
        // uiUpEvent.SimpleEventSender += OnNewUIUpEvent;
        //
        // var uiDownEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIDownEventId);
        // uiDownEvent.SimpleEventSender += OnNewUIDownEvent;
    }

    public void InjectMenuElements(List<GameObject> newMenuElements)
    {
        _allMenuElements = newMenuElements;

         Debug.Log("Menu elements count: " + _allMenuElements.Count);
    }
    
    private void OnNewUIDownEvent()
    {
        Debug.Log("Down event");
        _allMenuElements[_currentIndex].GetComponent<UIMenuElement>().DeSelectedElement();

        _selectedIndex++;
        if (_selectedIndex == _allMenuElements.Count)
        {
            _selectedIndex = 0;
        }
        _allMenuElements[_selectedIndex].GetComponent<UIMenuElement>().SetSelectedElement();
        _currentIndex = _selectedIndex;
    }

    private void OnNewUIUpEvent()
    {
        Debug.Log("Up event");

        _allMenuElements[_currentIndex].GetComponent<UIMenuElement>().DeSelectedElement();

        _selectedIndex--;
        if (_selectedIndex < 0)
        {
            _selectedIndex = _allMenuElements.Count - 1;
        }
        _allMenuElements[_selectedIndex].GetComponent<UIMenuElement>().SetSelectedElement();
        _currentIndex = _selectedIndex;
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
        return (_gamepad.leftStick.value.y > 0 ||
                _gamepad.leftStick.value.y < 0 ||
                _gamepad.buttonSouth.wasPressedThisFrame ||
                _gamepad.leftTrigger.wasPressedThisFrame ||
                _gamepad.rightTrigger.wasPressedThisFrame ||
                _gamepad.startButton.wasPressedThisFrame ||
                _gamepad.selectButton.wasPressedThisFrame);
    }

    
    public void UnsubscribeToEvents()
    {
        // var uiUpEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIUpEventId);
        // uiUpEvent.SimpleEventSender -= OnNewUIUpEvent;
        //
        // var uiDownEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIDownEventId);
        // uiDownEvent.SimpleEventSender -= OnNewUIDownEvent;
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }
}
