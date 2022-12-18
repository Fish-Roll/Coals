using System.Collections.Generic;
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
        [SerializeField] private LayerMask ground;

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
        [field: SerializeField] public bool IsJumping { get; set; }
        [field: SerializeField] public bool IsGrounded { get; set; }
        [field: SerializeField] public bool IsOpenChest { get; set; }
        [field: SerializeField] public bool CanOpenChest{ get; set; }
        [field: SerializeField] public bool CanSpeak { get; set; }
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
                _playerInput.PlayerActionMap.Jump.started += OnJump;
                _playerInput.PlayerActionMap.Jump.performed += OnJump;
                _playerInput.PlayerActionMap.Jump.canceled += OnJump;
                _playerInput.PlayerActionMap.UseHeal.performed += OnHealButton;
                _playerInput.PlayerActionMap.OpenChest.performed += OnOpenChest;
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

        public bool CheckIsGround()
        {
            var colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2, ground);
            return colliders.Length != 0;
        }
        private void OnAim(InputAction.CallbackContext context)
        {
            IsAiming = !IsAiming;
            aimPoint.SetActive(IsAiming);
            aimCamera.enabled = IsAiming;
        }

        private void OnHealButton(InputAction.CallbackContext context)
        {
            var inventory = Inventory.Inventory.GetInventory();
            inventory.items.TryGetValue(3, out List<InventoryItem> value);
            if (value?.Count > 0)
            {
                GetComponent<PlayerCharacteristics>().Heal(30);
                inventory.RemoveItem(value[value.Count - 1]);
            }
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
        private void OnOpenChest(InputAction.CallbackContext context) => IsOpenChest = context.ReadValueAsButton();
        private void OnLook(InputAction.CallbackContext context) => look = context.ReadValue<Vector2>();
        private void OnJump(InputAction.CallbackContext context) => IsJumping = context.ReadValueAsButton();

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Item"))
                CanInteract = true;
            else if (other.CompareTag("SpeakingNPC"))
                CanSpeak = true;
            else if (other.CompareTag("Chest"))
                CanOpenChest = true;
            Interact = other.GetComponent<Interactable>();
        }

        private void OnTriggerExit(Collider other)
        {
            CanSpeak = false;
            CanOpenChest = false;
            CanInteract = false;
            Interact = null;
        }
    }
}
