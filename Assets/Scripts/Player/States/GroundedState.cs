using UnityEngine;

namespace Player.States
{
    public class GroundedState: PlayerBaseState 
    {
        public GroundedState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
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
            if(_ctx.InputSystem.IsJumping)
                SwitchState(_playerStateFactory.Jump());
        }

        public override void Update()
        {
            CheckSwitchState();
        }

        public override void InitSubState()
        {
            if(_ctx.InputSystem.IsDodging)
                SetSubState(_playerStateFactory.Dodge());
            else if(_ctx.InputSystem.IsWalking && _ctx.InputSystem.IsRunning)
                SetSubState(_playerStateFactory.Run());
            else if(_ctx.InputSystem.IsWalking)
                SetSubState(_playerStateFactory.Walk());
            else
                SetSubState(_playerStateFactory.Idle());
        }
    }
}
