using System.Collections;
using UnityEngine;

// 1. We create a custom data container for your profiles.
// [System.Serializable] tells Unity to display this in the Inspector!
[System.Serializable]
public struct ChaseProfile
{
    [Tooltip("How fast the monster moves during this phase")]
    public float speed;

    [Tooltip("How many seconds this phase lasts")]
    public float duration;
}

[RequireComponent(typeof(Rigidbody2D))]
public class FinalBoss : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Pacing Sequence")]
    [Tooltip("Set up your 3 (or more) speed profiles here!")]
    public ChaseProfile[] speedProfiles;

    [Tooltip("The permanent speed the monster uses after all profiles run out")]
    public float finalSpeed = 8f;

    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Start reading the profiles as soon as the monster spawns
        if (speedProfiles != null && speedProfiles.Length > 0)
        {
            StartCoroutine(RunChaseSequence());
        }
        else
        {
            // Safety fallback if the array is empty
            currentSpeed = finalSpeed;
        }
    }

    private IEnumerator RunChaseSequence()
    {
        // 2. Loop through every profile in your list one by one
        for (int i = 0; i < speedProfiles.Length; i++)
        {
            // Apply the speed for this specific profile
            currentSpeed = speedProfiles[i].speed;

            // Pause the coroutine here for the duration of the profile
            yield return new WaitForSeconds(speedProfiles[i].duration);
        }

        // 3. Once all profiles have finished, lock into the final speed
        currentSpeed = finalSpeed;
    }

    void FixedUpdate()
    {
        // Constantly push the monster to the left at whatever the current speed is
        rb.linearVelocity = Vector2.left * currentSpeed;
    }
}