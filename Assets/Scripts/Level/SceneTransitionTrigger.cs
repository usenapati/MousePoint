using System.Collections;
using Core.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Level
{
    [RequireComponent(typeof(BoxCollider))]
    public class SceneTransitionTrigger : MonoBehaviour
    {
        [Header("Scene Transition Settings")] 
        [SerializeField] private Image fadeOverlay;
        [SerializeField] private float fadeDuration = 2f;
        [SerializeField] private string cutScene = "TottenTownBeta";
        
        [Header("VisualCue")]
        [SerializeField] private GameObject visualCue;

        private bool _isPlayerNear;
        
        private void OnEnable()
        {
            GameEventsManager.instance.inputEvents.OnInteractPressed += InteractPressed;
        }

        private void OnDisable()
        {
            GameEventsManager.instance.inputEvents.OnInteractPressed -= InteractPressed;
        }
        
        // Start is called before the first frame update
        private void Start()
        {
            visualCue.SetActive(false);
        }

        private void InteractPressed()
        {
            if (!_isPlayerNear)
            {
                return;
            }
            StartCoroutine(TransitionSequence());
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.CompareTag("Player")) // Corrected line
            {
                visualCue.SetActive(true);
                _isPlayerNear = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) // Corrected line
            {
                visualCue.SetActive(false);
                _isPlayerNear = false;
            }
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
