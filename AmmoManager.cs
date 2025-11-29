using UnityEngine;
using System.Collections.Generic; // Required for Dictionary

public class AmmoManager : MonoBehaviour
{
    
    // The dictionary's key is now the AmmoTypeSO asset
    private Dictionary<AmmoTypeSO, int> ammoInventory = new Dictionary<AmmoTypeSO, int>();

    
    // The dictionary will grow as we pick up new types.

    public void AddAmmo(AmmoTypeSO type, int amountToAdd)
    {
        // Check if we've picked up this type before
        if (ammoInventory.ContainsKey(type))
        {
            // If yes, just add to the current count
            ammoInventory[type] += amountToAdd;
        }
        else
        {
            // If no, this is the first time! Add it to the dictionary.
            ammoInventory.Add(type, amountToAdd);
        }

        Debug.Log("Added " + amountToAdd + " " + type.name + " ammo. Total: " + ammoInventory[type]);
        
        // Update UI here
    }

    public int GetAmmoCount(AmmoTypeSO type)
    {
        if (ammoInventory.ContainsKey(type))
           {
            return ammoInventory[type];
        }
        
        // If we don't have any of this type, return 0
        return 0;
    }
}