using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] Image fadeOutImage;
    [SerializeField] float fadeDuration;

    private void Start()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        yield return new WaitForSeconds(1f);

        float elapsedTime = 0f;
        Color startColor = fadeOutImage.color;

        // Loop until the elapsed time reaches the fade duration
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the new alpha value linearly over time
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // Apply the new alpha to the image
            fadeOutImage.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            yield return null; // Wait until the next frame
        }

        // Ensure it is completely transparent at the end
        fadeOutImage.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        // Optional: Disable the image object entirely so it doesn't block raycasts/clicks
        fadeOutImage.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
