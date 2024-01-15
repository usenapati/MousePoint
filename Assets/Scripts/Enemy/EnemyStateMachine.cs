using Core.State_Machine;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyStateMachine: StateMachine<EnemyStateMachine>
    {
        [SerializeField] private Transform spriteTransform;
        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private NavMeshAgent agent;
        
        // Player Reference
        private PlayerStateMachine playerStateMachine;
        
        // Enemy Stats
        public bool canAttack => _canAttack;
        private bool _canAttack = true;

        
        
        protected override void Start()
        {
            base.Start();
            playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        }

        // Methods for Agent
        // Pick Random Coordinate in Radius - Patrol
        public void PatrolRadius(float innerPatrolRadius, float outerPatrolRadius)
        {
            // Generate position in radius
            agent.destination = GeneratePositionInRadius(innerPatrolRadius, outerPatrolRadius);
        }
        
        // Follow Player - Chase
        public void FollowPlayer()
        {
            agent.SetDestination(playerStateMachine.gameObject.transform.position);
        }

        public bool IsEnemyInRadius(float detectionRadius)
        {
            return Vector3.Distance(playerStateMachine.gameObject.transform.position, transform.position) <
                   detectionRadius;
        }
        
        public bool IsEnemyInGoalRadius(float goalRadius)
        {
            return Vector3.Distance(agent.destination, transform.position) <
                   goalRadius;
        }

        private Vector3 GeneratePositionInRadius(float innerRadius, float outerRadius)
        {
            Vector3 randomPosition = Random.insideUnitCircle;
            return transform.position + randomPosition.normalized * innerRadius + randomPosition * (outerRadius - innerRadius);
        }
        
        // Has Completed Attack
        private void Attack()
        {
            // Play Animation
            _canAttack = false;
            // Start coroutine, wait X time, reset canAttack to true;
        }
    }
}
