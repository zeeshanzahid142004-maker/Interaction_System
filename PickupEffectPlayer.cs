using UnityEngine;

public class PickupEffectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject pickupEffectPrefab;

    /// <summary>
    /// This is the public method the AmmoPickup script will call.
    /// </summary>
    public void Play()
    {
        if (pickupEffectPrefab != null)
        {
            // Spawn the particle effect at this object's position
            Instantiate(pickupEffectPrefab, transform.position, Quaternion.identity);
        }
    }
}