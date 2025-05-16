using System.Collections.Generic;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMenuNavigator : MonoBehaviour
{
    [SerializeField] private EventId usingMouseEventId;
    [SerializeField] private EventId usingKeyboardEventId;
    [SerializeField] private EventId usingGamepadEventId;

    [SerializeField] private UIMenuHorizontalNavigator uiHorizontalNavigator;
    
    [SerializeField] private UIShowMessage uiMessage;
    
    private List<GameObject> _allMenuElements = new();

    private bool _isUsingMouse;
    
    public void InjectMenuElements(List<GameObject> newMenuElements)
    {
        _allMenuElements = newMenuElements;

         Debug.Log("Menu elements count: " + _allMenuElements.Count);
    }

    private void Start()
    {
        var isUsingKeyboardEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(usingKeyboardEventId);
        isUsingKeyboardEvent.SimpleEventSender += OnNewIsUsingKeyboardEvent;
        
        var isUsingMouseEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(usingMouseEventId);
        isUsingMouseEvent.SimpleEventSender += OnNewIsUsingMouseEvent;
        
        var isUsingGamepadEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(usingGamepadEventId);
        isUsingGamepadEvent.SimpleEventSender += OnNewIsUsingGamepadEvent;
    }

    private void OnNewIsUsingMouseEvent()
    {
        Debug.Log("Mouse is in use...");
        _isUsingMouse = true;
        
        Cursor.visible = true;
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.SetSelectedGameObject(null);
        uiMessage.TextToShow = "Mouse";
    }

    private void OnNewIsUsingKeyboardEvent()
    {         
        Debug.Log("Keyboard is in use...");
        _isUsingMouse = false;

        SetAllMenuElementsNormal();

        Cursor.visible = false;
        
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.SetSelectedGameObject(null);
        uiMessage.TextToShow = "Keyboard";
        PointToFirstElement();
    }

    private void SetAllMenuElementsNormal()
    {
        foreach (var element in _allMenuElements)
        {
            EventSystem.current.SetSelectedGameObject(element);

            var isPointerOverCurrentElement = EventSystem.current.IsPointerOverGameObject();
            if (!isPointerOverCurrentElement)
            {
                continue;
            }
            
            var pointer = new PointerEventData(EventSystem.current);
            var currentSelectable = element.GetComponent<UIMenuElement>().SelectableInElement;
            currentSelectable.OnPointerExit(pointer);
        }
    }
    
    private void OnNewIsUsingGamepadEvent()
    {
        Debug.Log("Gamepad is in use...");

        _isUsingMouse = false;
        
        SetAllMenuElementsNormal();
        
        Cursor.visible = false;
        
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.SetSelectedGameObject(null);
        uiMessage.TextToShow = "Gamepad";
        PointToFirstElement();
    }

    public void PointToFirstElement()
    {
        if (_isUsingMouse)
        {
            Debug.Log("No point to first element, is using mouse");
            return;
        }
        
        if (_allMenuElements.Count == 0)
        {
            Debug.Log("No elements to point to");
            return;
        }
        
        Debug.Log(("Pointing first element..."));
        EventSystem.current.firstSelectedGameObject = _allMenuElements[0];
        EventSystem.current.SetSelectedGameObject(_allMenuElements[0]);
    }
    
    private void Update()
    {
        CheckIfCurrentObjectIsSubOption();
    }

    private void CheckIfCurrentObjectIsSubOption()
    {
        uiHorizontalNavigator.CheckIfCurrentObjectIsSubOption();
    }
    private void OnDestroy()
    {
        var isUsingKeyboardEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(usingKeyboardEventId);
        isUsingKeyboardEvent.SimpleEventSender -= OnNewIsUsingKeyboardEvent;
        
        var isUsingMouseEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(usingMouseEventId);
        isUsingMouseEvent.SimpleEventSender -= OnNewIsUsingMouseEvent;
        
        var isUsingGamepadEvent = (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(usingGamepadEventId);
        isUsingGamepadEvent.SimpleEventSender -= OnNewIsUsingGamepadEvent;
    }
}
