using Extensions;
using UnityEngine;

namespace Player.States.Attacks
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Player/Attack", order = 0)]
    public class AttackSO : ScriptableObject
    {
        [Tooltip("The name of the animation to use")]
        public string attackName;

        [Tooltip("How big is this attack")] 
        public Bounds AttackBounds;

        [Tooltip("The offset from the player")]
        public Vector3 BoundsOffset;

        [Tooltip("The damage to perform on the target")]
        public float Damage;

        [Tooltip("Which Layer is this target")]
        public LayerMask TargetMask;

        /// <summary>
        /// Calculates the bounds from the player
        /// </summary>
        /// <param name="player">The "caller" game object</param>
        /// <param name="isFacingRight"></param>
        /// <returns></returns>
        private Bounds GetBoundsRelativeToPlayer(Transform player, bool isFacingRight)
        {
            var bounds = AttackBounds;
            var xValue = isFacingRight ? 1 : -1;
            var offset = BoundsOffset;
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
            // we call our extension method
            bounds.DrawBounds(1);

            return Physics.OverlapBox(bounds.center, bounds.extents / 2f, Quaternion.identity, TargetMask);
        }
    }
}
