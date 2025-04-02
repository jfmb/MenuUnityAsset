using ScriptableObjects;
using ScriptableObjects.Classes.Ids;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using TMPro;
using UnityEngine;

public class UIButtonCreator : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private EventId _menuOptionEventId;
    private MenuOptionId _menuOptionId;
    
    public void Setup(OptionSO optionSo)
    {
        text.text = optionSo.Text;
        _menuOptionEventId = optionSo.EventId;
        _menuOptionId = optionSo.OptionId;
    }

    public void SendEventFromButton()
    {
        var args = new StringEventData(_menuOptionId.Id);
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(_menuOptionEventId, args);
    }
}
