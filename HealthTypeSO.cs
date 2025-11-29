using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthType", menuName = "Pickups/Health Type")]
public class HealthTypeSO : ScriptableObject
{
    // How much this health pack heals
    public int healAmount = 25; 

    
     public string pickupName;
    // public Sprite icon;
}