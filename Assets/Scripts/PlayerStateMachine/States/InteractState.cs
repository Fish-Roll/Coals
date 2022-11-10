namespace PlayerStateMachine.States
{
    public class InteractState : PlayerBaseState
    {
        public InteractState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Animator.SetBool(_ctx.InteractHash, true);
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsWalking && (!_ctx.InputSystem.CanInteract || !_ctx.InputSystem.IsInteracting))
                SwitchState(_ctx.PlayerStateFactory.Walk());
            else if(!_ctx.InputSystem.CanInteract || !_ctx.InputSystem.IsInteracting)
                SwitchState(_ctx.PlayerStateFactory.Idle());
        }

        public override void Update()
        {
            if (_ctx.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0 && !_ctx.Animator.IsInTransition(0))
            {
                _ctx.InputSystem.IsInteracting = false;
                CheckSwitchState();
            }
        }

        public override void InitSubState()
        {
            
        }
        
    }
}