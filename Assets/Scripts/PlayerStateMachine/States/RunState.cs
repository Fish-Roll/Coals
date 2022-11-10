namespace PlayerStateMachine.States
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
            _ctx.Animator.SetBool(_ctx.DodgeHash, false);
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsDodging)
                SwitchState(_playerStateFactory.Dodge());
            else if(!_ctx.InputSystem.IsRunning)
                SwitchState(_playerStateFactory.Walk());
        }

        public override void Update()
        {
            _ctx.Rb.velocity = _ctx.MoveDirection.normalized * _ctx.RunSpeed;
            CheckSwitchState();
        }

        public override void InitSubState()
        {
            
        }
    }
}