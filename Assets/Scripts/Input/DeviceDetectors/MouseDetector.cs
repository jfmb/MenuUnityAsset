using System;
using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseDetector : MonoBehaviour
{
    [SerializeField] private EventId usingMouseEventId;

    private Vector2 _currentMousePosition;
    private Vector2 _lastMousePosition;

    private void Awake()
    {
        _lastMousePosition = Mouse.current.position.ReadValue();
    }

    private void Start()
    {
        GetCurrentMousePosition();
    }

    private void GetCurrentMousePosition()
    {
        _currentMousePosition = Mouse.current.position.ReadValue();
    }

    private void Update()
    {
        _lastMousePosition = _currentMousePosition;
        GetCurrentMousePosition();
    }
    
    public bool IsUsingMouse()
    {
        return _currentMousePosition != _lastMousePosition;
    }
    
    public void SendIsUsingMouseEvent()
    {
        ServiceLocator.GetService<EventQueue>().EnqueueEvent(usingMouseEventId, EventArgs.Empty);
    }
}
