using Core.State_Machine;
using Enemy.States.Attacks;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Chase")]
    public class EnemyChaseState : State<EnemyStateMachine>
    {
        [SerializeField] private float enemyAttackRadius = 3f;
        [SerializeField] private float enemyDetectionRadius = 10f;
        [SerializeField] private bool canPatrol = true;
        // Last seen player 
        // Enemy Speed
        
        
        public override void Tick(float deltaTime)
        {
            if (!runner)
                return;
            runner.FollowPlayer();
        }

        public override void FixedTick(float fixedDeltaTime)
        {
        }

        public override void ChangeState()
        {
            if (!runner)
                return;
            // If player is in range, attack the player
            
            if (runner.IsEnemyInRadius(enemyAttackRadius) && runner.canAttack)
            {
                runner.SetState(typeof(EnemyAttackState));
            }
            
            // If the player is not in sight for a period of time, patrol
            if (!runner.IsEnemyInRadius(enemyDetectionRadius) && canPatrol) // Last seen player
            {
                runner.SetState(typeof(EnemyPatrolState));
            }

            if (!runner.IsEnemyInRadius(enemyDetectionRadius) && !canPatrol)
            {
                runner.SetState(typeof(EnemyIdleState));
            }
        }
    }
}
