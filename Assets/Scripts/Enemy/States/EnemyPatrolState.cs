using Core.State_Machine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Patrol")]
    public class EnemyPatrolState : State<EnemyStateMachine>
    {
        // Enemy Speed
        // Enemy Detection
        [SerializeField] private float patrolRadius = 15f;

        public override void Enter(EnemyStateMachine parent)
        {
            base.Enter(parent);
            runner.PatrolRadius(patrolRadius);
        }

        public override void Tick(float deltaTime)
        {
            // Move to a random point in level (Should be ground) and walk to it
            
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            
        }

        public override void ChangeState()
        {
            // Check if player is in radius and in sight, if so chase them
        }
    }
}
