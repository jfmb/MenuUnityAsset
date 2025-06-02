using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Services.Languages;
using TMPro;
using UnityEngine;

public class TextLocalizer : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private TMP_Text textToLocalize;
    [SerializeField] private EventId localizationEventId;
    
    void OnEnable()
    {
        var localizationEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(localizationEventId);
        localizationEvent.SimpleEventSender += OnNewLocalizationEvent;
    }

    private void OnNewLocalizationEvent()
    {
        var localizedText = ServiceLocator.GetService<Localization>().GetLocalizedText(key);
        textToLocalize.text = localizedText;
    }

    private void OnDisable()
    {
        var localizationEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(localizationEventId);
        localizationEvent.SimpleEventSender -= OnNewLocalizationEvent;
    }
}
