using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Slider))]
public class PlayerHealthUi : MonoBehaviour
{
    [Tooltip("PlayerHealth (on player object)")]
    [SerializeField] private PlayerHealth playerHealth;
    private Slider healthSlider;
    void Awake()
    {
        healthSlider = GetComponent<Slider>();
    }
    void Update()
    {
        if (playerHealth == null)
            Debug.Log("Player health is missing");
        else
            healthSlider.value = playerHealth.HealthFraction;
    }
}
