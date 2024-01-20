using Extensions;
using UnityEngine;
using Weapons;

namespace Player.States.Attacks
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Player/Attack", order = 0)]
    public class AttackSO : ScriptableObject
    {
        [Tooltip("The name of the animation to use")]
        public string attackName;

        [Tooltip("How big is this attack")] 
        public Bounds attackBounds;

        [Tooltip("The offset from the player")]
        public Vector3 boundsOffset;

        [Tooltip("The damage to perform on the target")]
        public float damage;

        [Tooltip("Which Layer is this target")]
        public LayerMask targetMask;
        
        [Tooltip("Which attack index should we transition to")]
        [HideInInspector]
        public int nextComboIndex;

        [Tooltip("The time to start checking for next combo time")]
        public float comboAttackTime;
        
        [Tooltip("The current weapon we are holding")]
        public WeaponSO weapon;

        /// <summary>
        /// Calculates the bounds from the player
        /// </summary>
        /// <param name="player">The "caller" game object</param>
        /// <param name="isFacingRight"></param>
        /// <returns></returns>
        private Bounds GetBoundsRelativeToPlayer(Transform player, bool isFacingRight)
        {
            var bounds = attackBounds;
            var xValue = isFacingRight ? 1 : -1;
            var offset = boundsOffset;
            offset.x *= xValue;
            bounds.center = player.position + offset;

            return bounds;
        }

        /// <summary>
        /// Performs a Physics query to check for colliders in a specific point
        /// </summary>
        /// <param name="origin">The point to perform the check</param>
        /// <param name="isFacingRight">Is the attacker facing right or not</param>
        /// <returns></returns>
        public Collider[] Hit(Transform origin, bool isFacingRight)
        {
            var bounds = GetBoundsRelativeToPlayer(origin, isFacingRight);
						
            // we need to check if we have a weapon and if so we need to add the bounds
            // of the weapon into the attack itself
            if (weapon != null)
            {
                bounds.Encapsulate(weapon.GetBoundsRelativeToPlayer(origin));
            }
            
            // we call our extension method
            bounds.DrawBounds(1);

            // ReSharper disable once Unity.PreferNonAllocApi
            return Physics.OverlapBox(bounds.center, bounds.extents / 2f, Quaternion.identity, targetMask);
        }
    }
}
