using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIMenuCreator : MonoBehaviour
{
    [SerializeField] private MenuSO menuSO;
    [SerializeField] private GameObject labelOption;
    [SerializeField] private RectTransform scrollView;
    
    void Start()
    {
        foreach (var option in menuSO.AllOptions)
        {
            var newOption = Instantiate(labelOption, scrollView);
            newOption.GetComponent<UIButtonCreator>().Setup(option);
        }
    }
}
