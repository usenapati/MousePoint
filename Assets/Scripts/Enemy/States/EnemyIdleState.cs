using Core.State_Machine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Idle")]
    public class EnemyIdleState : State<EnemyStateMachine>
    {
        public override void Enter(EnemyStateMachine parent)
        {
            base.Enter(parent);
            // Idle Anims
        }

        public override void Tick(float deltaTime)
        {
            
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            
        }

        public override void ChangeState()
        {
            // Check if player is in radius, if so chase them
        }
    }
}
