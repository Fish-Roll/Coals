namespace Player.States
{
    public class AttackState : PlayerBaseState
    {
        public AttackState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsDead)
                SwitchState(_ctx.PlayerStateFactory.Death());
            else if((!_ctx.InputSystem.IsAiming || !_ctx.InputSystem.IsAttacking) && _ctx.InputSystem.IsWalking)
                SwitchState(_ctx.PlayerStateFactory.Walk());
            else if(!_ctx.InputSystem.IsAiming || !_ctx.InputSystem.IsAttacking)
                SwitchState(_ctx.PlayerStateFactory.Idle());
        }

        public override void Update()
        {
            _ctx.Attacks[0].Attack(_ctx.InputSystem.GetMouseHitVector(), _ctx.spawnAttackPosition);
            CheckSwitchState();
        }

        public override void InitSubState()
        {
            
        }
    }
}