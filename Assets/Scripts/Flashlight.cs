using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI; // Required for the Slider

public class Flashlight : MonoBehaviour
{
    private SoundManager soundManager;

    [SerializeField] Light2D lightSource;
    [SerializeField] AudioClip toggleOnSound;
    [SerializeField] AudioClip toggleOffSound;

    [Header("Battery Settings")]
    public float maxBattery = 100f;

    public float drainRate = 15f;

    public float regenRate = 5f;

    private float currentBattery;

    [Header("UI")]
    public Slider batterySlider;

    public bool IsEnabled => lightSource.enabled;

    void Awake()
    {
        soundManager = FindFirstObjectByType<SoundManager>();
        currentBattery = maxBattery; // Start with a full battery
    }

    void Start()
    {
        lightSource.enabled = false;
        UpdateUI();
    }

    void Update()
    {
        HandleBattery();

        if (IsEnabled)
        {
            ProcessLightBeam();
        }
    }

    private void HandleBattery()
    {
        if (IsEnabled)
        {
            // Drain the battery over time
            currentBattery -= drainRate * Time.deltaTime;

            // Force the flashlight off if the battery dies
            if (currentBattery <= 0)
            {
                currentBattery = 0;
                ForceTurnOff();
            }
        }
        else
        {
            // Regenerate the battery over time if it isn't full
            if (currentBattery < maxBattery)
            {
                currentBattery += regenRate * Time.deltaTime;

                // Cap it at maxBattery so it doesn't overcharge
                if (currentBattery > maxBattery)
                {
                    currentBattery = maxBattery;
                }
            }
        }

        UpdateUI();
    }

    private void ForceTurnOff()
    {
        lightSource.enabled = false;
        if (soundManager != null && toggleOffSound != null)
        {
            soundManager.PlaySFX(toggleOffSound);
        }
    }

    private void UpdateUI()
    {
        if (batterySlider != null && batterySlider.gameObject.activeSelf == true)
        {
            // We use normalized value (0 to 1) so it works regardless of maxBattery size
            batterySlider.value = currentBattery / maxBattery;
        }
    }

    public void ToggleFlashlight()
    {
        if (Inventory.Instance.HasItem("Flashlight") == false)
        {
            return;
        }

        // Prevent turning on if the battery is completely dead
        if (!IsEnabled && currentBattery <= 0)
        {
            // Optional: Play a "dead flashlight click" sound here
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

    private void ProcessLightBeam()
    {
        Ghost[] allGhosts = FindObjectsByType<Ghost>(FindObjectsSortMode.None);

        foreach (Ghost ghost in allGhosts)
        {
            if (IsTargetInBeam(ghost.gameObject))
            {
                ghost.ReactToLight(Time.deltaTime);
            }
        }
    }

    private bool IsTargetInBeam(GameObject targetObject)
    {
        if (lightSource == null || targetObject == null) return false;

        Vector3 flashlightPos = lightSource.transform.position;
        Vector3 flashlightDir = lightSource.transform.up;
        Vector3 toTarget = (targetObject.transform.position - flashlightPos).normalized;

        float distance = Vector3.Distance(flashlightPos, targetObject.transform.position);
        if (distance > lightSource.pointLightOuterRadius) return false;

        float angle = Vector3.Angle(flashlightDir, toTarget);
        return angle <= (lightSource.pointLightOuterAngle / 2f);
    }

    // --- NEW METHOD: Increase Max Battery ---
    public void IncreaseMaxBattery(float amount)
    {
        maxBattery += amount;
        currentBattery += amount; // Immediately give them the new juice
        UpdateUI();
    }
}