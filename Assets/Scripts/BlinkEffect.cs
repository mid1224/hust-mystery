using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BlinkEffect : MonoBehaviour
{
    [Header("Blink Settings")]
    [Tooltip("How fast the sprite pulses in and out")]
    public float blinkSpeed = 2f;

    [Tooltip("The lowest alpha it will reach (0 = totally invisible)")]
    public float minAlpha = 0.1f;

    [Tooltip("The highest alpha it will reach (1 = fully solid)")]
    public float maxAlpha = 1f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Calculate the difference between max and min
        float alphaRange = maxAlpha - minAlpha;

        // PingPong bounces a number between 0 and alphaRange based on the current time
        float currentAlpha = minAlpha + Mathf.PingPong(Time.time * blinkSpeed, alphaRange);

        // Apply the new alpha to the sprite's color
        Color newColor = spriteRenderer.color;
        newColor.a = currentAlpha;
        spriteRenderer.color = newColor;
    }
}