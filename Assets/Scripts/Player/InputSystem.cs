using InteractWithWorld;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputSystem : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Vector2 _readMoveDirection;
        private Camera _camera;
        public Vector2 _look;
        public Vector3 _inputMoveDirection;
        public InteractableObject Interact { get; set; }
        [field: SerializeField] public bool IsDead { get; set; }
        [field: SerializeField] public bool IsWalking { get; set; }
        [field: SerializeField] public bool IsRunning { get; set; }
        [field: SerializeField] public bool IsDodging { get; set; }
        [field: SerializeField] public bool IsInteracting { get; set; }
        [field: SerializeField] public bool CanInteract { get; set; }
        
        private void Awake()
        {
            _camera = Camera.main;
        }
        private void OnEnable()
        {
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.PlayerActionMap.Walk.performed += OnWalk;
                _playerInput.PlayerActionMap.Run.performed += OnRun;
                _playerInput.PlayerActionMap.Dodge.performed += OnDodge;
                _playerInput.PlayerActionMap.Interact.performed += OnInteract;
                _playerInput.PlayerActionMap.Look.performed += OnLook;
                _playerInput.PlayerActionMap.Look.canceled += OnLook;
            }
            _playerInput.Enable();
        }
        private void OnDisable()
        {
            _playerInput.Disable();
            // if (!IsDead)
            // {
            //     _playerInput.PlayerActionMap.Walk.performed -= OnWalk;
            //     _playerInput.PlayerActionMap.Run.performed -= OnRun;
            //     _playerInput.PlayerActionMap.Dodge.performed -= OnDodge;
            //     _playerInput.PlayerActionMap.Interact.performed -= OnInteract;
            //     _playerInput.PlayerActionMap.Look.performed -= OnLook;
            // }
        }
        private void OnWalk(InputAction.CallbackContext context) {
            _readMoveDirection = context.ReadValue<Vector2>();
            IsWalking = _readMoveDirection.x != 0 || _readMoveDirection.y != 0;
            _inputMoveDirection.x = _readMoveDirection.x;
            _inputMoveDirection.y = 0;
            _inputMoveDirection.z = _readMoveDirection.y;
        }
        private void OnRun(InputAction.CallbackContext context) => IsRunning = context.ReadValueAsButton();
        private void OnDodge(InputAction.CallbackContext context) => IsDodging = context.ReadValueAsButton();
        private void OnInteract(InputAction.CallbackContext context) => IsInteracting = context.ReadValueAsButton();
        private void OnLook(InputAction.CallbackContext context) => _look = context.ReadValue<Vector2>();
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                CanInteract = true;
                Interact = other.GetComponent<InteractableObject>();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Item"))
            {
                CanInteract = false;
                Interact = null;
            }
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
}
