using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Health
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private float immuneTime = 1f;
        //Delete After Alpha
        [SerializeField] private AudioSource hitSoundSource;
        public event Action<float, float> OnTakeDamage;
        public event Action OnDie;
        public event Action<float> OnSetupHealth;

        protected bool isDead => _health == 0;
        protected float _health;
        protected WaitForSeconds waitingTime;
        protected bool isImmune;
    
        private void Awake()
        {
            _health = maxHealth;
            waitingTime = new WaitForSeconds(immuneTime);
        }
    
        private void Start()
        {
            OnSetupHealth?.Invoke(_health);
        }
    
        public void Setup(int initialMaxHealth)
        {
            this.maxHealth = initialMaxHealth;
            _health = initialMaxHealth;
    
            OnSetupHealth?.Invoke(_health);
        }
    
        public void Heal(float amount)
        {
            _health = Mathf.Min(_health + amount, maxHealth);
            OnTakeDamage?.Invoke(0, _health);
        }
    
        public bool Damage(float amount)
        {
            if (isDead || isImmune) { return false; }
    
            _health = Mathf.Max(_health - amount, 0);
    
            OnTakeDamage?.Invoke(amount, _health);
    
            StartCoroutine(Immune());
            //Delete After Alpha
            hitSoundSource.Play();
            if (isDead)
            {
                Die();
            }
    
            return true;
        }

        protected virtual void Die()
        {
            isImmune = true;
            OnDie?.Invoke();
            
        }
    
        private IEnumerator Immune()
        {
            isImmune = true;
            yield return waitingTime;
            isImmune = false;
        }
    }
}

