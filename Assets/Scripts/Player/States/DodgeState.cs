using TMPro;
using UnityEngine;

namespace Player.States
{
    public class DodgeState : PlayerBaseState
    {
        private Vector3 _playerVelocity;
        public DodgeState(PlayerStateMachine ctx, PlayerStateFactory playerStateFactory) : base(ctx, playerStateFactory)
        {
        }

        public override void EnterState()
        {
            _ctx.Animator.SetBool(_ctx.DodgeHash, true);
            _ctx.Animator.SetBool(_ctx.WalkHash, false);
            Dodge();
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            if(_ctx.InputSystem.IsAiming && _ctx.InputSystem.IsAttacking && _ctx.InputSystem.IsWalking && !_ctx.InputSystem.IsDodging)
                SwitchState(_playerStateFactory.MoveAttack());
            else if(_ctx.InputSystem.IsWalking && !_ctx.InputSystem.IsDodging)
                SwitchState(_playerStateFactory.Walk());
            else if(_ctx.InputSystem.IsAiming && _ctx.InputSystem.IsAttacking)
                SwitchState(_playerStateFactory.Attack());
            else if(!_ctx.InputSystem.IsWalking && !_ctx.InputSystem.IsDodging)
                SwitchState(_playerStateFactory.Idle());
        }

        public override void Update()
        {
            if (_ctx.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0 && !_ctx.Animator.IsInTransition(0))
            {
                _ctx.Rb.velocity = _playerVelocity;
                _ctx.InputSystem.IsDodging = false;
                CheckSwitchState();
            }
        }

        public override void InitSubState()
        {
            
        }

        private void Dodge()
        {
            _playerVelocity = _ctx.Rb.velocity;
            Vector3 dodgeVelocity = _ctx.MoveDirection.normalized;
            dodgeVelocity.x *= _ctx.DodgeDistance;
            dodgeVelocity.z *= _ctx.DodgeDistance;
            _ctx.Rb.velocity = dodgeVelocity;
        }
    }
}
