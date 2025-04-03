using System.Collections.Generic;
using ScriptableObjects.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;

public class UIMenuSelector : MonoBehaviour
{
    [SerializeField] private List<UIMenuConfigurator> menus;
    [SerializeField] private MenuId mainMenu;
    [SerializeField] private EventId menuToEnableEventId;
    
    private readonly Dictionary<string, UIMenuConfigurator> _allMenus = new ();
    
    private string _currentMenuIdEnabled;

    private UIMenuNavigator _uiCurrentMenuNavigator;
    
    void Start()
    {
        _uiCurrentMenuNavigator = GetComponent<UIMenuNavigator>();
        _uiCurrentMenuNavigator.SubscribeToEvents();
        
        var menuToEnableEvent = (StringEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(menuToEnableEventId);
        menuToEnableEvent.StringEventSender += OnNewMenuEnableEvent;
        
        foreach (var menu in menus)
        {
            menu.Setup();
            menu.gameObject.SetActive(false);

            var key = menu.MenuSo.Id.Id;
            _allMenus.Add(key, menu);
        }
        
        EnableNewMenuWith(mainMenu.Id);
    }

    private void OnNewMenuEnableEvent(object source, StringEventData args)
    {
        if (string.IsNullOrEmpty(args.Value))
        {
            Debug.Log("MenuId is null or empty");
            return;
        }

        DisableCurrentMenu();
        EnableNewMenuWith(args.Value);
    }

    private void DisableCurrentMenu()
    {
        _uiCurrentMenuNavigator.UnsubscribeToEvents();
        _allMenus[_currentMenuIdEnabled].gameObject.SetActive(false);
        Debug.Log("Menu to disable id: " + _currentMenuIdEnabled);
    }

    private void EnableNewMenuWith(string id)
    {
        _currentMenuIdEnabled = id;
        Debug.Log("Menu to enable id: " + id);
        
        _allMenus[_currentMenuIdEnabled].gameObject.SetActive(true);

        _uiCurrentMenuNavigator.Setup();

        foreach (var element in _allMenus[_currentMenuIdEnabled].AllMenuElements)
        {
            Debug.Log("Element: " + element.name);
        }
        _uiCurrentMenuNavigator.InjectMenuElements(_allMenus[_currentMenuIdEnabled].AllMenuElements);
        _uiCurrentMenuNavigator.PointToFirstElement();
    }

    private bool CurrentMenuIdIsEmptyBecauseIsFirstTime()
    {
        return string.IsNullOrEmpty(_currentMenuIdEnabled);
    }
    
    private void OnDestroy()
    {
        var menuToEnableEvent = (StringEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(menuToEnableEventId);
        menuToEnableEvent.StringEventSender -= OnNewMenuEnableEvent;
    }
}
