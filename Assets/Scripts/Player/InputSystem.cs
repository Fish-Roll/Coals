using Cinemachine;
using InteractWithWorld;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class InputSystem : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera aimCamera;
        [SerializeField] private GameObject aimPoint;
        [SerializeField] private LayerMask layerMask;
        private PlayerInput _playerInput;
        private Vector2 _readMoveDirection;
        public Vector3 inputMoveDirection;
        public Vector2 look;
        public Interactable Interact { get; set; }
        [field: SerializeField] public bool IsDead { get; set; }
        [field: SerializeField] public bool IsWalking { get; set; }
        [field: SerializeField] public bool IsRunning { get; set; }
        [field: SerializeField] public bool IsDodging { get; set; }
        [field: SerializeField] public bool IsInteracting { get; set; }
        [field: SerializeField] public bool CanInteract { get; set; }
        [field: SerializeField] public bool IsAiming { get; set; }
        [field: SerializeField] public bool IsAttacking { get; set; }
        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (_playerInput == null)
            {
                _playerInput = new PlayerInput();
                _playerInput.PlayerActionMap.Walk.performed += OnWalk;
                _playerInput.PlayerActionMap.Run.performed += OnRun;
                _playerInput.PlayerActionMap.Dodge.performed += OnDodge;
                _playerInput.PlayerActionMap.Aim.performed += OnAim;
                _playerInput.PlayerActionMap.Attack.performed += OnAttack;
                _playerInput.PlayerActionMap.Attack.started += OnAttack;
                _playerInput.PlayerActionMap.Attack.canceled += OnAttack;
                _playerInput.PlayerActionMap.Interact.performed += OnInteract;
                _playerInput.PlayerActionMap.Look.performed += OnLook;
                _playerInput.PlayerActionMap.Look.canceled += OnLook;
            }
            _playerInput.Enable();
        }
        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
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

        public Vector3 GetMouseHitVector()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            Physics.Raycast(ray, out RaycastHit hit, layerMask);
            return hit.point;
        }
        private void OnAim(InputAction.CallbackContext context)
        {
            IsAiming = !IsAiming;
            aimPoint.SetActive(IsAiming);
            aimCamera.enabled = IsAiming;
        }
        private void OnAttack(InputAction.CallbackContext context)
        {
            IsAttacking = context.ReadValueAsButton();
        }
        private void OnWalk(InputAction.CallbackContext context) {
            _readMoveDirection = context.ReadValue<Vector2>();
            IsWalking = _readMoveDirection.x != 0 || _readMoveDirection.y != 0;
            inputMoveDirection.x = _readMoveDirection.x;
            inputMoveDirection.y = 0;
            inputMoveDirection.z = _readMoveDirection.y;
        }
        private void OnRun(InputAction.CallbackContext context) => IsRunning = context.ReadValueAsButton();
        private void OnDodge(InputAction.CallbackContext context) => IsDodging = context.ReadValueAsButton();
        private void OnInteract(InputAction.CallbackContext context) => IsInteracting = context.ReadValueAsButton();
        private void OnLook(InputAction.CallbackContext context) => look = context.ReadValue<Vector2>();
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Item") || other.CompareTag("SpeakingNPC"))
            {
                CanInteract = true;
                Interact = other.GetComponent<Interactable>();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            CanInteract = false;
            Interact = null;
        }
    }
}
