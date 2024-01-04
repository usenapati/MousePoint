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
        private bool _canAttack;

        
        
        protected override void Start()
        {
            base.Start();
            playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        }

        // Methods for Agent
        // Pick Random Coordinate in Radius - Patrol
        public void PatrolRadius(float patrolRadius)
        {
            // Generate position in radius
            agent.destination = GeneratePositionInRadius(patrolRadius);
            // If enemy doesn't reach location in given time, generate a new position
        }
        
        // Follow Player - Chase
        public void FollowPlayer()
        {
            agent.destination = playerStateMachine.gameObject.transform.position;
        }

        public bool IsEnemyInRadius(float detectionRadius)
        {
            return false;
        }

        private Vector3 GeneratePositionInRadius(float radius)
        {
            return transform.position;
        }
        
        // Has Completed Attack
    }
}
