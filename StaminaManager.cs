using System;
using System.Collections;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
   [Header("Stamina Values")]
    [Tooltip("The maximum stamina the player can have.")]
    [SerializeField] private float maxStamina = 100f;

    [Tooltip("The player's current stamina. (Visible for debugging)")]
    [SerializeField] [Range(0, 100)] // We can still use attributes like Range
    private float currentStamina;

    [Header("Regeneration")]
    [SerializeField] private float staminaRegenRate = 5f; // Stamina per second
    [SerializeField] private float RegenDelay = 2f;


    private Coroutine regenCoRoutine;
     public float StaminaFraction => currentStamina / maxStamina;

    
    public float MaxStamina => maxStamina;
    void Start()
    {
        currentStamina = maxStamina;
    }

    public bool TrySpendStamina(float amount)

    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            if (regenCoRoutine != null)
            {
                StopCoroutine(regenCoRoutine);
            }

            regenCoRoutine = StartCoroutine(regenDelay());
            return true;
        }

        else
            return false;
    }

    private IEnumerator regenDelay()
    {
        yield return new WaitForSeconds(RegenDelay);
        regenCoRoutine = StartCoroutine(Regenerate());
    }
    private IEnumerator Regenerate()
    {
        while(currentStamina<maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
            yield return null;
        }
    }

}
