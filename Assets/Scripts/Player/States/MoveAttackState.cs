namespace Player.States
{
    public class MoveAttackState : PlayerBaseState
    {
        public MoveAttackState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
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
            if (_ctx.InputSystem.IsDead)
                SwitchState(_ctx.PlayerStateFactory.Death());
            else if(_ctx.InputSystem.IsDodging && _ctx.InputSystem.IsWalking)
                SwitchState(_ctx.PlayerStateFactory.Dodge());
            else if ((!_ctx.InputSystem.IsAiming || !_ctx.InputSystem.IsAttacking) && _ctx.InputSystem.IsWalking)
                SwitchState(_ctx.PlayerStateFactory.Walk());
            else if (_ctx.InputSystem.IsAiming && _ctx.InputSystem.IsAttacking && !_ctx.InputSystem.IsWalking)
                SwitchState(_ctx.PlayerStateFactory.Attack());
        }

        public override void Update()
        {
            _ctx.Attacks[0].Attack(_ctx.InputSystem.GetMouseHitVector(), _ctx.spawnAttackPosition);
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