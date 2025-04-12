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
    [SerializeField] private EventId isGamepadConnectedId;

    [SerializeField] private KeyboardDetector keyboardDetector;
    [SerializeField] private MouseDetector mouseDetector;
    [SerializeField] private GamepadDetector gampepadDetector;

    private InputActionMap _actionMap;
    private bool _joystickCanBeUsed = true;

    private bool _isAlreadyUsingMouse;
    private bool _isAlreadyUsingKeyboard;
    private bool _isAlreadyUsingGamepad;

    private bool _isGamepadConnected;
    
    private void Start()
    {
        _actionMap = inputActions.FindActionMap("UI");

        _isGamepadConnected = gampepadDetector.IsGamepadConnected();

        var navigateActionMap = _actionMap.FindAction("Navigate");
        navigateActionMap.performed += PerformHorizontalNavigation;
        navigateActionMap.Enable();

        var isGamepadConnectedEvent =
            (BooleanEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isGamepadConnectedId);
        isGamepadConnectedEvent.BooleanEventSender += OnNewIsGamePadConnectedEvent;
    }

    private void OnNewIsGamePadConnectedEvent(object source, BooleanEventData args)
    {
        _isGamepadConnected = args.Value;

        if (!_isGamepadConnected)
        {
            _isAlreadyUsingGamepad = false;
            return;
        }
        
        DoThingsWhenIsUsingGamepad();
    }

    private void PerformHorizontalNavigation(InputAction.CallbackContext ctx)
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
        if (!mouseDetector.IsUsingMouse())
        {
            return;
        }

        DoThingsWhenIsUsingMouse();
    }

    private void DoThingsWhenIsUsingMouse()
    {
        _isAlreadyUsingKeyboard = false;
        _isAlreadyUsingGamepad = false;
        
        if (_isAlreadyUsingMouse)
        {
            return;
        }

        Debug.Log("Mouse is detected and is not already in use");
        
        _isAlreadyUsingMouse = true;
        mouseDetector.SendIsUsingMouseEvent();
    }

    private void CheckKeyboard()
    {
        if (!keyboardDetector.IsUsingKeyboard())
        {
            return;
        }

        DoThingsWhenIsUsingKeyboard();
    }

    private void DoThingsWhenIsUsingKeyboard()
    {
        _isAlreadyUsingMouse = false;
        _isAlreadyUsingGamepad = false;

        if (_isAlreadyUsingKeyboard)
        {
            return;
        }
        
        Debug.Log("Keyboard is detected and is not already in use");

        _isAlreadyUsingKeyboard = true;
        keyboardDetector.SendIsUsingKeyboardEvent();
    }

    private void CheckGamepad()
    {
        if (!_isGamepadConnected)
        {
            return;
        }
        
        if (!gampepadDetector.IsGamepadTouched())
        {
            return;
        }

        DoThingsWhenIsUsingGamepad();
    }

    private void DoThingsWhenIsUsingGamepad()
    {
        _isAlreadyUsingMouse = false;
        _isAlreadyUsingKeyboard = false;
        
        if (_isAlreadyUsingGamepad)
        {
            return;
        }

        _isAlreadyUsingGamepad = true;
        gampepadDetector.SendIsUsingGamepadEvent();
    }
    
    private void OnDisable()
    {        
        _actionMap = inputActions.FindActionMap("UI");
        var navigateAction = _actionMap.FindAction("Navigate");
        navigateAction.performed -= PerformHorizontalNavigation;
        navigateAction.Disable();
    }
    
    public void OnDestroy()
    {
        var isGamepadConnectedEvent =
            (BooleanEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isGamepadConnectedId);
        isGamepadConnectedEvent.BooleanEventSender -= OnNewIsGamePadConnectedEvent;
    }
}
