using System;
using System.Collections.Generic;
using Core.Managers;
using UnityEngine;

namespace Health.UI
{
    public class HealthHUDManager : MonoBehaviour
    {
        // Player Health
        [SerializeField] private GameObject heartPrefab;
        private PlayerHealth _playerHealth;
        private List<HealthHeart> hearts = new List<HealthHeart>();

        private void OnEnable()
        {
            GameEventsManager.instance.playerEvents.OnPlayerDamaged += DrawHearts;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.playerEvents.OnPlayerDamaged -= DrawHearts;
        }

        // Start is called before the first frame update
        void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
            DrawHearts();
        }

        public void DrawHearts()
        {
            ClearHearts();
            float maxHealth = _playerHealth.GetMaxHealth() % 2;
            int heartsToMake = (int)(_playerHealth.GetMaxHealth() / 2 + maxHealth);
            for (int i = 0; i < heartsToMake; i++)
            {
                CreateEmptyHeart();
            }

            for (int i = 0; i < hearts.Count; i++)
            {
                int heartStatusRemainder = (int)Mathf.Clamp(_playerHealth.GetCurrentHealth() - (i * 2), 0, 2);
                hearts[i].SetHeartImage((HeartStatus) heartStatusRemainder);
            }
        }

        public void CreateEmptyHeart()
        {
            GameObject newHeart = Instantiate(heartPrefab, transform, true);

            HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
            heartComponent.SetHeartImage(HeartStatus.Empty);
            hearts.Add(heartComponent);
        }

        public void ClearHearts()
        {
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
            hearts.Clear();
        }
    }
}
