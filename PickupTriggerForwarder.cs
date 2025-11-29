using UnityEngine;

public class PickupTriggerForwarder : MonoBehaviour
{
    private pickUp parentPickup;

    private void Awake()
    {
        parentPickup = GetComponentInParent<pickUp>();
    }

 
}
