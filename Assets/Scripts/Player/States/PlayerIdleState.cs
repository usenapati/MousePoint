using Core.State_Machine;
using Player.States.Attacks;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Idle")]
    public class PlayerIdleState : State<PlayerStateMachine>
    {
        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);
            runner.Move(Vector2.zero);
            parent.animations.PlayIdle();
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void FixedTick(float fixedDeltaTime)
        {
        }

        public override void ChangeState()
        {
            if (runner.meleeAttackPressed)
            {
                runner.SetState(typeof(PlayerAttackState));
                return;
            }
            
            if (runner.movement.sqrMagnitude != 0)
            {
                runner.SetState(typeof(PlayerMoveState));
            }
        }
    }
}
