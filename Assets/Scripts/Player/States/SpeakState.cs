namespace Player.States
{
    public class SpeakState : PlayerBaseState
    {
        public SpeakState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.InputSystem.Interact.Interact();
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsWalking && (!_ctx.InputSystem.CanSpeak || !_ctx.InputSystem.IsInteracting))
                SwitchState(_ctx.PlayerStateFactory.Walk());
            else if(!_ctx.InputSystem.CanSpeak || !_ctx.InputSystem.IsInteracting)
                SwitchState(_ctx.PlayerStateFactory.Idle());
        }

        public override void Update()
        {
            _ctx.InputSystem.IsInteracting = false;
            CheckSwitchState();
        }

        public override void InitSubState()
        {
            
        }
    }
}