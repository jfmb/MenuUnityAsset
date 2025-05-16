using UnityEngine;
using UnityEngine.UI;

public class UIMenuElement : MonoBehaviour
{
    [SerializeField]private Selectable selectable;

    public Selectable SelectableInElement => selectable;

    public bool IsSubOption { get; set; }

    public void SetSelectedElement()
    {
    }

    
    public void DeSelectedElement()
    {
//        selectable.targetGraphic.color = selectable.colors.normalColor;
    }
}
