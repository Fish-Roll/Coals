namespace Player.States
{
    public class DeathState : PlayerBaseState
    {
        public DeathState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Animator.SetBool(_ctx.InteractHash, false);
            _ctx.Animator.SetBool(_ctx.RunHash, false);
            _ctx.Animator.SetBool(_ctx.WalkHash, false);
            _ctx.Animator.SetBool(_ctx.DeathHash, true);
            _ctx.enabled = false;
        }

        public override void ExitState()
        {
            _ctx.enabled = true;
        }

        public override void CheckSwitchState()
        {
            if(!_ctx.InputSystem.IsDead)
                SwitchState(_playerStateFactory.Idle());
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