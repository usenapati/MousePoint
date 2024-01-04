using Core.State_Machine;
using UnityEngine;

namespace Enemy.States.Attacks
{
    [CreateAssetMenu(menuName = "States/Enemy/Attack")]
    public class EnemyAttackState : State<EnemyStateMachine>
    {
        // Attack Damage
        // Attack Cooldown
        
        public override void Tick(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeState()
        {
            // Back to Chase after attack is finished
        }
    }
}
