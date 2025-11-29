using UnityEngine;


public class RepairPickup : PickupBase 
{
    [Header("Repair Config")]
    [SerializeField] private RepairTypeSO repairType; 
    [SerializeField] private int amountToAdd = 1;

   
    protected override bool OnPickup(Interactor interactor)
    {
        if (repairType == null)
        {
            Debug.LogError("RepairTypeSO is not assigned on " + gameObject.name);
            return false;
        }

        RepairManager repairManager = interactor.GetComponent<RepairManager>();
        if (repairManager == null)
        {
            Debug.LogError("RepairManager script is NULL!", interactor.gameObject);
            return false;
        }

        // Add the item to the inventory
        repairManager.AddRepairItem(repairType, amountToAdd);
        
        // Return true to signal the pickup was successful
        return true;
    }
}