using UnityEngine;
using UnityEngine.InputSystem;

public class InputForPlayerTest : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    
    private InputActionMap actionMap;
    
    private void Start()
    {
        actionMap = inputActions.FindActionMap("Player");

        var navigateAction = actionMap.FindAction("Move");

        navigateAction.performed += PerformNavigation;

        navigateAction.Enable();
    }


    private void PerformNavigation(InputAction.CallbackContext ctx)
    {
        var direction = ctx.ReadValue<Vector2>();
        if (direction.y > 0) 
            Debug.Log("Move Up performed");
        if (direction.y < 0)
            Debug.Log("Move Down performed");
    }

    private void OnDisable()
    {
        var navigateAction = actionMap.FindAction("Move");

        navigateAction.performed -= PerformNavigation;
        navigateAction.Disable();
    }
}
