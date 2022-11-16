using Player.States;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputSystem))]
    public class PlayerStateMachine : MonoBehaviour
    {
        public InputSystem InputSystem { get; private set; }
        public Rigidbody Rb { get; private set; }
        [field: SerializeField] public float WalkSpeed { get; set; }
        [field: SerializeField] public float RunSpeed { get; set; }
        [field: SerializeField] public float RotationSpeed { get; set; }
        [field: SerializeField] public float DodgeDistance { get; set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Transform PlayerToRotate { get; set; }
        [SerializeField] private GameObject objectToRotation;
        [SerializeField] private float mouseSensitivity;
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

        private void Update()
        {
            RotateCamera();
            RotatePlayer();
            MoveDirection = InputSystem._inputMoveDirection;
            MoveDirection = InputSystem.ConvertToCameraMovement(MoveDirection);
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

            if (InputSystem.IsWalking)
            {
                transform.rotation = Quaternion.Euler(0, objectToRotation.transform.rotation.eulerAngles.y, 0);
                objectToRotation.transform.localEulerAngles = new Vector3(angles.x, 0, 0);
            }
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
