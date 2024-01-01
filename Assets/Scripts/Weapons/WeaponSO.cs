using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Player/Weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        public GameObject prefab;
        public float damage;
        public Sprite weaponSprite;
        
        [Tooltip("How big is this attack")] 
        public Bounds bounds;

        public Bounds GetBoundsRelativeToPlayer(Transform player)
        {
            var tempBounds = bounds;
            tempBounds.center = player.position;
            return tempBounds;
        }
    }
}
