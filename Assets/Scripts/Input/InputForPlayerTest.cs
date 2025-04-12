using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputForPlayerTest : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private EventId isPlayerDriving;
    
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
        {
            var args = new BooleanEventData(true);
            ServiceLocator.GetService<EventQueue>().EnqueueEvent(isPlayerDriving, args);
            //Move car forward            
        }
        else
        {
            var args = new BooleanEventData(false);
            ServiceLocator.GetService<EventQueue>().EnqueueEvent(isPlayerDriving, args);
        }

    }

    private void OnDisable()
    {
        var navigateAction = actionMap.FindAction("Move");

        navigateAction.performed -= PerformNavigation;
        navigateAction.Disable();
    }
}
