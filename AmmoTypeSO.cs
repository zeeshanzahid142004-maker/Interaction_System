using UnityEngine;

// This attribute lets you right-click in the Project window to create a new Ammo Type
[CreateAssetMenu(fileName = "NewAmmoType", menuName = "Pickups/Ammo Type")]
public class AmmoTypeSO : ScriptableObject
{
    // You can add more data here later!
    // For example:
    public string ammoName;
     public Sprite ammoIcon;
     public string description;
    
}