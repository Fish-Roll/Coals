namespace Player.States
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
            _ctx.Animator.SetBool(_ctx.InteractHash, false);
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
            else if(_ctx.InputSystem.CanInteract && _ctx.InputSystem.IsInteracting)
                SwitchState(_playerStateFactory.Interact());
            else if(!_ctx.InputSystem.IsWalking)
                SwitchState(_playerStateFactory.Idle());
            else if(_ctx.InputSystem.IsAiming && _ctx.InputSystem.IsAttacking)
                SwitchState(_playerStateFactory.MoveAttack());
            else if(_ctx.InputSystem.IsRunning)
                SwitchState(_playerStateFactory.Run());
        }

        public override void Update()
        {
            MovePlayer();
            CheckSwitchState();
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
