using UnityEngine;
using System.Collections.Generic; // Required for Dictionary

public class RepairManager : MonoBehaviour
{
    // The dictionary's key is the RepairTypeSO asset
    private Dictionary<RepairTypeSO, int> repairInventory = new Dictionary<RepairTypeSO, int>();

    /// <summary>
    /// Adds a repair item to the inventory.
    /// Called by RepairPickup.cs
    /// </summary>
    public void AddRepairItem(RepairTypeSO type, int amountToAdd)
    {
        if (repairInventory.ContainsKey(type))
        {
            repairInventory[type] += amountToAdd;
        }
        else
        {
            repairInventory.Add(type, amountToAdd);
        }

        Debug.Log("Picked up " + amountToAdd + " " + type.name + ". Total: " + repairInventory[type]);
    }

    /// <summary>
    /// Gets the current count of a specific repair item type.
    /// </summary>
    public int GetRepairItemCount(RepairTypeSO type)
    {
        if (repairInventory.ContainsKey(type))
        {
            return repairInventory[type];
        }
        return 0; // Not in inventory
    }

    /// <summary>
    /// Removes a repair item from the inventory (e.g., when the player uses it).
    /// </summary>
    public void UseRepairItem(RepairTypeSO type)
    {
        if (repairInventory.ContainsKey(type) && repairInventory[type] > 0)
        {
            repairInventory[type]--;
            Debug.Log("Used 1 " + type.name + ". Remaining: " + repairInventory[type]);
            
            // Here you would tell the player's "ArmorHealth" script to add health
        }
    }
}