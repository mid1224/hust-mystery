using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    private SoundManager soundManager;

    [SerializeField] Light2D lightSource;

    [SerializeField] AudioClip toggleOnSound;
    [SerializeField] AudioClip toggleOffSound;

    // Public property to check if flashlight is enabled
    public bool IsEnabled => lightSource.enabled;

    void Awake()
    {
        soundManager = FindFirstObjectByType<SoundManager>();
    }

    void Start()
    {
        lightSource.enabled = false;
    }

    public void ToggleFlashlight()
    {
        if (Inventory.Instance.HasItem("Flashlight") == false)
        {
            return;
        }

        soundManager.PlaySFX(lightSource.enabled ? toggleOffSound : toggleOnSound);

        lightSource.enabled = !lightSource.enabled;
    }

    public void SetFlashlightDirection(Vector2 direction)
    {
        if (lightSource.enabled == false)
        {
            return;
        }

        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        lightSource.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
