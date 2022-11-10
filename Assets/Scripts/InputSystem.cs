using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Vector2 _readMoveDirection;
    private Camera _camera;
    
    public Vector2 _look;
    public Vector3 _inputMoveDirection;
    public bool IsWalking { get; set; }
    public bool IsRunning { get; set; }
    public bool IsDodging { get; set; }
    [field: SerializeField]public bool IsInteracting { get; set; }
    [field: SerializeField]public bool CanInteract { get; set; }
    private void Awake()
    {
        _camera = Camera.main;
    }
    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.PlayerActionMap.Walk.performed += context =>
            {
                _readMoveDirection = context.ReadValue<Vector2>();
                IsWalking = _readMoveDirection.x != 0 || _readMoveDirection.y != 0;
                _inputMoveDirection.x = _readMoveDirection.x;
                _inputMoveDirection.y = 0;
                _inputMoveDirection.z = _readMoveDirection.y;
            };
            _playerInput.PlayerActionMap.Run.performed += context =>
            {
                IsRunning = context.ReadValueAsButton();
            };
            _playerInput.PlayerActionMap.Dodge.performed += context =>
            {
                IsDodging = context.ReadValueAsButton();
            };
            _playerInput.PlayerActionMap.Interact.performed += context =>
            {
                IsInteracting = context.ReadValueAsButton();
            };
            _playerInput.PlayerActionMap.Look.started += context =>
            {
                _look = context.ReadValue<Vector2>();
            };
            _playerInput.PlayerActionMap.Look.performed += context =>
            {
                _look = context.ReadValue<Vector2>();
            };
            _playerInput.PlayerActionMap.Look.canceled += context =>
            {
                _look = context.ReadValue<Vector2>();
            };
        }
        _playerInput.PlayerActionMap.Walk.Enable();
        _playerInput.PlayerActionMap.Run.Enable();
        _playerInput.PlayerActionMap.Interact.Enable();
        _playerInput.PlayerActionMap.Dodge.Enable();
        _playerInput.PlayerActionMap.Look.Enable();
    }
    private void OnDisable()
    {
        _playerInput.PlayerActionMap.Walk.Disable();
        _playerInput.PlayerActionMap.Run.Disable();
        _playerInput.PlayerActionMap.Interact.Disable();
        _playerInput.PlayerActionMap.Dodge.Disable();
        _playerInput.PlayerActionMap.Look.Disable();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Item"))
            CanInteract = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
            CanInteract = false;
    }

    public Vector3 ConvertToCameraMovement(Vector3 moveDirection)
    {
        float yMoveValue = moveDirection.y;
    
        Vector3 cameraForward = _camera.transform.forward;
        Vector3 cameraRight = _camera.transform.right;
        
        cameraForward.y = 0;
        cameraRight.y = 0;
    
        Vector3 CameraForwardZ = _inputMoveDirection.z * cameraForward.normalized;
        Vector3 CameraForwardX = _inputMoveDirection.x * cameraRight.normalized;
        
        Vector3 resultMove = CameraForwardX + CameraForwardZ;
        resultMove.y = yMoveValue;
        return resultMove;
    }
    
}
