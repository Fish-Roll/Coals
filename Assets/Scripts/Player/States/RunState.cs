using Player;
using Player.States;

namespace Player.States
{
    public class RunState : PlayerBaseState
    {
        public RunState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Animator.SetBool(_ctx.RunHash, true);
            _ctx.Animator.SetBool(_ctx.WalkHash, true);
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsDead)
                SwitchState(_playerStateFactory.Death());
            else if(_ctx.InputSystem.IsDodging)
                SwitchState(_playerStateFactory.Dodge());
            else if(!_ctx.InputSystem.IsRunning)
                SwitchState(_playerStateFactory.Walk());
            else if(!_ctx.InputSystem.IsWalking)
                SwitchState(_playerStateFactory.Walk());
        }

        public override void Update()
        {
            MovePlayer();
            CheckSwitchState();
        }
        private void MovePlayer()
        {
            _ctx.Rb.velocity = _ctx.MoveDirection.normalized * _ctx.RunSpeed;
        }

        public override void InitSubState()
        {
            
        }
    }
}