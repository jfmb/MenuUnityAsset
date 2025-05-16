using Services.EventQueue;
using Services.EventQueue.Events.ScriptableObjects;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    [SerializeField] private EventId gameStartsEventId;
    [SerializeField] private EventId isPlayerDrivingEventId;
    [SerializeField] private Transform currentCamera;
    
    private bool _isPlayerDriving;

    [SerializeField] private float acceleration = 5;
    [SerializeField] private float deceleration = 7;
    [SerializeField] private float maxSpeed = 3;

    private Vector3 _velocity;
    
    [SerializeField] private Transform targetPosition;  // destination Transform
    [SerializeField] private float movementSpeed = 5f;  // control the speed of the movement

    private Coroutine _activeMoveRoutine;

    
    private void Start()
    {
        var gameStartsEvent =
            (SimpleEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameStartsEventId);
        gameStartsEvent.SimpleEventSender += OnNewGameStartsEvent;
    }

    private void OnNewGameStartsEvent()
    {
        MoveCameraToTarget();
    }
    
    private void MoveCameraToTarget()
    {
        //TODO: move the camera with an animation
        currentCamera.position = targetPosition.position;
        currentCamera.rotation = targetPosition.rotation;
        currentCamera.parent = targetPosition;
        
        SubscribeToPlayerDrivingEvents();
    }

    private void SubscribeToPlayerDrivingEvents()
    {
        var isPlayerDrivingEvent =
            (BooleanEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isPlayerDrivingEventId);
        isPlayerDrivingEvent.BooleanEventSender += OnNewIsPlayerDrivingEvent;
    }
    
    private void OnNewIsPlayerDrivingEvent(object source, BooleanEventData args)
    {
        _isPlayerDriving= args.Value;
        
        Debug.Log("Is player driving: " + _isPlayerDriving);
    }

    private void Update()
    {
        // Accelerate
        if (_isPlayerDriving) {
            _velocity.z += acceleration * Time.deltaTime;
            _velocity.z = Mathf.Clamp(_velocity.z, -maxSpeed, maxSpeed);
        }
        // Decelerate
        else if (_velocity.z != 0) {
            float decel = deceleration * Time.deltaTime;

            if (Mathf.Abs(_velocity.z) <= decel)
                _velocity.z= 0;
            else
                _velocity.z -= Mathf.Sign(_velocity.z) * decel;
        }

        // Apply movement
        transform.Translate(_velocity * Time.deltaTime);
    }

    private void OnDestroy()
    {
        var gameStartsEvent =
            (SimpleEvent)ServiceLocator.GetService<EventQueue>().GetEventWithEventId(gameStartsEventId);
        gameStartsEvent.SimpleEventSender -= OnNewGameStartsEvent;
        
        var isPlayerDrivingEvent =
            (BooleanEvent) ServiceLocator.GetService<EventQueue>().GetEventWithEventId(isPlayerDrivingEventId);
        isPlayerDrivingEvent.BooleanEventSender -= OnNewIsPlayerDrivingEvent;

    }
}
