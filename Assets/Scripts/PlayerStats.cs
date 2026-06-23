using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int startingLives = 3;
    private int currentLives;

    [Header("Damage Feedback")]
    [Tooltip("How long the player is safe after taking damage")]
    public float invulnerabilityTime = 1.5f;
    [Tooltip("How fast the sprite flashes")]
    public float flashSpeed = 0.1f;
    private bool isInvulnerable = false;

    [Header("Jumpscare")]
    public GameObject jumpscareVideo;
    public float jumpscareDuration = 1f;

    [Header("UI Screens")]
    public GameObject endgameScreen; // <-- New variable for your Game Over UI

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public GameObject[] healthIcons;

    void Start()
    {
        currentLives = startingLives;
        UpdateHealthIcons();

        // Ensure screens start turned off
        if (jumpscareVideo != null)
        {
            jumpscareVideo.SetActive(false);
        }

        if (endgameScreen != null)
        {
            endgameScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Don't do anything if we are already dead
        if (currentLives <= 0) return;

        if (collision.CompareTag("Ghost") && !isInvulnerable)
        {
            // 1. Play the jumpscare and wipe the map
            StartCoroutine(PlayJumpscare());

            // 2. Take damage
            TakeDamage();

            // 3. Vanish the ghost that hit us
            collision.gameObject.GetComponent<Ghost>().Vanish();
        }

        if (collision.CompareTag("Boss"))
        {
            currentLives = 0;
            StartCoroutine(PlayJumpscare());
        }
    }

    private IEnumerator PlayJumpscare()
    {
        // Turn on the video
        if (jumpscareVideo != null)
        {
            jumpscareVideo.SetActive(true);
        }

        // Find ALL other ghosts on the map and tell them to vanish
        Ghost[] allGhosts = FindObjectsByType<Ghost>(FindObjectsSortMode.None);
        foreach (Ghost ghost in allGhosts)
        {
            if (ghost != null)
            {
                ghost.Vanish();
            }
        }

        // Wait for the video to play
        yield return new WaitForSeconds(jumpscareDuration);

        // Turn the video off
        if (jumpscareVideo != null)
        {
            jumpscareVideo.SetActive(false);
        }

        // --- NEW LOGIC: Check for Game Over AFTER the jumpscare finishes ---
        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    public void TakeDamage()
    {
        if (isInvulnerable || currentLives <= 0) return;

        currentLives--;

        UpdateHealthIcons();

        // Only start the invulnerability flashing if we are still alive
        if (currentLives > 0)
        {
            StartCoroutine(DamageRoutine());
        }
    }

    public void RestoreLife()
    {
        if (currentLives < startingLives)
        {
            currentLives++;
            UpdateHealthIcons();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over! 0 lives remaining.");

        // Turn on your Endgame Screen!
        if (endgameScreen != null)
        {
            endgameScreen.SetActive(true);
        }

        // Completely freeze the game in the background so nothing else can happen
        Time.timeScale = 0f;
    }

    private IEnumerator DamageRoutine()
    {
        isInvulnerable = true;

        float elapsedTime = 0f;

        while (elapsedTime < invulnerabilityTime)
        {
            Color c = spriteRenderer.color;
            c.a = (c.a == 1f) ? 0.2f : 1f;
            spriteRenderer.color = c;

            yield return new WaitForSeconds(flashSpeed);
            elapsedTime += flashSpeed;
        }

        Color finalColor = spriteRenderer.color;
        finalColor.a = 1f;
        spriteRenderer.color = finalColor;

        isInvulnerable = false;
    }

    private void UpdateHealthIcons()
    {
        if (healthIcons != null && healthIcons.Length > 0)
        {
            for (int i = 0; i < healthIcons.Length; i++)
            {
                if (i < currentLives)
                {
                    healthIcons[i].SetActive(true);
                }
                else
                {
                    healthIcons[i].SetActive(false);
                }
            }
        }
    }
}