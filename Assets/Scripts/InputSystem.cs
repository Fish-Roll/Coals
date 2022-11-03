using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Vector2 _readMoveDirection;
    public Vector2 _look;
    public Vector3 _inputMoveDirection;
    private Camera _camera;
    public bool IsMoving { get; set; }
    [field: SerializeField] public bool IsDodging { get; set; }

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.MovementActionMap.MovementAction.performed += context =>
            {
                _readMoveDirection = context.ReadValue<Vector2>();
                IsMoving = _readMoveDirection.x != 0 || _readMoveDirection.y != 0;
                _inputMoveDirection.x = _readMoveDirection.x;
                _inputMoveDirection.y = 0;
                _inputMoveDirection.z = _readMoveDirection.y;
            };
            _playerInput.MovementActionMap.Dodge.performed += context =>
            {
                IsDodging = true;
            };
            _playerInput.MovementActionMap.Look.started += context =>
            {
                _look = context.ReadValue<Vector2>();
            };
            _playerInput.MovementActionMap.Look.performed += context =>
            {
                _look = context.ReadValue<Vector2>();
            };
            _playerInput.MovementActionMap.Look.canceled += context =>
            {
                _look = context.ReadValue<Vector2>();
            };
        }
        _playerInput.MovementActionMap.MovementAction.Enable();
        _playerInput.MovementActionMap.Dodge.Enable();
        _playerInput.MovementActionMap.Look.Enable();
    }
    private void OnDisable()
    {
        _playerInput.MovementActionMap.MovementAction.Disable();
        _playerInput.MovementActionMap.Dodge.Disable();
        _playerInput.MovementActionMap.Look.Disable();
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
