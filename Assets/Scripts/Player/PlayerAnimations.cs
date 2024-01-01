using UnityEngine;

namespace Player
{
    public class PlayerAnimations
    {
        private Animator _animator;

        // this needs to match the names on the Animator view
        private int PLAYER_IDLE = Animator.StringToHash("Idle");
        private int PLAYER_MOVE = Animator.StringToHash("Move");
        private int PLAYER_ROLL = Animator.StringToHash("Roll");

        private float _transitionDuration = .1f;

        public PlayerAnimations(Animator animator)
        {
            _animator = animator;
        }

        public void PlayIdle()
        {
            _animator.CrossFadeInFixedTime(PLAYER_IDLE, _transitionDuration);
        }

        public void PlayMove()
        {
            _animator.CrossFadeInFixedTime(PLAYER_MOVE, _transitionDuration);
        }

        public void PlayRoll()
        {
            _animator.CrossFadeInFixedTime(PLAYER_ROLL, _transitionDuration);
        }
        
        public void PlayAttack(string attackName)
        {
            _animator.CrossFadeInFixedTime(attackName, _transitionDuration);
        }
        
        public float GetNormalizedTime()
        {
            var currentInfo = _animator.GetCurrentAnimatorStateInfo(0);
            var nextInfo = _animator.GetNextAnimatorStateInfo(0);

            if (_animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
            {
                return nextInfo.normalizedTime;
            }

            if (!_animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
            {
                return currentInfo.normalizedTime;
            }

            return 0f;
        }
    }
}
