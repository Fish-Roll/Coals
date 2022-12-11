using UnityEngine;

namespace Player.States
{
    public class JumpState : PlayerBaseState
    {
        public JumpState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
            IsRootState = true;
            InitSubState();
        }

        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsGrounded && !_ctx.InputSystem.IsJumping)
                SwitchState(_playerStateFactory.Grounded());
        }

        public override void Update()
        {
            Jump();
            CheckSwitchState();
        }

        public override void InitSubState()
        {
            if(_ctx.InputSystem.IsWalking && _ctx.InputSystem.IsRunning)
                SetSubState(_playerStateFactory.Run());
            else if(_ctx.InputSystem.IsWalking)
                SetSubState(_playerStateFactory.Walk());
            else
                SetSubState(_playerStateFactory.Idle());
        }

        private void Jump()
        {
            _ctx.Rb.velocity = new Vector3(0, _ctx.JumpForce, 0);
        }
    }
}