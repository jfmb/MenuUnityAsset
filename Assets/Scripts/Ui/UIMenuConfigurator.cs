using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;

public class UIMenuConfigurator : MonoBehaviour
{
    [SerializeField] private MenuSO menuSO;
    [SerializeField] private GameObject buttonOption;
    [SerializeField] private GameObject elementSubOption;
    [SerializeField] private RectTransform root;

    public MenuSO MenuSo => menuSO;


    private List<GameObject> _allMenuElements = new();
    public List<GameObject> AllMenuElements => _allMenuElements;

    public void Setup()
    {
        Assert.IsNotNull(MenuSo, "menuSO can't be null");

        BuildSubOptions();
        
        BuildOptions();
    }

    private void BuildSubOptions()
    {
        var index = 0;
        foreach (var subOption in MenuSo.AllSubOptions)
        {
            var newSubOption = Instantiate(elementSubOption, root);
            newSubOption.GetComponent<UISubOptionConfigurator>().Setup(subOption);
            AllMenuElements.Add(newSubOption);
        }
    }

    private void BuildOptions()
    {
        var index = 0;
        foreach (var option in MenuSo.AllOptions)
        {
            var newOption = Instantiate(buttonOption, root);
            newOption.GetComponent<UIButtonCreator>().Setup(option);
            AllMenuElements.Add(newOption);
        }
    }
}
