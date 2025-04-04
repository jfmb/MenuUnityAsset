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

        Debug.Log("Setup up in Menu Configurator in object " + name);
        BuildSubOptions();
        
        BuildOptions();
    }

    private void BuildSubOptions()
    {
        var index = 0;
        foreach (var subOption in MenuSo.AllSubOptions)
        {
            var newSubOption = Instantiate(elementSubOption, root);
            newSubOption.name = subOption.Title;
            newSubOption.GetComponent<UISubOptionConfigurator>().Setup(subOption);
            newSubOption.GetComponent<UIMenuElement>().IsSubOption = true;
            AllMenuElements.Add(newSubOption);
        }
    }

    private void BuildOptions()
    {
        var index = 0;
        foreach (var option in MenuSo.AllOptions)
        {
            var newOption = Instantiate(buttonOption, root);
            newOption.name = option.Text;
            newOption.GetComponent<UIButtonCreator>().Setup(option);
            newOption.GetComponent<UIMenuElement>().IsSubOption = false;
            AllMenuElements.Add(newOption);
        }
    }
}
