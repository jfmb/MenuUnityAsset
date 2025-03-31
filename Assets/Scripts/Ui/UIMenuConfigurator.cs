using ScriptableObjects;
using UnityEngine;
using UnityEngine.Assertions;

public class UIMenuConfigurator : MonoBehaviour
{
    [SerializeField] private MenuSO menuSO;
    [SerializeField] private GameObject labelOption;
    [SerializeField] private RectTransform root;

    public MenuSO MenuSo => menuSO;

    public void Setup()
    {
        Assert.IsNotNull(MenuSo, "menuSO can't be null");
        
        foreach (var option in MenuSo.AllOptions)
        {
            var newOption = Instantiate(labelOption, root);
            newOption.GetComponent<UIButtonCreator>().Setup(option);
        }
    }
}
