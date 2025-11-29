using UnityEngine;

public class PlayerHealth : MonoBehaviour

{
    [SerializeField][Range(0,100)] private float maxHealth = 100f;
    [SerializeField] [Range(0,100)] private float currentHealth = 100f;
    public float MaxHealth => maxHealth;
    public float HealthFraction => currentHealth / maxHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }
    public bool IsAlive()
    {
        return currentHealth > 0;
    }

public bool canHeal()
        {
        return IsAlive() && currentHealth < maxHealth;
    }

    public void tryHealing(float amountToHeal)
    {
        if (!IsAlive())
            return;


        currentHealth += amountToHeal;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        Debug.Log("added health" + amountToHeal);

    }
    
    public void takeDamage(float amountToDamage)
    {
        if (!IsAlive())
            return;

        currentHealth -= amountToDamage;
        if (!IsAlive())
        {
            currentHealth = 0; 
            Debug.Log("Player has died!");
          
        }
       
        Debug.Log("Damage taken" + amountToDamage + " health depleted to " + currentHealth);
    }
}
