using ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;

public class UIMenuConfigurator : MonoBehaviour
{
    [SerializeField] private MenuSO menuSO;
    [SerializeField] private GameObject buttonOption;
    [SerializeField] private GameObject elementSubOption;
    [SerializeField] private RectTransform root;

    public MenuSO MenuSo => menuSO;

    public void Setup()
    {
        Assert.IsNotNull(MenuSo, "menuSO can't be null");

        foreach (var subOption in MenuSo.AllSubOptions)
        {
            var newSubOption = Instantiate(elementSubOption, root);
            newSubOption.GetComponent<UISubOptionConfigurator>().Setup(subOption);
        }
        
        foreach (var option in MenuSo.AllOptions)
        {
            var newOption = Instantiate(buttonOption, root);
            newOption.GetComponent<UIButtonCreator>().Setup(option);
        }
    }
}
