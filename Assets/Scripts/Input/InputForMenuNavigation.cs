using UnityEngine;
using UnityEngine.InputSystem;

public class InputForMenuNavigation : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    
    private InputActionMap actionMap;
    
    private void Start()
    {
        actionMap = inputActions.FindActionMap("UI");

        var navigateAction = actionMap.FindAction("Navigate");

        navigateAction.performed += PerformNavigation;

        navigateAction.Enable();
    }
    
    private void PerformNavigation(InputAction.CallbackContext ctx)
    {
        var direction = ctx.ReadValue<Vector2>();
        if (direction.y == 1) 
            Debug.Log("Navigate Up performed");
        if (direction.y == -1)
            Debug.Log("Navigate Down performed");
    }

    private void OnDisable()
    {
        var navigateAction = actionMap.FindAction("Navigate");

        navigateAction.performed -= PerformNavigation;
        navigateAction.Disable();
    }
}
