using Core.State_Machine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Idle")]
    public class EnemyIdleState : State<EnemyStateMachine>
    {
        [SerializeField] private bool isInEnemyRadius = false;
        [SerializeField] private float enemyDetectionRadius = 10f;
        
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
            // Check if player is in radius and in sight, if so chase them
        }
    }
}
