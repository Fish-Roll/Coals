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
        [SerializeField] private GameObject objectToRotation;
        [SerializeField] private float mouseSensitivity;
        [field: SerializeField] public Transform PlayerToRotate { get; set; }
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
            RotateCamera();
            MoveDirection = InputSystem._inputMoveDirection;
            MoveDirection = InputSystem.ConvertToCameraMovement(MoveDirection);
        }

        private void LateUpdate()
        {
            
        }

        private void FixedUpdate()
        {
            CurrentState.Updates();
        }
        public void RotateCamera()
        {
            objectToRotation.transform.rotation *= Quaternion.AngleAxis(InputSystem._look.x * mouseSensitivity * Time.deltaTime, Vector3.up);
            objectToRotation.transform.rotation *= Quaternion.AngleAxis(-InputSystem._look.y * mouseSensitivity* Time.deltaTime, Vector3.right);

            Vector3 angles = objectToRotation.transform.localEulerAngles;
            angles.z = 0;
            float angle = objectToRotation.transform.localEulerAngles.x;
            if (angle > 180 && angle < 340)
            {
                angles.x = 340;
            }
            else if(angle < 180 && angle > 40)
            {
                angles.x = 40;
            }
            objectToRotation.transform.localEulerAngles = angles;

            if (InputSystem.IsMoving)
            {
                transform.rotation = Quaternion.Euler(0, objectToRotation.transform.rotation.eulerAngles.y, 0);
                objectToRotation.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }
        }

    }
}
