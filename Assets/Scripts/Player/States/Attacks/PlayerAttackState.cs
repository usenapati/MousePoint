using Core.State_Machine;
using Health;
using Extensions;
using UnityEngine;

namespace Player.States.Attacks
{
    [CreateAssetMenu(menuName = "States/Player/Attack")]
    public class PlayerAttackState : State<PlayerStateMachine>
    {
        [SerializeField] private AttackSO[] _attackDatas;
        
        private int _currentAttackIndex;
        private AttackSO _attackData;
        private float _previousFrameTime;

        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);
            // Play Animations Here
            parent.Animations.PlayAttack(_attackData.attackName);
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);

            if (triggerType == AnimationTriggerType.FinishAttack)
            {
                if (runner.MeleeAttackPressed)
                {
                    // Next Combo
                }
                else
                {
                    runner.SetState(typeof(PlayerIdleState));
                }
            }

            if (triggerType != AnimationTriggerType.HitBox) return;
            
            // Check for Collsions
            var colliders = _attackData.Hit(runner.transform, runner.IsFacingRight);

            PerformDamage(colliders);
        }

        private void PerformDamage(Collider[] colliders)
        {
            if (colliders.Length <= 0) return;

            foreach (var collider in colliders)
            {
                Debug.Log($"Colliding with {collider.gameObject.name}");
                // Damage Object Here
                if (collider.TryGetComponent(out IDamageable damageable))
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
            if (runner.Movement.sqrMagnitude != 0)
            {
                runner.SetState(typeof(PlayerMoveState));
            }
        }
    }
}
