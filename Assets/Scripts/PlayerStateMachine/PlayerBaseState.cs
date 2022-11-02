namespace PlayerStateMachine
{
    public abstract class PlayerBaseState
    {
        protected bool IsRootState = false;
        protected PlayerStateMachine _ctx;
        protected PlayerStateFactory _playerStateFactory;
        private PlayerBaseState _subState;
        private PlayerBaseState _superState;
        public PlayerBaseState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory)
        {
            _ctx = ctx;
            _playerStateFactory = playerStateFactory;
        }
        
        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void CheckSwitchState();
        public abstract void Update();
        public abstract void InitSubState();

        public void Updates()
        {
            Update();
            _subState?.Updates();
        }
        public void SwitchState(PlayerBaseState newState)
        {
            ExitState();
            newState.EnterState();
            _ctx.CurrentState = newState;
        }
        
        public void SetSubState(PlayerBaseState newSubState)
        {
            _subState = newSubState;
            newSubState.SetSuperState(this);
        }
        
        public void SetSuperState(PlayerBaseState newSuperState)
        {
            _superState = newSuperState;
        }
    }
}
