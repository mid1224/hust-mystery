using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransitionEffect : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] Image blackscreen;
    [SerializeField] float pitchBlackDuration = 1f;
    [SerializeField] float fadeDuration = 1.5f;

    private Coroutine currentTransitionCoroutine;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void PlayRoomTransitionEffect()
    {
        if (currentTransitionCoroutine != null)
        {
            StopCoroutine(currentTransitionCoroutine);
        }

        currentTransitionCoroutine = StartCoroutine(RoomTransitionCoroutine());
    }

    IEnumerator RoomTransitionCoroutine()
    {
        playerController.disableMovement = true;

        blackscreen.enabled = true;

        Color color = blackscreen.color;
        color.a = 1f;
        blackscreen.color = color;

        yield return new WaitForSeconds(pitchBlackDuration); // Stay black

        float elapsedTime = 0f;
        bool movementReenabled = false;

        // Fade from black to clear
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Enable movement after fading halfway
            if (!movementReenabled && elapsedTime >= fadeDuration/2)
            {
                playerController.disableMovement = false;
                movementReenabled = true;
            }

            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            blackscreen.color = color;
            yield return null;
        }

        color.a = 0f;
        blackscreen.color = color;

        blackscreen.enabled = false;

        // Failsafe check
        if (!movementReenabled)
        {
            playerController.disableMovement = false;
        }
    }
}
