using UnityEngine;
using System.Collections.Generic; 

public class HealthManager : MonoBehaviour
{
    // The dictionary's key is the HealthTypeSO asset
    private Dictionary<HealthTypeSO, int> healthInventory = new Dictionary<HealthTypeSO, int>();

    /// <summary>
    /// Adds a health pack to the inventory.
    /// Called by HealthPickup.cs
    /// </summary>
    public void AddHealthItem(HealthTypeSO type, int amountToAdd)
    {
        // Check if we've picked up this type before
        if (healthInventory.ContainsKey(type))
        {
            // If yes, just add to the current count
            healthInventory[type] += amountToAdd;
        }
        else
        {
            // If no, this is the first time! Add it to the dictionary.
            healthInventory.Add(type, amountToAdd);
        }

        Debug.Log("Picked up " + amountToAdd + " " + type.name + ". Total: " + healthInventory[type]);
        
        // You would tell the UI to update here
    }

    /// <summary>
    /// Gets the current count of a specific health pack type.
    /// This could be called by your UI or by a "Use Health Pack" script.
    /// </summary>
    public int GetHealthItemCount(HealthTypeSO type)
    {
        if (healthInventory.ContainsKey(type))
        {
            return healthInventory[type];
        }
        
        // If we don't have any of this type, return 0
        return 0;
    }

    /// <summary>
    /// Removes a health pack from the inventory (e.g., when the player uses it).
    /// </summary>
    public void UseHealthItem(HealthTypeSO type)
    {
        if (healthInventory.ContainsKey(type) && healthInventory[type] > 0)
        {
            healthInventory[type]--;
            Debug.Log("Used 1 " + type.name + ". Remaining: " + healthInventory[type]);
            
            // Here you would tell the PlayerHealth script to add health
            // and tell the UI to update.
        }
    }
}