using UnityEngine;

namespace PlayerStateMachine
{
    public class IdleState : PlayerBaseState 
    {
        public IdleState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Animator.SetBool(_ctx.MoveHash, false);
            _ctx.Animator.SetBool(_ctx.DodgeHash, false);
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsMoving)
                SwitchState(_playerStateFactory.Move());
            else if(_ctx.InputSystem.IsDodging)
                SwitchState(_playerStateFactory.Dodge());
        }

        public override void Update()
        {
            CheckSwitchState();
        }

        public override void InitSubState()
        {
            
        }
    }
}
