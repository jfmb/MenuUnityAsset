using ScriptableObjects;
using TMPro;
using UnityEngine;

public class UIButtonCreator : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private string _eventName;
    
    public void Setup(OptionSO optionSo)
    {
        text.text = optionSo.Text;
        _eventName = optionSo.EventName;
    }

    public void SendEventFromButton()
    {
        Debug.Log("Option " + _eventName +" sent");
    }
}
