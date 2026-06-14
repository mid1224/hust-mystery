using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Level_8_FindKeyInTheDark : MonoBehaviour
{
    [SerializeField] Light2D flashlight;
    [SerializeField] GameObject keyObject;
    [SerializeField] Flashlight flashlightController;

    private void Update()
    {
        if (keyObject == null) return;

        if (flashlightController.IsEnabled && IsKeyInFlashlightRange())
        {
            keyObject.SetActive(true);
        }
        else
        {
            keyObject.SetActive(false);
        }
    }

    private bool IsKeyInFlashlightRange()
    {
        if (flashlight == null || keyObject == null)
            return false;

        Vector3 flashlightPos = flashlight.transform.position;
        Vector3 flashlightDir = flashlight.transform.up;  // Changed from .right to .up
        Vector3 toKey = (keyObject.transform.position - flashlightPos).normalized;

        // Check distance
        float distance = Vector3.Distance(flashlightPos, keyObject.transform.position);
        if (distance > flashlight.pointLightOuterRadius)
            return false;

        // Check angle (considers flashlight direction)
        float angle = Vector3.Angle(flashlightDir, toKey);
        return angle <= flashlight.pointLightInnerAngle;
    }
}
