using System;
using System.Collections.Generic;
using ScriptableObjects.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class UIMenuBuilder : MonoBehaviour
{
    [SerializeField] private List<UIMenuConfigurator> menus;
    [SerializeField] private MenuId mainMenu;
    [SerializeField] private EventId menuToEnableEventId;
    
    private readonly Dictionary<string, UIMenuConfigurator> _allMenus = new ();
    private string _currentMenuIdEnabled;

    void Start()
    {
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
        
        EnableNewMenuWith(args.Value);
    }

    private void EnableNewMenuWith(string id)
    {
        if (_currentMenuIdEnabled != null)
        {
            _allMenus[_currentMenuIdEnabled].gameObject.SetActive(false);
        }
       
        _allMenus[id].gameObject.SetActive(true);
        _currentMenuIdEnabled = id;
    }

    private void OnDestroy()
    {
        var menuToEnableEvent = (StringEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(menuToEnableEventId);
        menuToEnableEvent.StringEventSender -= OnNewMenuEnableEvent;
    }
}
