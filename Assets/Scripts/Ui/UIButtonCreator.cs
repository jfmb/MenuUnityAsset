using ScriptableObjects;
using ScriptableObjects.Classes.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonCreator : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image icon;

    private EventId _menuOptionEventId;
    private MenuOptionId _menuOptionId;
    
    public void Setup(OptionSO optionSo)
    {
        text.text = optionSo.Text;

        icon.sprite = optionSo.Icon;
        
        _menuOptionEventId = optionSo.EventId;
        _menuOptionId = optionSo.OptionId;
    }

    public void SendEventFromButton()
    {
        var args = new StringEventData(_menuOptionId.Id);
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(_menuOptionEventId, args);
    }
}
