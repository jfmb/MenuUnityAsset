using UnityEngine;
using UnityEngine.InputSystem;

public class InputForGeneral : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    
    private InputActionMap _actionMap;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += DeviceChange;
    }
    
        
    private void DeviceChange(InputDevice device, InputDeviceChange change)
    {
        
        if (change == InputDeviceChange.Added)
        {
//            SubscribeToEvents();
            Debug.Log("Device Connected: " + device);
        }
        else if (change == InputDeviceChange.Removed)
        {
//            UnsubscribeToEvents();
            Debug.Log("Device Disconnected: " + device);
        }
    }
    
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
        InputSystem.onDeviceChange -= DeviceChange;
        
        var generalActionMap = inputActions.FindActionMap("General");
        var navigateAction = generalActionMap.FindAction("Settings");

        navigateAction.performed -= PerformSettings;

        navigateAction.Disable();
    }
}
