using Core.State_Machine;
using UnityEngine;

namespace Enemy.States.Attacks
{
    [CreateAssetMenu(menuName = "States/Enemy/Attack")]
    public class EnemyAttackState : State<EnemyStateMachine>
    {
        // Attack Damage
        [SerializeField] private float enemyAttackDamage = 3f;
        // Attack Cooldown
        [SerializeField] private float enemyAttackCooldown = 3f;
        
        public override void Tick(float deltaTime)
        {
        }

        public override void FixedTick(float fixedDeltaTime)
        {
        }

        public override void ChangeState()
        {
            // Back to Chase after attack is finished
            if (!runner.canAttack)
            {
                runner.SetState(typeof(EnemyChaseState));
            }
        }
    }
}
