using Core.State_Machine;
using Health;
using Extensions;
using UnityEngine;

namespace Player.States.Attacks
{
    [CreateAssetMenu(menuName = "States/Player/Attack")]
    public class PlayerAttackState : State<PlayerStateMachine>
    {
        [SerializeField] private AttackSO[] attackDatas;
        
        private int _currentAttackIndex;
        private AttackSO _attackData;
        private float _previousFrameTime;

        private void OnValidate()
        {
            // this will automatically change the next combo index according to the 
            // ordering of the attack scriptable objects on the editor
            for (var i = 0; i < attackDatas.Length; i++)
            {
                // if it's the last one, the next one is the first one so we loop the animations
                if (i == attackDatas.Length - 1)
                {
                    attackDatas[i].nextComboIndex = 0;
                }
                else
                {
                    attackDatas[i].nextComboIndex = i + 1;
                }
            }
        }

        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);

            // we need to gather which is the current attack we want
            _attackData = attackDatas[_currentAttackIndex];

            parent.animations.PlayAttack(_attackData.attackName);
        }

        public override void Tick(float deltaTime)
        {
            var normalizedTime = runner.animations.GetNormalizedTime();

            if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
            {
                if (runner.meleeAttackPressed)
                {
                    TryComboAttack(normalizedTime);
                }
            }
            else
            {
                // go back to idle
                runner.SetState(typeof(PlayerIdleState));
            }

            _previousFrameTime = normalizedTime;
        }

        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);

            if (triggerType != AnimationTriggerType.HitBox) return;

            var colliders = _attackData.Hit(runner.transform, runner.isFacingRight);

            PerformDamage(colliders);
        }

        private void TryComboAttack(float normalizedTime)
        {
            if (normalizedTime < _attackData.comboAttackTime)
            {
                return;
            }

            // we clone the current state using the extension we created
            var newCombo = this.Clone();

            // we set the current index to the next one in the list
            newCombo._currentAttackIndex = _attackData.nextComboIndex;

            runner.SetState(newCombo);
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
            if (runner.movement.sqrMagnitude != 0)
            {
                runner.SetState(typeof(PlayerMoveState));
            }
        }
    }
}
