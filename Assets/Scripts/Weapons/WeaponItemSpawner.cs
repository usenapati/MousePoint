using UnityEngine;

namespace Weapons
{
    public class WeaponItemSpawner : MonoBehaviour
    {
        [SerializeField] private WeaponItem prefab;
        [SerializeField] private float spawnForce = 10f;

        private static WeaponItemSpawner _instance;

        public static WeaponItemSpawner instance => _instance;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public void SpawnNew(WeaponSO weapon, Vector3 position)
        {
            var newItem = Instantiate(prefab, position, Quaternion.identity);
            newItem.CreateInstance(weapon, spawnForce);
        }
    }
}
