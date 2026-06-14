using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Movement")]
    public float baseSpeed = 3f;
    public float slowedSpeed = 0.8f;
    private float currentSpeed;

    [Header("Light Vulnerability")]
    [Tooltip("How many seconds of light exposure to destroy the ghost")]
    public float timeToDestroy = 3f;
    private float currentExposure = 0f;
    private bool isBeingIlluminated = false;

    [Header("Animation")]
    public Animator animator;
    [Tooltip("How long to wait before destroying the object, matching your animation length")]
    public float vanishAnimationDuration = 1f;
    private bool isVanishing = false;

    private Transform player;
    public SpriteRenderer spriteRenderer;
    private Collider2D ghostCollider;

    void Start()
    {
        currentSpeed = baseSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ghostCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isVanishing)
        {
            return;
        }

        if (player != null)
        {
            // 1. Move toward the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, currentSpeed * Time.deltaTime);

            // 2. Flip the sprite to face the player
            // If the player's X is less than the ghost's X, the player is on the left, so we flip the right-facing sprite.
            spriteRenderer.flipX = player.position.x < transform.position.x;
        }

        if (!isBeingIlluminated)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, baseSpeed, Time.deltaTime * 3f);
            currentExposure = Mathf.Max(0, currentExposure - Time.deltaTime);
        }

        isBeingIlluminated = false;

        UpdateVisuals();
    }

    public void ReactToLight(float exposureTime)
    {
        if (isVanishing)
        {
            return;
        }

        isBeingIlluminated = true;
        currentSpeed = slowedSpeed;
        currentExposure += exposureTime;

        if (currentExposure >= timeToDestroy)
        {
            Vanish();
        }
    }

    private void UpdateVisuals()
    {
        if (spriteRenderer != null && !isVanishing)
        {
            float alpha = 1f - (currentExposure / timeToDestroy);
            Color newColor = spriteRenderer.color;
            newColor.a = Mathf.Clamp(alpha, 0.7f, 1f);
            spriteRenderer.color = newColor;
        }
    }

    public void Vanish()
    {
        isVanishing = true;

        if (ghostCollider != null)
        {
            ghostCollider.enabled = false;
        }

        if (animator != null)
        {
            animator.SetTrigger("Vanish");
        }

        Destroy(gameObject, vanishAnimationDuration);
    }
}