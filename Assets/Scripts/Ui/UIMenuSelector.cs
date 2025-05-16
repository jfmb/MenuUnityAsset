using System;
using System.Collections.Generic;
using ScriptableObjects.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class UIMenuSelector : MonoBehaviour
{
    [SerializeField] private List<UIMenuConfigurator> menus;
    [SerializeField] private MenuId mainMenu;
    [SerializeField] private EventId menuToEnableEventId;
    [SerializeField] private EventId gameStartsEventId;
    [SerializeField] private EventId gameContinuesEventId;
    [SerializeField] private EventId backInMenuEventId;
    [SerializeField] private EventId startButtonPressedMenuInGameId;
    
    private readonly Dictionary<string, UIMenuConfigurator> _allMenus = new ();

    private UIMenuNavigator _uiCurrentMenuNavigator;
    
    private Stack<string> _stackOfMenus = new();

    private bool _isGameStarted;

    private void Start()
    {
        _uiCurrentMenuNavigator = GetComponent<UIMenuNavigator>();
        
        var menuToEnableEvent = (StringEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(menuToEnableEventId);
        menuToEnableEvent.StringEventSender += OnNewMenuEnableEvent;

        var gameStartsEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameStartsEventId);
        gameStartsEvent.SimpleEventSender += OnNewGameStartsEvent;

        var gameContinuesEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameContinuesEventId);
        gameContinuesEvent.SimpleEventSender += OnNewGameContinuesEvent;

        var backInMenuEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(backInMenuEventId);
        backInMenuEvent.SimpleEventSender += OnNewBackInMenuEvent;

        var startButtonPressedMenuInGameEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>()
            .GetEventWithEventId(startButtonPressedMenuInGameId);
        startButtonPressedMenuInGameEvent.SimpleEventSender += OnNewStartPressedMenuInGame;
        
        foreach (var menu in menus)
        {
            menu.Setup();
            menu.gameObject.SetActive(false);

            var key = menu.MenuSo.Id.Id;
            _allMenus.Add(key, menu);
        }
        
        _stackOfMenus.Push(mainMenu.Id);
//        Debug.Log("Menus in stack: " + _stackOfMenus.Count);

        EnableNewMenuWith(mainMenu.Id);
    }

    private void OnNewStartPressedMenuInGame()
    {
        if (!_isGameStarted)
        {
            return;
        }

        if (_stackOfMenus.Count != 1)
        {
            return;
        }
        
        RemoveMenuAndGoToGame();
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(gameContinuesEventId, EventArgs.Empty);
//            Debug.Log("Menus in stack: " + _stackOfMenus.Count);
    }


    private void EnableNewMenuWith(string id)
    {
//       Debug.Log("Menu to enable id: " + id);
        
        _allMenus[id].gameObject.SetActive(true);

        foreach (var element in _allMenus[id].AllMenuElements)
        {
            Debug.Log("Element: " + element.name);
        }

        _uiCurrentMenuNavigator.InjectMenuElements(_allMenus[id].AllMenuElements);
        _uiCurrentMenuNavigator.PointToFirstElement();
    }
    
    private void OnNewMenuEnableEvent(object source, StringEventData args)
    {
        if (string.IsNullOrEmpty(args.Value))
        {
            Debug.Log("MenuId is null or empty");
            return;
        }

        HideCurrentMenu();
        
        var id = args.Value;
        _stackOfMenus.Push(id);
//        Debug.Log("Menus in stack: " + _stackOfMenus.Count);

        EnableNewMenuWith(id);
    }
    
    private void HideCurrentMenu()
    {
        if (_stackOfMenus.Count == 0)
        {
            return;
        }
        
        var currentMenuId = _stackOfMenus.Peek();
        _allMenus[currentMenuId].gameObject.SetActive(false);  
    }
    
    private void OnNewBackInMenuEvent()
    {
        if (!_isGameStarted)
        {
            if (_stackOfMenus.Count == 1)
            {
                return;
            }
        }

        if (_stackOfMenus.Count == 1)
        {
            RemoveMenuAndGoToGame();
            ServiceLocator.GetService<EventQueue>().EnqueueEvent(gameContinuesEventId, EventArgs.Empty);
//            Debug.Log("Menus in stack: " + _stackOfMenus.Count);
            return;
        }
        
        RemoveCurrentMenu();
        var menuId = _stackOfMenus.Peek();
        EnableNewMenuWith(menuId);
    }
        
    private void RemoveCurrentMenu()
    {
        if (_stackOfMenus.Count == 0)
        {
            return;
        }

        var currentMenuId = _stackOfMenus.Pop();
//        Debug.Log("Menus in stack: " + _stackOfMenus.Count);

        _allMenus[currentMenuId].gameObject.SetActive(false);
//        Debug.Log("Menu to disable id: " + currentMenuId);
    }

    private void RemoveMenuAndGoToGame()
    {
        RemoveCurrentMenu();
    }

    private void OnNewGameStartsEvent()
    {
        _isGameStarted = true;
        RemoveMenuAndGoToGame();
    }


    private void OnNewGameContinuesEvent()
    {
        RemoveMenuAndGoToGame();
    }
    
    private void OnDestroy()
    {
        var menuToEnableEvent = (StringEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(menuToEnableEventId);
        menuToEnableEvent.StringEventSender -= OnNewMenuEnableEvent;
        
        var gameStartsEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameStartsEventId);
        gameStartsEvent.SimpleEventSender -= OnNewGameStartsEvent;

        var gameContinuesEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameContinuesEventId);
        gameContinuesEvent.SimpleEventSender -= OnNewGameContinuesEvent;
        
        var backInMenuEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(backInMenuEventId);
        backInMenuEvent.SimpleEventSender -= OnNewBackInMenuEvent;
        
        
        var startButtonPressedMenuInGameEvent = (SimpleEvent)ServiceLocator.GetService<EventQueue>()
            .GetEventWithEventId(startButtonPressedMenuInGameId);
        startButtonPressedMenuInGameEvent.SimpleEventSender -= OnNewStartPressedMenuInGame;
    }
}
