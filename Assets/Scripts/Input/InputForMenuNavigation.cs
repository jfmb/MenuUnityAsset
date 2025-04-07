using System;
using System.Collections;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputForMenuNavigation : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private float delayForGamepadJoystick;
    
    [SerializeField] private EventId uiLeftEventId;
    [SerializeField] private EventId uiRightEventId;
    [SerializeField] private EventId usingMouseEventId;

    [SerializeField] private KeyboardChecker keyboardChecker;
    [SerializeField] private MouseChecker mouseChecker;
    [SerializeField] private GamepadChecker gampepadChecker;

    private InputActionMap _actionMap;
    private bool _joystickCanBeUsed = true;

    private bool _isAlreadyUsingMouse;
    private bool _isAlreadyUsingKeyboard;
    private bool _isAlreadyUsingGamepad;

    private void Start()
    {
        _actionMap = inputActions.FindActionMap("UI");

        var navigateActionMap = _actionMap.FindAction("Navigate");
        navigateActionMap.performed += PerformNavigation;
        navigateActionMap.Enable();
    }

    private void PerformNavigation(InputAction.CallbackContext ctx)
    {
        var direction = ctx.ReadValue<Vector2>();
        
        if (direction.x == -1)
        {
            Debug.Log("Joystick Left");
            DoThingsWhenInputIsLeft();
        }
        
        if (direction.x == 1)
        {
            Debug.Log("Joystick Right");
            DoThingsWhenInputIsRight();
        }
    }

    private void DoThingsWhenInputIsLeft()
    {
        if (!_joystickCanBeUsed)
        {
            return;
        }
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(uiLeftEventId, EventArgs.Empty);

        StartCoroutine(WaitToBeAbleToUseJoystickAgain());
    }

    IEnumerator WaitToBeAbleToUseJoystickAgain()
    {
        _joystickCanBeUsed = false;

        yield return new WaitForSeconds(delayForGamepadJoystick);
        _joystickCanBeUsed = true;
    }
    
    private void DoThingsWhenInputIsRight()
    {        
        if (!_joystickCanBeUsed)
        {
            return;
        }
        
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(uiRightEventId, EventArgs.Empty);
        StartCoroutine(WaitToBeAbleToUseJoystickAgain());
    }

    private void Update()
    {
        CheckKeyboard();
        CheckMouse();
        CheckGamepad();
    }

    private void CheckMouse()
    {
        if (!mouseChecker.IsUsingMouse())
        {
            return;
        }

        Cursor.visible = true;
        _isAlreadyUsingKeyboard = false;
        _isAlreadyUsingGamepad = false;
        
        if (_isAlreadyUsingMouse)
        {
            return;
        }

        _isAlreadyUsingMouse = true;
        mouseChecker.SendIsUsingMouseEvent();
    }

    private void CheckKeyboard()
    {
        if (!keyboardChecker.IsUsingKeyboard())
        {
            return;
        }
        _isAlreadyUsingMouse = false;
        _isAlreadyUsingGamepad = false;
        Cursor.visible = false;

        if (_isAlreadyUsingKeyboard)
        {
            return;
        }
        _isAlreadyUsingKeyboard = true;
        
        keyboardChecker.SendIsUsingKeyboardEvent();
    }

    private void CheckGamepad()
    {
        if (!gampepadChecker.IsGamepadTouched())
        {
            return;
        }
        Cursor.visible = false;

        _isAlreadyUsingMouse = false;
        _isAlreadyUsingKeyboard = false;
        
        if (_isAlreadyUsingGamepad)
        {
            return;
        }
        
        _isAlreadyUsingGamepad = true;
        gampepadChecker.SendIsUsingMouseEvent();
    }


    private void OnDisable()
    {
        var navigateAction = _actionMap.FindAction("Navigate");
        navigateAction.performed -= PerformNavigation;
        navigateAction.Disable();
        
        // var clickAction = _actionMap.FindAction("Click");
        // clickAction.performed -= PerformClick;
        // clickAction.Disable();
    }
}
