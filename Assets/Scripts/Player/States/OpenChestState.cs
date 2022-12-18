namespace Player.States
{
    public class OpenChestState : PlayerBaseState
    {
        public OpenChestState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Animator.SetBool(_ctx.OpenChestHash, true);
            _ctx.InputSystem.Interact.Interact();
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsWalking && (!_ctx.InputSystem.CanOpenChest || !_ctx.InputSystem.IsOpenChest))
                SwitchState(_ctx.PlayerStateFactory.Walk());
            else if(!_ctx.InputSystem.CanOpenChest || !_ctx.InputSystem.IsOpenChest)
                SwitchState(_ctx.PlayerStateFactory.Idle());
        }

        public override void Update()
        {
            if (_ctx.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0 && !_ctx.Animator.IsInTransition(0))
            {
                _ctx.InputSystem.IsOpenChest = false;
                CheckSwitchState();
            }
        }

        public override void InitSubState()
        {
            
        }
    }
}