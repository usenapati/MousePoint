using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    [RequireComponent(typeof(Health))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _animationSpeed = 10f;

        private Health _health;
        private float _currentHealth;
        private float _maxHealth;

        private void Awake()
        {
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _health.OnSetupHealth += HandleHealthSetup;
            _health.OnTakeDamage += HandleTakeDamage;
        }

        private void OnDisable()
        {
            _health.OnSetupHealth -= HandleHealthSetup;
            _health.OnTakeDamage -= HandleTakeDamage;
        }

        private void HandleHealthSetup(float health)
        {
            _maxHealth = health;
            _currentHealth = _maxHealth;
            _slider.value = _currentHealth;
        }

        private void HandleTakeDamage(float amount, float health)
        {
            _currentHealth = health / _maxHealth;
        }

        private void Update()
        {
            // we just lerp the values. We could use SmoothStep, Lerp or Slerp here
            _slider.value = Mathf.SmoothStep(_slider.value, _currentHealth, _animationSpeed * Time.deltaTime);
        }
    }
}
