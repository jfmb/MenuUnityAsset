using UnityEngine;
using UnityEngine.UI;

public class UIMenuElement : MonoBehaviour
{
    [SerializeField]private Selectable selectable;

    public Selectable SelectableInElement => selectable;

    public void SetSelectedElement()
    {
//        selectable.targetGraphic.color = selectable.colors.selectedColor;
//        selectable.Select();
    }

    
    public void DeSelectedElement()
    {
//        selectable.targetGraphic.color = selectable.colors.normalColor;
    }
}
