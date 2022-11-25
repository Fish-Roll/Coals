using System;
using Attack;
using Player.States;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputSystem),
        typeof(ThirdPersonCamera))]
    public class PlayerStateMachine : MonoBehaviour
    {
        public InputSystem InputSystem { get; private set; }
        public ThirdPersonCamera PlayerCamera { get; private set; }
        public Rigidbody Rb { get; private set; }
        [field: SerializeField] public float WalkSpeed { get; set; }
        [field: SerializeField] public float RunSpeed { get; set; }
        [field: SerializeField] public float RotationSpeed { get; set; }
        [field: SerializeField] public float DodgeDistance { get; set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Transform PlayerToRotate { get; set; }
        [field: SerializeField] public BaseAttack[] Attacks { get; set; }
        public Transform[] spawnAttackPosition;
        public PlayerCharacteristics Characteristics { get; set; }
        public PlayerBaseState CurrentState { get; set; }
        public PlayerStateFactory PlayerStateFactory { get; set; }
        public Vector3 MoveDirection { get; set; }
        public int WalkHash { get; private set; }
        public int RunHash { get; private set; }
        public int DodgeHash { get; private set; }
        public int DeathHash { get; private set; }
        public int InteractHash { get; private set; }
        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            InputSystem = GetComponent<InputSystem>();
            PlayerCamera = GetComponent<ThirdPersonCamera>();
            Characteristics = GetComponent<PlayerCharacteristics>();
            Rb.freezeRotation = true;
            WalkHash = Animator.StringToHash("isWalk");
            RunHash = Animator.StringToHash("isRun");
            DodgeHash = Animator.StringToHash("isDodge");
            InteractHash = Animator.StringToHash("isInteract");
            DeathHash = Animator.StringToHash("isDeath");
            PlayerStateFactory = new PlayerStateFactory(this);
            CurrentState = PlayerStateFactory.Grounded();
            CurrentState.EnterState();
        }
        private void LateUpdate()
        {
            if (!InputSystem.IsAiming)
            {
                transform.rotation = PlayerCamera.RotateCamera(InputSystem.look, transform.rotation, InputSystem.IsWalking);
                RotatePlayer();
            }
            else
            {
                transform.rotation = PlayerCamera.RotateCamera(InputSystem.look, transform.rotation);
            }
        }
        private void Update()
        {
            MoveDirection = InputSystem.inputMoveDirection;
            MoveDirection = PlayerCamera.ConvertToCameraMovement(MoveDirection, InputSystem.inputMoveDirection);
        }
        private void FixedUpdate()
        {
            if (Characteristics.Health == 0)
                InputSystem.IsDead = true;
            CurrentState.Updates();
        }
        private void RotatePlayer()
        {
            Vector3 positionToLookAt;
            positionToLookAt.x = MoveDirection.x;
            positionToLookAt.y = 0f;
            positionToLookAt.z = MoveDirection.z;
            Quaternion curRotation = PlayerToRotate.rotation;
            if (InputSystem.IsWalking)
            {
                Quaternion rotate = Quaternion.LookRotation(positionToLookAt);
                PlayerToRotate.rotation = Quaternion.Slerp(curRotation, rotate, RotationSpeed * Time.deltaTime);
            }
        }
    }
}
