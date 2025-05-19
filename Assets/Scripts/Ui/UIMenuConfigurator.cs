using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIMenuConfigurator : MonoBehaviour
{
    [SerializeField] private MenuSO menuSO;
    [SerializeField] private GameObject buttonOption;
    [SerializeField] private GameObject elementSubOption;
    [SerializeField] private RectTransform root;

    public MenuSO MenuSo => menuSO;

    private List<GameObject> _allMenuElements = new();
    public List<GameObject> AllMenuElements => _allMenuElements;

    private Navigation _nav = new();
    public void Setup()
    {
        Assert.IsNotNull(MenuSo, "menuSO can't be null");

        _nav.mode = Navigation.Mode.Explicit;
        
        Debug.Log("Setup up in Menu Configurator in object " + name);
        BuildSubOptions();
        
        BuildOptions();

        BuildNavigation();
    }
    
    private void BuildSubOptions()
    {
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
        foreach (var option in MenuSo.AllOptions)
        {
            var newOption = Instantiate(buttonOption, root);
            newOption.name = option.Text;
            newOption.GetComponent<UIButtonCreator>().Setup(option);
            newOption.GetComponent<UIMenuElement>().IsSubOption = false;
            AllMenuElements.Add(newOption);
        }
    }


    private void BuildNavigation()
    {
        BuildNavigationForTheFirstElement();
        
        for (var i = 1; i < AllMenuElements.Count-1; i++)
        {
            _nav.selectOnUp = AllMenuElements[i - 1].GetComponent<UIMenuElement>().SelectableInElement;
            _nav.selectOnDown = AllMenuElements[i + 1].GetComponent<UIMenuElement>().SelectableInElement;

            AllMenuElements[i].GetComponent<UIMenuElement>().SelectableInElement.navigation = _nav;
        }

        BuildNavigationForTheLastElement();
    }

    private void BuildNavigationForTheFirstElement()
    {
        _nav.selectOnUp = AllMenuElements[AllMenuElements.Count - 1].GetComponent<UIMenuElement>().SelectableInElement;
        _nav.selectOnDown = AllMenuElements[1].GetComponent<UIMenuElement>().SelectableInElement;

        AllMenuElements[0].GetComponent<UIMenuElement>().SelectableInElement.navigation = _nav;
    }

    private void BuildNavigationForTheLastElement()
    {
        _nav.selectOnUp = AllMenuElements[AllMenuElements.Count - 2].GetComponent<UIMenuElement>().SelectableInElement;
        _nav.selectOnDown = AllMenuElements[0].GetComponent<UIMenuElement>().SelectableInElement;
        
        AllMenuElements[AllMenuElements.Count - 1].GetComponent<UIMenuElement>().SelectableInElement.navigation = _nav;
    }

    public void SaveCurrentSubOptionsPermanent()
    {
        foreach (var element in AllMenuElements)
        {
            if (!element.GetComponent<UIMenuElement>().IsSubOption)
            {
                return;
            }

            element.GetComponent<UISubOptionConfigurator>().SaveCurrentValuePermanent();
        }
    }
}
