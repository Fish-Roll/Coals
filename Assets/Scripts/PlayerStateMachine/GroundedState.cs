using UnityEngine;

namespace PlayerStateMachine
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
            
        }

        public override void Update()
        {
            CheckSwitchState();
        }

        public override void InitSubState()
        {
            if(_ctx.InputSystem.IsMoving)
                SetSubState(_playerStateFactory.Move());
            else if(_ctx.InputSystem.IsDodging)
                SetSubState(_playerStateFactory.Dodge());
            else
                SetSubState(_playerStateFactory.Idle());
        }
    }
}
