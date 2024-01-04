using Core.State_Machine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Chase")]
    public class EnemyChaseState : State<EnemyStateMachine>
    {
        [SerializeField] private bool isInEnemyAttackRadius = false;
        [SerializeField] private float enemyAttackRadius = 3f;
        
        // Enemy Speed
        
        public override void Tick(float deltaTime)
        {
            // Follow and look at the enemy
            throw new System.NotImplementedException();
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeState()
        {
            // If player is in range, attack the player
            // If the player is not in sight for a period of time, patrol
        }
    }
}
