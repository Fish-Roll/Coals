namespace PlayerStateMachine
{
    public class PlayerStateFactory
    {
        private PlayerStateMachine _ctx;
        public PlayerStateFactory(PlayerStateMachine ctx)
        {
            _ctx = ctx;
        }
        public PlayerBaseState Idle()
        {
            return new IdleState(_ctx, this);
        }

        public PlayerBaseState Dodge()
        {
            return new DodgeState(_ctx, this);
        }

        public PlayerBaseState Move()
        {
            return new MoveState(_ctx, this);
        }

        public PlayerBaseState Grounded()
        {
            return new GroundedState(_ctx, this);
        }
    }
}
