using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [System.Serializable]
    public class PickupType
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnWeight = 1f; // higher = more likely
    }

    public PickupType[] pickupTypes;
    public Transform[] spawnPoints;
    [Range(0f, 1f)]
    public float spawnChance = 0.6f; // chance to spawn something at a point
    public float spawnHeightOffset = 1f;

    void Start()
    {
        SpawnPickups();
    }

    void SpawnPickups()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (Random.value <= spawnChance)
            {
                GameObject chosenPickup = ChooseWeightedPickup();
                if (chosenPickup != null)
                {
                    Vector3 spawnPos = spawnPoint.position + Vector3.up * spawnHeightOffset;
                    Instantiate(chosenPickup, spawnPos, Quaternion.identity);
                }
            }
        }
    }

    GameObject ChooseWeightedPickup()
    {
        // Calculate total weight
        float totalWeight = 0f;
        foreach (var p in pickupTypes)
            totalWeight += p.spawnWeight;

        // Pick a random value between 0 and totalWeight
     
     
        if (totalWeight <= 0f)
        {
        Debug.LogWarning("PickupSpawner: All spawn weights are 0! Cannot select pickup.");
        return null;
        }
        float randomValue = Random.value * totalWeight;

        // Go through each pickup and subtract its weight until we find the one to spawn
        foreach (var p in pickupTypes)
        {
            if (randomValue < p.spawnWeight)
                return p.prefab;
            randomValue -= p.spawnWeight;
        }

        return null; // fallback (shouldn't happen)
    }
}
