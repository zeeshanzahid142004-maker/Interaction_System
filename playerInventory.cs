
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerInventory: MonoBehaviour
{
    public int noOfHp { get; private set; }
    public int noOfAmmo {  get; private set; }
    public int noOfRepair {  get; private set; }
   
    
    public void hpCollected(int amount)
    {
        amount++;
        noOfHp++;
    }
    
    public void ammoCollected(int amount) 
    
    {
        amount++;
        noOfAmmo++; 
    }
    
    public void repairCollected(int amount) 
    
    {
        amount++;
        noOfRepair++;
    }
}
