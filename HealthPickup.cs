using UnityEngine;

// Inherit from PickupBase, NOT MonoBehaviour
public class HealthPickup : PickupBase 
{
    [Header("Health Config")]
    [SerializeField] private HealthTypeSO healthType;
    [SerializeField] private int amountToAdd = 1; // How many of this item to add

   
    protected override bool OnPickup(Interactor interactor)
    {
        if (healthType == null)
        {
            Debug.LogError("HealthTypeSO is not assigned on " + gameObject.name);
            return false;
        }

        HealthManager healthManager = interactor.GetComponent<HealthManager>();
        if (healthManager == null)
        {
            Debug.LogError("HealthManager script is NULL!", interactor.gameObject);
            return false;
        }

        // Add the item to the inventory
        healthManager.AddHealthItem(healthType, amountToAdd);
        
        // Return true to signal the pickup was successful
        return true;
    }
}