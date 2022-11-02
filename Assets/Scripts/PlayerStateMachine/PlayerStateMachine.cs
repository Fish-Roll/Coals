using System;
using UnityEngine;

namespace PlayerStateMachine
{
    [RequireComponent(typeof(InputSystem))]
    public class PlayerStateMachine : MonoBehaviour
    {
        public InputSystem InputSystem { get; private set; }
        public Rigidbody Rb { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; set; }
        [field: SerializeField] public float RotationSpeed { get; set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        public PlayerBaseState CurrentState { get; set; }
        public PlayerStateFactory PlayerStateFactory { get; set; }
        public Vector3 MoveDirection { get; set; }
        public int MoveHash { get; private set; }
        public int DodgeHash { get; private set; }
        public int DeathHash { get; private set; }
        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            InputSystem = GetComponent<InputSystem>();
            Rb.freezeRotation = true;

            MoveHash = Animator.StringToHash("isMove");
            DodgeHash = Animator.StringToHash("isDodge");
            DeathHash = Animator.StringToHash("isDeath");
            
            PlayerStateFactory = new PlayerStateFactory(this);
            CurrentState = PlayerStateFactory.Grounded();
            CurrentState.EnterState();
        }

        private void Update()
        {
            MoveDirection = InputSystem.ConvertToCameraMovement(MoveDirection);
        }
        private void FixedUpdate()
        {
            CurrentState.Updates();
        }
        
    }
}
