using Core.State_Machine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Patrol")]
    public class EnemyPatrolState : State<EnemyStateMachine>
    {
        // Enemy Speed
        // Enemy Detection
        [SerializeField] private float innerPatrolRadius = 5;
        [SerializeField] private float outerPatrolRadius = 15f;
        [SerializeField] private float goalRadius = 1f;
        [SerializeField] private float enemyDetectionRadius = 10f;
        [SerializeField] private float patrolTimer = 10;
        private float _waitTime;

        public override void Enter(EnemyStateMachine parent)
        {
            base.Enter(parent);
            // Move to a random point in level (Should be ground) and walk to it
            runner.PatrolRadius(innerPatrolRadius, outerPatrolRadius);
            _waitTime = patrolTimer;
        }

        public override void Tick(float deltaTime)
        {
            // If enemy doesn't reach location in given time, generate a new position
            if (runner.IsEnemyInGoalRadius(goalRadius) || _waitTime <= 0)
            {
                runner.PatrolRadius(innerPatrolRadius, outerPatrolRadius);
                _waitTime = patrolTimer;
            }

            _waitTime -= deltaTime;
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            
        }

        public override void ChangeState()
        {
            // Check if player is in radius and in sight, if so chase them
            if (runner.IsEnemyInRadius(enemyDetectionRadius))
            {
                runner.SetState(typeof(EnemyChaseState));
            }
        }
    }
}
