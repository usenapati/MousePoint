using System;
using System.Collections;
using Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    
    public class PlayerHealth : Health
    {
        
        [Header("Scene Transition Settings")] 
        [SerializeField] private Image fadeOverlay;
        [SerializeField] private float fadeDuration = 2f;
        [SerializeField] private string cutScene = "TottenTownBeta";
        
        
        public override bool Damage(float amount)
        {
            bool result = base.Damage(amount);
            GameEventsManager.instance.playerEvents.PlayerDamaged();
            return result;
        }

        // Player Input
        // Player State Machine - Sprites and Animation
        // Scene Manager/Respawn Manager - Respawn player in same scene or different scene
        protected override void Die()
        {
            GameEventsManager.instance.playerEvents.PlayerDeath();
            // Disable Player Input
            // Death Animation
            // Hide Player Sprites
            // UI Death Sequence
            // Transition to other scene if var is set (Combat zone to Lodge Scene)
            StartCoroutine(TransitionSequence());
            
        }

        public void Respawn()
        {
            GameEventsManager.instance.playerEvents.PlayerSpawn();
            // Show Player Sprites
            // Reset Player's Health
            // Enable Player Input
        }
        
        private IEnumerator TransitionSequence()
        {
            // Fade out the main menu
            yield return StartCoroutine(FadeImage(fadeOverlay, 1));

            // Load and play the BoatScene
            UnityEngine.SceneManagement.SceneManager.LoadScene(cutScene);
            // Note: The playback and completion of BoatScene should be handled in its own scene
        }

        private IEnumerator FadeImage(Image image, float targetAlpha)
        {
            float startAlpha = image.color.a;
            float time = 0;

            while (time < fadeDuration)
            {
                Color newColor = image.color;
                newColor.a = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
                image.color = newColor;
                time += Time.deltaTime;
                yield return null;
            }

            image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);
        }
    }
}

