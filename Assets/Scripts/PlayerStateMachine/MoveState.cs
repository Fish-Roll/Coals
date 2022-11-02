using UnityEngine;

namespace PlayerStateMachine
{
    public class MoveState : PlayerBaseState
    {
        public MoveState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Animator.SetBool(_ctx.MoveHash, true);
            _ctx.Animator.SetBool(_ctx.DodgeHash, false);
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsDodging)
                SwitchState(_playerStateFactory.Dodge());
            else if(!_ctx.InputSystem.IsMoving)
                SwitchState(_playerStateFactory.Idle());
        }

        public override void Update()
        {
            CheckSwitchState();
            MovePlayer();
        }

        public override void InitSubState()
        {
        }

        private void MovePlayer()
        {
            _ctx.Rb.velocity = _ctx.MoveDirection.normalized * _ctx.MoveSpeed;
            RotatePlayer();
        }

        private void RotatePlayer()
        {
            Vector3 positionToLookAt;
            positionToLookAt.x = _ctx.MoveDirection.x;
            positionToLookAt.y = 0f;
            positionToLookAt.z = _ctx.MoveDirection.z;
            Quaternion curRotation = _ctx.transform.rotation;
            if (_ctx.InputSystem.IsMoving)
            {
                Quaternion rotate = Quaternion.LookRotation(positionToLookAt);
                _ctx.transform.rotation = Quaternion.Slerp(curRotation, rotate, _ctx.RotationSpeed * Time.deltaTime);
            }
        }
    }
}
