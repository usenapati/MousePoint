using Core.State_Machine;
using UnityEngine;

namespace Enemy
{
    public class EnemyStateMachine: StateMachine<EnemyStateMachine>
    {
        [SerializeField] private Transform spriteTransform;
        [SerializeField] private Rigidbody rigidBody;
        
        
    }
}
