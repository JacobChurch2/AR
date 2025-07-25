using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARPlaneManager))]
public class SimpleARSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] spawnPrefabs;
    public int maxSpawnCount = 20;
    public float minSpawnHeight = 0f;
    public float maxSpawnHeight = 0.5f;
    public float minDistanceBetweenObjects = 0.3f;

    [Header("Spawn Timing")]
    public bool useSpawnDelay = true;
    public float spawnDelay = 0.5f;

    private ARPlaneManager planeManager;
    private List<Vector3> spawnedPositions = new List<Vector3>();
    private List<GameObject> spawnedObjects = new List<GameObject>();
    private int currentSpawnCount = 0;
    private float lastSpawnTime = 0f;

    void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
    }

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        if (currentSpawnCount >= maxSpawnCount || spawnPrefabs == null || spawnPrefabs.Length == 0)
            return;

        foreach (ARPlane plane in args.added)
        {
            if (plane.alignment == PlaneAlignment.HorizontalUp && currentSpawnCount < maxSpawnCount)
            {
                TrySpawnOnPlane(plane);
            }
        }

        foreach (ARPlane plane in args.updated)
        {
            if (plane.alignment == PlaneAlignment.HorizontalUp && currentSpawnCount < maxSpawnCount)
            {
                TrySpawnOnPlane(plane);
            }
        }
    }

    void TrySpawnOnPlane(ARPlane plane)
    {
        // Check spawn delay
        if (useSpawnDelay && Time.time - lastSpawnTime < spawnDelay)
            return;

        Vector2 planeSize = plane.size;
        int attemptsPerPlane = Mathf.Min(5, maxSpawnCount - currentSpawnCount);

        for (int i = 0; i < attemptsPerPlane; i++)
        {
            if (currentSpawnCount >= maxSpawnCount) break;

            // Generate random position on plane
            float xOffset = Random.Range(-planeSize.x * 0.4f, planeSize.x * 0.4f);
            float zOffset = Random.Range(-planeSize.y * 0.4f, planeSize.y * 0.4f);
            float yOffset = Random.Range(minSpawnHeight, maxSpawnHeight);

            Vector3 localPos = new Vector3(xOffset, yOffset, zOffset);
            Vector3 worldPos = plane.transform.TransformPoint(localPos);

            // Check if position is far enough from existing spawns
            if (IsPositionValid(worldPos))
            {
                SpawnObject(worldPos);
                spawnedPositions.Add(worldPos);
                currentSpawnCount++;
                lastSpawnTime = Time.time;

                // Only spawn one object per delay period
                if (useSpawnDelay) break;
            }
        }
    }

    bool IsPositionValid(Vector3 newPosition)
    {
        foreach (Vector3 existingPos in spawnedPositions)
        {
            if (Vector3.Distance(newPosition, existingPos) < minDistanceBetweenObjects)
            {
                return false;
            }
        }
        return true;
    }

    void SpawnObject(Vector3 position)
    {
        // Get random prefab from list
        GameObject prefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];

        // Random rotation around Y axis
        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);

        GameObject spawnedObject = Instantiate(prefab, position, rotation);
        spawnedObjects.Add(spawnedObject);

        Debug.Log($"Spawned {prefab.name} at {position}. Total spawned: {currentSpawnCount}");
    }

    [ContextMenu("Clear All Spawned Objects")]
    public void ClearAllSpawned()
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
                DestroyImmediate(obj);
        }

        spawnedObjects.Clear();
        spawnedPositions.Clear();
        currentSpawnCount = 0;
        lastSpawnTime = 0f;

        Debug.Log("Cleared all spawned objects");
    }

    [ContextMenu("Spawn More Objects")]
    public void SpawnMore()
    {
        // Try to spawn on existing planes
        foreach (var plane in planeManager.trackables)
        {
            if (currentSpawnCount >= maxSpawnCount) break;
            if (plane.alignment == PlaneAlignment.HorizontalUp)
            {
                TrySpawnOnPlane(plane);
            }
        }
    }
}