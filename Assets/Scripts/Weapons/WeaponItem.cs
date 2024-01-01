using Player;
using UnityEngine;

namespace Weapons
{
    public class WeaponItem : MonoBehaviour
    {
        [SerializeField] private WeaponSO weapon;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Rigidbody _rigidbody;
        private Collider _collider;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            spriteRenderer.sprite = weapon.weaponSprite;
            
            _collider.enabled = false;
            
            Invoke(nameof(EnableCollider), 1f);
        }

        private void EnableCollider()
        {
            _collider.enabled = true;
        }

        public void CreateInstance(WeaponSO weaponSO, float force)
        {
            weapon = weaponSO;
            spriteRenderer.sprite = weapon.weaponSprite;

            _rigidbody.AddForce((Vector3.up + Vector3.right) * force, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && other.TryGetComponent(out PlayerStateMachine stateMachine))
            {
                stateMachine.weapons.AttachWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}
