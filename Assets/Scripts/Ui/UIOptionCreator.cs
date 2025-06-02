using ScriptableObjects;
using ScriptableObjects.Classes.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Services.Languages;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionCreator : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image icon;
    [SerializeField] private EventId localizationEventId;
    
    private EventId _menuOptionEventId;
    private MenuOptionId _menuOptionId;
   
    void OnEnable()
    {
        var localizationEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(localizationEventId);
        localizationEvent.SimpleEventSender += OnNewLocalizationEvent;
    }
    
    public void Setup(OptionSO optionSo)
    {
        icon.sprite = optionSo.Icon;
        
        _menuOptionEventId = optionSo.EventId;
        _menuOptionId = optionSo.OptionId;
        
        LocalizeText();
    }

    public void LocalizeText()
    {
        var key = _menuOptionId.Id;
        var localizedText = ServiceLocator.GetService<Localization>().GetLocalizedText(key);
        text.text = localizedText;
    }
    
    public void SendEventFromButton()
    {
        var args = new StringEventData(_menuOptionId.Id);
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(_menuOptionEventId, args);
    }
    
    private void OnNewLocalizationEvent()
    {
        LocalizeText();
    }

    private void OnDisable()
    {
        var localizationEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(localizationEventId);
        localizationEvent.SimpleEventSender -= OnNewLocalizationEvent;
    }
}
