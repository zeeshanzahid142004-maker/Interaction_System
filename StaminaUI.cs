using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class StaminaUI : MonoBehaviour
{
     [Header("References")]
    [Tooltip("The StaminaManager on  Player")]
    [SerializeField] private StaminaManager staminaManager;
    
    private Slider staminaSlider;
    void Awake()

    {
        staminaSlider = GetComponent<Slider>();
    }
    
    void Update()
    {
        if (staminaManager == null)
            Debug.LogWarning("Stamina manager is missing !!!");
        else
            staminaSlider.value = staminaManager.StaminaFraction;
    }
}
