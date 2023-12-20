using System;
using System.Collections;
using UnityEngine;

namespace Health
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private float _inmuneTime = 1f;
    
        public event Action<float, float> OnTakeDamage;
        public event Action OnDie;
        public event Action<float> OnSetupHealth;
    
        private bool _isDead => _health == 0;
        private float _health;
        private WaitForSeconds _waitingTime;
        private bool _isInmune;
    
        private void Awake()
        {
            _health = _maxHealth;
            _waitingTime = new WaitForSeconds(_inmuneTime);
        }
    
        private void Start()
        {
            OnSetupHealth?.Invoke(_health);
        }
    
        public void Setup(int maxHealth)
        {
            _maxHealth = maxHealth;
            _health = maxHealth;
    
            OnSetupHealth?.Invoke(_health);
        }
    
        public void Heal(float amount)
        {
            _health = Mathf.Min(_health + amount, _maxHealth);
            OnTakeDamage?.Invoke(0, _health);
        }
    
        public bool Damage(float amount)
        {
            if (_isDead || _isInmune) { return false; }
    
            _health = Mathf.Max(_health - amount, 0);
    
            OnTakeDamage?.Invoke(amount, _health);
    
            StartCoroutine(Immune());
    
            if (_isDead)
            {
                Die();
            }
    
            return true;
        }
    
        private void Die()
        {
            _isInmune = true;
            OnDie?.Invoke();
            Destroy(transform.root.gameObject);
        }
    
        private IEnumerator Immune()
        {
            _isInmune = true;
            yield return _waitingTime;
            _isInmune = false;
        }
    }
}

