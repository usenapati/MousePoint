using UnityEngine;
using Weapons;

namespace Player.States.Attacks
{
    public class PlayerWeapons
    {
        public bool hasWeapon => _lastWeapon != null && _lastWeapon.prefab != null;
        public float weaponDamage => hasWeapon ? _lastWeapon.damage : 0;

        private Transform _playerHand;
        private WeaponSO _lastWeapon;

        public PlayerWeapons(Transform playerHand)
        {
            _playerHand = playerHand;
        }

        public void AttachWeapon(WeaponSO weapon)
        {
            if (weapon.prefab == null) return;

            if (_playerHand.childCount > 0)
                RemoveWeapon();

            _lastWeapon = weapon;
            Object.Instantiate(weapon.prefab, _playerHand);
        }

        private void RemoveWeapon()
        {
            WeaponItemSpawner.instance.SpawnNew(_lastWeapon, _playerHand.root.position);

            _lastWeapon = null;
            RemoveChildren();
        }

        private void RemoveChildren()
        {
            while (_playerHand.childCount > 0) {
                Object.DestroyImmediate(_playerHand.GetChild(0).gameObject);
            }
        }
    }
}
