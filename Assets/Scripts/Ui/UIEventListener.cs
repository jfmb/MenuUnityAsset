using System;
using System.Collections.Generic;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Ui.MenuOptions.Interfaces;
using UnityEngine;

public class UIEventListener : MonoBehaviour
{
    [SerializeField] private EventId menuOptionEventId;
    [SerializeField] private List<MenuOption> menuOptions;

    private Dictionary<string, MenuOption> _allMenuOptions = new ();
    void Start()
    {
        foreach (var option in menuOptions)
        {
            var key = option.OptionId.Id;
            _allMenuOptions.Add(key, option);
        }

        var menuOptionEvent = (StringEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(menuOptionEventId);
        menuOptionEvent.StringEventSender += OnNewMenuOptionEvent;
    }

    private void OnNewMenuOptionEvent(object source, StringEventData args)
    {
        Debug.Log("Event MenuOption Received!!!");
        _allMenuOptions[args.Value].Execute();
    }

    private void OnDestroy()
    {
        var menuOptionEvent = (StringEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(menuOptionEventId);
        menuOptionEvent.StringEventSender -= OnNewMenuOptionEvent;
    }
}
