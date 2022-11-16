using Player.States;
namespace Player
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

        public PlayerBaseState Walk()
        {
            return new WalkState(_ctx, this);
        }
        public PlayerBaseState Run()
        {
            return new RunState(_ctx, this);
        }

        public PlayerBaseState Grounded()
        {
            return new GroundedState(_ctx, this);
        }

        public PlayerBaseState Interact()
        {
            return new InteractState(_ctx, this);
        }
    }
}
