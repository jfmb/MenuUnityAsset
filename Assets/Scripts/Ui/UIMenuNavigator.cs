using System.Collections.Generic;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIMenuNavigator : MonoBehaviour
{
    [SerializeField] private EventId UIUpEventId;
    [SerializeField] private EventId UIDownEventId;
    [SerializeField] private EventId UISelectEventId;
    
    private List<GameObject> _allMenuElements = new();
    
    private int _selectedIndex;
    private int _currentIndex;
    void OnEnable()
    {
        InputSystem.onDeviceChange += DeviceChange;
    }

    public void Setup()
    {
        _currentIndex = 0;
        _selectedIndex = _currentIndex;
        SubscribeToEvents();
    }
    
    public void SubscribeToEvents()
    {
        var uiUpEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIUpEventId);
        uiUpEvent.SimpleEventSender += OnNewUIUpEvent;

        var uiDownEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIDownEventId);
        uiDownEvent.SimpleEventSender += OnNewUIDownEvent;

        var uiSelectEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UISelectEventId);
        uiSelectEvent.SimpleEventSender += OnNewUISelectEvent;
    }

    private void OnNewUISelectEvent()
    {
        if (!_allMenuElements[_currentIndex].GetComponent<Button>())
        {
            return;
        }
        Debug.Log("Click on button with index " + _currentIndex);
        _allMenuElements[_currentIndex].GetComponent<Button>().onClick.Invoke();
    }

    public void InjectMenuElements(List<GameObject> newMenuElements)
    {
        _allMenuElements.Clear();
        _allMenuElements = newMenuElements;
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
        if (_allMenuElements.Count == 0)
        {
            return;
        }
        Debug.Log(("Pointing first element..."));
        EventSystem.current.firstSelectedGameObject = _allMenuElements[0];
        EventSystem.current.SetSelectedGameObject(_allMenuElements[0]);
    }
    
    
    private void DeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (change == InputDeviceChange.Added)
        {
            Debug.Log("Device Connected: " + device);
        }
        else if (change == InputDeviceChange.Removed)
        {
            Debug.Log("Device Disconnected: " + device);
        }
    }
    
    void OnDisable()
    {
        InputSystem.onDeviceChange -= DeviceChange;
    }
    
    public void UnsubscribeToEvents()
    {
        var uiUpEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIUpEventId);
        uiUpEvent.SimpleEventSender -= OnNewUIUpEvent;

        var uiDownEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIDownEventId);
        uiDownEvent.SimpleEventSender -= OnNewUIDownEvent;
        
        var uiSelectEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UISelectEventId);
        uiSelectEvent.SimpleEventSender -= OnNewUISelectEvent;
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }
}
