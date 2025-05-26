using System.Collections.Generic;
using UnityEngine;

public class UISubOptionsMenuBuilder : MonoBehaviour
{
//    [SerializeField] private List<SubOptionSO> allSubOptionsSo;
    [SerializeField] private UISubOptionCreator subOption;
    
    private void Setup(List<SubOptionSO> allSubOptionsSo)
    {
        foreach (var element in allSubOptionsSo)
        {
            subOption.Setup(element);
        }
    }
}
