using Core;
using Core.State_Machine;
using Player.States.Attacks;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Move")]
    public class PlayerMoveState : State<PlayerStateMachine>
    {
        [SerializeField, Range(250f, 500f)] private float _speed = 300f;

        private Vector3 _playerInput;
        
        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);
            parent.animations.PlayMove();
        }

        public override void Tick(float deltaTime)
        {
            _playerInput = new Vector3(runner.movement.x, 0, runner.movement.y).normalized;
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            runner.Move(_playerInput * (_speed * fixedDeltaTime));
        }

        public override void ChangeState()
        {
            if (runner.meleeAttackPressed)
            {
                runner.SetState(typeof(PlayerAttackState));
                return;
            }
            
            if (runner.rollPressed)
            {
                runner.SetState(typeof(PlayerRollState));
                return;
            }
            
            if (_playerInput == Vector3.zero)
            {
                runner.SetState(typeof(PlayerIdleState));
            }
        }
    }
}
