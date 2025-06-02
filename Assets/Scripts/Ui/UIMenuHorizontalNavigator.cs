using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMenuHorizontalNavigator : MonoBehaviour
{
    [SerializeField] private EventId UILeftEventId;
    [SerializeField] private EventId UIRIghtEventId;
    
    private GameObject _lastSelectedGameObject;
    
    public void CheckIfCurrentObjectIsSubOption()
    {
        if (_lastSelectedGameObject == EventSystem.current.currentSelectedGameObject)
        {
            return;
        }
        
        UnsubscribeCurrentSuboptionToHorizontalInputs();
        _lastSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        SubscribeNextSubOptionToHorizontalInputs();

    }
    
    private void UnsubscribeCurrentSuboptionToHorizontalInputs()
    {
        if (_lastSelectedGameObject && _lastSelectedGameObject.GetComponent<UIMenuElement>().IsSubOption)
        {
            SaveCurrentSubOptionValueIntoGameSettings();
            UnsubscribeToHorizontalInputEvents();
        }
    }

    private void SaveCurrentSubOptionValueIntoGameSettings()
    {
        var currentObject = EventSystem.current.currentSelectedGameObject;
        
        if (!currentObject)
        {
            return;
        }
        
        if(!currentObject.GetComponent<UIMenuElement>().IsSubOption)
        {
            return;
        }
        
        var currentSubOptionId = currentObject.GetComponent<UISubOptionCreator>().SubOptionId;
        var currentSubOptionValue = currentObject.GetComponent<UISubOptionCreator>().CurrentValue;
        ServiceLocator.GetService<GameInfoFacade>().SaveSettingNewValueWithKey(currentSubOptionId, currentSubOptionValue);
        
        Debug.Log("My Debug: Setting saved:" + currentSubOptionId + " -> " + currentSubOptionValue);
    }

    public void UnsubscribeToHorizontalInputEvents()
    {
        var uiLeftEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UILeftEventId);
        uiLeftEvent.SimpleEventSender -= OnNewUILeftEvent;
        
        var uiRightEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIRIghtEventId);
        uiRightEvent.SimpleEventSender -= OnNewUIRightEvent;
    }

    private void SubscribeNextSubOptionToHorizontalInputs()
    {
        if (!_lastSelectedGameObject)
        {
            return;
        }
        
        if (_lastSelectedGameObject.GetComponent<UIMenuElement>().IsSubOption)
        {                
            SubscribeToHorizontalInputEvents();
        }
    }
    
    public void SubscribeToHorizontalInputEvents()
    {
        var uiLeftEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UILeftEventId);
        uiLeftEvent.SimpleEventSender += OnNewUILeftEvent;
        
        var uiRightEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(UIRIghtEventId);
        uiRightEvent.SimpleEventSender += OnNewUIRightEvent;
    }
    
    private void OnNewUIRightEvent()
    {
        var currentObject = EventSystem.current.currentSelectedGameObject;
        if (!currentObject.GetComponent<UIMenuElement>().IsSubOption)
        {
            return;
        }
        currentObject.GetComponent<UISubOptionCreator>().IncreaseValueFromButton();
    }

    private void OnNewUILeftEvent()
    {
        var currentObject = EventSystem.current.currentSelectedGameObject;
        if (!currentObject.GetComponent<UIMenuElement>().IsSubOption)
        {
            return;
        }
        
        currentObject.GetComponent<UISubOptionCreator>().DecreaseValueFromButton();
    }

    private void OnDestroy()
    {
        UnsubscribeToHorizontalInputEvents();
    }
}
