using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Interface for collectible behavior
public interface ICollectible
{
    void OnCollected();
    event System.Action<ICollectible> Collected;
}

[System.Serializable]
public class CollectibleSpawnRule
{
    public GameObject collectiblePrefab;
    [Range(0, 100)]
    public float spawnWeight = 1f;
    [Range(0, 100)]
    public int maxCount = 5;
}

public class CollectibleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private List<CollectibleSpawnRule> collectibles = new List<CollectibleSpawnRule>();
    [SerializeField] private int totalCollectibleCount = 5;
    [SerializeField] private int minDistanceFromPlayer = 1;

    [Header("Optional Settings")]
    [SerializeField] private bool ensureMinimumSpacing = false;
    [SerializeField] private float minimumSpacingDistance = 2f;

    private List<ICollectible> spawnedCollectibles = new List<ICollectible>();

    public void SpawnCollectibles(HashSet<Vector2Int> floorPositions, Vector2Int playerStartPosition)
    {
        if (collectibles.Count == 0)
        {
            Debug.LogWarning("No collectibles configured in the spawner!");
            return;
        }

        List<Vector2Int> availableSpawnPoints = GetValidSpawnPoints(floorPositions, playerStartPosition);

        if (availableSpawnPoints.Count == 0)
        {
            Debug.LogWarning("No valid spawn points available!");
            return;
        }

        int remainingToSpawn = Mathf.Min(totalCollectibleCount, availableSpawnPoints.Count);
        SpawnCollectiblesRandomly(availableSpawnPoints, remainingToSpawn);
    }

    private List<Vector2Int> GetValidSpawnPoints(HashSet<Vector2Int> floorPositions, Vector2Int playerStartPosition)
    {
        List<Vector2Int> validPoints = floorPositions.ToList();

        // Remove points too close to player
        validPoints.RemoveAll(point =>
            Mathf.Abs(point.x - playerStartPosition.x) <= minDistanceFromPlayer &&
            Mathf.Abs(point.y - playerStartPosition.y) <= minDistanceFromPlayer);

        return validPoints;
    }

    private void SpawnCollectiblesRandomly(List<Vector2Int> spawnPoints, int count)
    {
        float totalWeight = collectibles.Sum(c => c.spawnWeight);
        List<Vector3> usedPositions = new List<Vector3>();

        while (count > 0 && spawnPoints.Count > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Count);
            Vector2Int spawnPosition = spawnPoints[spawnIndex];
            Vector3 worldPosition = new Vector3(spawnPosition.x + 0.5f, spawnPosition.y + 0.5f, 0);

            if (ensureMinimumSpacing && usedPositions.Any(pos => Vector3.Distance(pos, worldPosition) < minimumSpacingDistance))
            {
                spawnPoints.RemoveAt(spawnIndex);
                continue;
            }

            float randomValue = Random.Range(0, totalWeight);
            float currentWeight = 0;

            foreach (var rule in collectibles)
            {
                currentWeight += rule.spawnWeight;
                if (randomValue <= currentWeight && 
                    spawnedCollectibles.Count(c => c != null && c.GetType() == rule.collectiblePrefab.GetComponent<ICollectible>()?.GetType()) < rule.maxCount)
                {
                    var obj = Instantiate(rule.collectiblePrefab, worldPosition, Quaternion.identity);
                    var collectible = obj.GetComponent<ICollectible>();
                    if (collectible != null)
                    {
                        collectible.Collected += OnCollectibleCollected;
                        spawnedCollectibles.Add(collectible);
                    }
                    usedPositions.Add(worldPosition);
                    break;
                }
            }

            spawnPoints.RemoveAt(spawnIndex);
            count--;
        }
    }

    private void OnCollectibleCollected(ICollectible collectible)
    {
        // Here you can add logic to add to inventory, update UI, etc.
        spawnedCollectibles.Remove(collectible);
    }

    public void ClearSpawnedCollectibles()
    {
        spawnedCollectibles.Clear();
    }
}