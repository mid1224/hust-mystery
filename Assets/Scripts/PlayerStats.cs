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

    public SpriteRenderer spriteRenderer;

    public GameObject[] healthIcons;

    void Start()
    {
        currentLives = startingLives;

        UpdateHealthIcons();
    }

    // Use OnTriggerEnter2D if your Ghost collider is set to "Is Trigger"
    // If it is a solid physics collider, use OnCollisionEnter2D(Collision2D collision) instead.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Check if we hit a ghost and aren't already flashing/invincible
        if (collision.CompareTag("Ghost") && !isInvulnerable)
        {
            Debug.Log("Called");

            TakeDamage();

            // 2. Destroy the ghost instantly upon touching the player.
            // (Note: If you want to use the Ghost's vanish animation instead, 
            // you will need to change 'private void Vanish()' to 'public void Vanish()' 
            // in Ghost.cs, and call it here using collision.GetComponent<Ghost>().Vanish();)
            collision.gameObject.GetComponent<Ghost>().Vanish();
        }
    }

    public void TakeDamage()
    {
        if (isInvulnerable) return;

        currentLives--;

        UpdateHealthIcons();

        if (currentLives <= 0)
        {
            currentLives = 0;
            GameOver();
        }
        else
        {
            // Start the flashing effect and invulnerability timer
            StartCoroutine(DamageRoutine());
        }
    }

    public void RestoreLife()
    {
        currentLives++;

        UpdateHealthIcons();
    }

    private void GameOver()
    {
        // Empty method for you to tackle later!
        Debug.Log("Game Over! 0 lives remaining.");
    }

    private IEnumerator DamageRoutine()
    {
        isInvulnerable = true;

        float elapsedTime = 0f;

        // Loop until our invulnerability time is up
        while (elapsedTime < invulnerabilityTime)
        {
            // Toggle alpha between 100% (1f) and 20% (0.2f)
            Color c = spriteRenderer.color;
            c.a = (c.a == 1f) ? 0.2f : 1f;
            spriteRenderer.color = c;

            // Wait for a fraction of a second before looping again
            yield return new WaitForSeconds(flashSpeed);
            elapsedTime += flashSpeed;
        }

        // Ensure the sprite is fully visible when the flashing ends
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