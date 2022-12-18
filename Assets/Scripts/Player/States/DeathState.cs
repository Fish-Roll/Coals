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
            
            _ctx.InputSystem.enabled = false;
            _ctx.InputSystem.IsDodging = false;
            _ctx.InputSystem.IsInteracting = false;
            _ctx.InputSystem.IsRunning = false;
            _ctx.InputSystem.IsWalking = false;
            
            _ctx.HUD.enabled = false;
            _ctx.DeathScreen.enabled = true;
        }

        public override void ExitState()
        {
            _ctx.InputSystem.enabled = true;
        }

        public override void CheckSwitchState()
        {
            if(!_ctx.InputSystem.IsDead)
                SwitchState(_playerStateFactory.Idle());
        }

        public override void Update()
        {
            if (_ctx.Characteristics.Health > 0)
                _ctx.InputSystem.IsDead = false;
            CheckSwitchState();
        }

        public override void InitSubState()
        {
            
        }
    }
}