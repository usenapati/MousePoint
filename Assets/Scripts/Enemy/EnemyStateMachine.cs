using System.Collections;
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

        public bool isFacingRight => _isFacingRight;
        private bool _isFacingRight = false;
        
        protected override void Start()
        {
            base.Start();
            playerStateMachine = FindObjectOfType<PlayerStateMachine>();
            agent = GetComponent<NavMeshAgent>();
        }

        // Methods for Agent
        // Pick Random Coordinate in Radius - Patrol
        public void PatrolRadius(float innerPatrolRadius, float outerPatrolRadius)
        {
            // Generate position in radius
            if (agent.enabled)
            {
                agent.destination = GeneratePositionInRadius(innerPatrolRadius, outerPatrolRadius);
            }
        }
        
        // Follow Player - Chase
        public void FollowPlayer()
        {
            if (agent.enabled)
            {
                agent.SetDestination(playerStateMachine.gameObject.transform.position);
            }
            CheckFlipSprite(agent.velocity);
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
        
        private void CheckFlipSprite(Vector2 velocity)
        {
            if ((!(velocity.x > 0f) || _isFacingRight) && (!(velocity.x < 0f) || !_isFacingRight)) return;
            
            _isFacingRight = !_isFacingRight;
            spriteTransform.Rotate(spriteTransform.rotation.x, -180f, spriteTransform.rotation.z);
        }
        
        // Has Completed Attack
        public void Attack(float attackCooldown)
        {
            agent.enabled = false;
            // Play Animation
            _canAttack = false;
            // Start coroutine, wait X time, reset canAttack to true;
            StartCoroutine(AttackCooldown(attackCooldown));
        }

        private IEnumerator AttackCooldown(float attackCooldown)
        {
            yield return new WaitForSeconds(attackCooldown);
            _canAttack = true;
            agent.enabled = true;
        }
        
        // TODO Handle Death (Disable Navmesh Agent)
    }
}
