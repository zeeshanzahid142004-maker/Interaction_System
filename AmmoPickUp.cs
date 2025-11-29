using UnityEngine;

// Inherit from PickupBase
public class AmmoPickup : PickupBase 
{
    [Header("Ammo Config")]
    [SerializeField] private AmmoTypeSO ammoType;
    [SerializeField] private int amount = 5;

       protected override bool OnPickup(Interactor interactor)
    {
        if (ammoType == null)
        {
            Debug.LogError("AmmoTypeSO is not assigned on " + gameObject.name);
            return false;
        }
        
        AmmoManager ammoManager = interactor.GetComponent<AmmoManager>();
        if (ammoManager == null)
        {
            Debug.LogError("AmmoManager is NULL!", interactor.gameObject);
            return false;
        }

        // 1. The Logic
        ammoManager.AddAmmo(ammoType, amount);
        
        // 2. Return true to signal the pickup was successful
        return true; 
    }
}