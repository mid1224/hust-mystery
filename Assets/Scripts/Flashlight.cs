using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Flashlight : MonoBehaviour
{
    private SoundManager soundManager;
    private Camera mainCamera;
    [SerializeField] PauseMenu pauseMenu;

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
        mainCamera = Camera.main;
        currentBattery = maxBattery;
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
            currentBattery -= drainRate * Time.deltaTime;

            if (currentBattery <= 0)
            {
                currentBattery = 0;
                ForceTurnOff();
            }
        }
        else
        {
            if (currentBattery < maxBattery)
            {
                currentBattery += regenRate * Time.deltaTime;

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
            batterySlider.value = currentBattery / maxBattery;
        }
    }

    public void ToggleFlashlight()
    {
        if (Inventory.Instance.HasItem("Flashlight") == false)
        {
            return;
        }

        if (!IsEnabled && currentBattery <= 0)
        {
            return;
        }

        soundManager.PlaySFX(lightSource.enabled ? toggleOffSound : toggleOnSound);
        lightSource.enabled = !lightSource.enabled;
    }

    // --- UPDATED: Removed the facing direction parameter and simplified the math ---
    public void AimWithMouse()
    {
        if (lightSource.enabled == false || pauseMenu.isPausing == true)
        {
            return;
        }

        if (Mouse.current == null) return;

        // 1. Get the mouse position
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        // 2. Get the direction from the flashlight to the mouse
        Vector2 aimDirection = (mouseWorldPos - lightSource.transform.position).normalized;

        // 3. Convert direction directly to an angle
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // 4. Apply rotation directly (subtracting 90 for Light2D's natural 'Up' alignment)
        lightSource.transform.rotation = Quaternion.Euler(0f, 0f, aimAngle - 90f);
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

    public void IncreaseMaxBattery(float amount)
    {
        maxBattery += amount;
        currentBattery += amount;
        UpdateUI();
    }
}