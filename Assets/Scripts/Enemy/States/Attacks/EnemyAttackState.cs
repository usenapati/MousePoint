using Core.State_Machine;
using Health;
using Player.States.Attacks;
using UnityEngine;

namespace Enemy.States.Attacks
{
    [CreateAssetMenu(menuName = "States/Enemy/Attack")]
    public class EnemyAttackState : State<EnemyStateMachine>
    {
        // Attack Cooldown
        [SerializeField] private float enemyAttackCooldown = 3f;
        [SerializeField] private AttackSO[] attackDatas;
        
        private int _currentAttackIndex;
        private AttackSO _attackData;
        private float _previousFrameTime;

        public override void Enter(EnemyStateMachine parent)
        {
            base.Enter(parent);
            _attackData = attackDatas[_currentAttackIndex];
            
            var colliders = _attackData.Hit(runner.transform, runner.isFacingRight);
            
            runner.Attack(enemyAttackCooldown);
            PerformDamage(colliders);
        }

        public override void Tick(float deltaTime)
        {
        }
        
        private void PerformDamage(Collider[] colliders)
        {
            if (colliders.Length <= 0) return;

            foreach (var col in colliders)
            {
                if (col.CompareTag("Player") && col.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(_attackData.damage);
                }
            }
        }

        public override void FixedTick(float fixedDeltaTime)
        {
        }

        public override void ChangeState()
        {
            if (!runner)
                return;
            // Back to Chase after attack is finished
            if (!runner.canAttack)
            {
                runner.SetState(typeof(EnemyChaseState));
            }
        }
    }
}
