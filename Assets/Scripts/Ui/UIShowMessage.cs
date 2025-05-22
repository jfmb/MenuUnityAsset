using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIShowMessage : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private TMP_Text text;
    private string _textToShow;

    private float _seconds;
    
    public string TextToShow
    {
        get => _textToShow;
        set
        {
            _textToShow = value;
            ShowMessage();
        }
    }

    private void Start()
    {
        text.GetComponent<TMP_Text>().text = "";
        messagePanel.SetActive(false);
        
        _seconds = messagePanel.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length;
    }
    
    private void ShowMessage()
    {
        messagePanel.SetActive(true);
        text.GetComponent<TMP_Text>().text = _textToShow;
    }

    private IEnumerator DisableMessagePanel()
    {
        yield return new WaitForSeconds(_seconds);
        
        messagePanel.SetActive(false);
    }
}
