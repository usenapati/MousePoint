using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public Image fadeOverlay;
    public float fadeDuration = 2f;
    public string cutScene = "BoatScene";

    public void StartGame()
    {
        StartCoroutine(TransitionSequence());
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
