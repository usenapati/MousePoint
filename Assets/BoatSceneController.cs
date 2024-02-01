using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class BoatSceneController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string gameplayScene = "TottenTownBeta";
    public Image fadeOverlay;
    public float fadeDuration = 3f;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.loopPointReached += EndReached;

        // Start the fade from black effect
        StartCoroutine(FadeFromBlack());
    }

    void EndReached(VideoPlayer vp)
    {
        // Start fade to black and then load the next scene
        StartCoroutine(FadeToBlackAndLoadScene());
    }

    private IEnumerator FadeFromBlack()
    {
        yield return StartCoroutine(FadeImage(0)); // Fade from black
    }

    private IEnumerator FadeToBlackAndLoadScene()
    {
        yield return StartCoroutine(FadeImage(1)); // Fade to black
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameplayScene); // Direct call to Unity's SceneManager
    }

    private IEnumerator FadeImage(float targetAlpha)
    {
        float startAlpha = fadeOverlay.color.a;
        float time = 0;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeOverlay.color = new Color(0, 0, 0, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        fadeOverlay.color = new Color(0, 0, 0, targetAlpha);
    }
}
