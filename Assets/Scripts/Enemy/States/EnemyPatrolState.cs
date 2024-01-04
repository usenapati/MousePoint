using Core.State_Machine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Patrol")]
    public class EnemyPatrolState : State<EnemyStateMachine>
    {
        // Enemy Speed
        // Enemy Detection
        
        public override void Tick(float deltaTime)
        {
            // Move to a random point in level (Should be ground) and walk to it
            throw new System.NotImplementedException();
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeState()
        {
            // Check if player is in radius and in sight, if so chase them
        }
    }
}
