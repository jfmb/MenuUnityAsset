using UnityEngine;
using UnityEngine.InputSystem;

public class InputForGeneral : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    
    private InputActionMap actionMap;

    private void SetupGeneralInput()
    {
        var generalActionMap = inputActions.FindActionMap("General");
        var navigateAction = generalActionMap.FindAction("Settings");

        navigateAction.performed += PerformSettings;

        navigateAction.Enable();
    }
    private void PerformSettings(InputAction.CallbackContext obj)
    {
        Debug.Log("Settings pressed");
    }
    
    private void OnDisable()
    {
        var generalActionMap = inputActions.FindActionMap("General");
        var navigateAction = generalActionMap.FindAction("Settings");

        navigateAction.performed -= PerformSettings;

        navigateAction.Disable();
    }
}
