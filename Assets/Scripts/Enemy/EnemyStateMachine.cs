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

        protected override void Start()
        {
            base.Start();
            playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        }

        // Methods for Agent
        // Pick Random Coordinate in Radius - Patrol
        // Follow Player - Chase
        
    }
}
