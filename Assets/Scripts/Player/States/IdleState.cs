namespace Player.States
{
    public class IdleState : PlayerBaseState 
    {
        public IdleState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Animator.SetBool(_ctx.WalkHash, false);
            _ctx.Animator.SetBool(_ctx.InteractHash, false);
            _ctx.Animator.SetBool(_ctx.DeathHash, false);
            _ctx.Animator.SetBool(_ctx.OpenChestHash, false);
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsDead)
                SwitchState(_playerStateFactory.Death());
            else if(_ctx.InputSystem.IsAiming && _ctx.InputSystem.IsAttacking)
                SwitchState(_playerStateFactory.Attack());
            else if(_ctx.InputSystem.IsWalking)
                SwitchState(_playerStateFactory.Walk());
            else if(_ctx.InputSystem.IsInteracting && _ctx.InputSystem.CanInteract)
                SwitchState(_playerStateFactory.Interact());
            else if(_ctx.InputSystem.IsOpenChest && _ctx.InputSystem.CanOpenChest)
                SwitchState(_playerStateFactory.OpenChest());
            else if(_ctx.InputSystem.CanSpeak && _ctx.InputSystem.IsInteracting)
                SwitchState(_playerStateFactory.Speak());
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
