using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    [Tooltip("List of different ghost prefabs to spawn")]
    public GameObject[] ghostPrefabs;

    [Tooltip("List of places where ghosts can appear")]
    public Transform[] spawnPoints;

    [Tooltip("How often a new ghost spawns (in seconds)")]
    public float spawnInterval = 5f;

    [Tooltip("The maximum number of ghosts allowed in the scene at once")]
    public int maxGhosts = 5;

    private float spawnTimer;

    public bool isSpawning;

    void Start()
    {
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        if (isSpawning == false) return;

        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnGhost();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnGhost()
    {
        // 1. Safety check to prevent errors
        if (ghostPrefabs == null || ghostPrefabs.Length == 0 || spawnPoints.Length == 0)
        {
            Debug.LogWarning("Ghost Spawner is missing prefabs or spawn points!");
            return;
        }

        // 2. Count existing ghosts to prevent infinite spawning
        Ghost[] existingGhosts = FindObjectsByType<Ghost>(FindObjectsSortMode.None);
        if (existingGhosts.Length >= maxGhosts)
        {
            return;
        }

        // 3. Pick a random Ghost Prefab from the array
        int randomPrefabIndex = Random.Range(0, ghostPrefabs.Length);
        GameObject chosenPrefab = ghostPrefabs[randomPrefabIndex];

        // Ensure the slot isn't accidentally empty in the inspector
        if (chosenPrefab == null) return;

        // 4. Pick a random Spawn Point from the array
        int randomPointIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomPointIndex];

        // 5. Create the chosen ghost at the chosen location
        Instantiate(chosenPrefab, chosenPoint.position, Quaternion.identity);
    }
}