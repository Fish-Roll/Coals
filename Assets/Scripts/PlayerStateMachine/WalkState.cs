using UnityEngine;

namespace PlayerStateMachine
{
    public class WalkState : PlayerBaseState
    {
        public WalkState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Animator.SetBool(_ctx.WalkHash, true);
            _ctx.Animator.SetBool(_ctx.RunHash, false);
            _ctx.Animator.SetBool(_ctx.DodgeHash, false);
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsDodging)
                SwitchState(_playerStateFactory.Dodge());
            else if(_ctx.InputSystem.IsRunning)
                SwitchState(_playerStateFactory.Run());
            else if(!_ctx.InputSystem.IsWalking)
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
            _ctx.Rb.velocity = _ctx.MoveDirection.normalized * _ctx.WalkSpeed;
        }
    }
}
