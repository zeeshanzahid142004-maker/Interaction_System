using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private TextMeshProUGUI promptText;
    
    // This should be the 'E' key Image component
    [SerializeField] private Image promptKey; 
    
    [SerializeField] private GameObject uiPanel;
    public bool isDisplayed = false;

    private void Start()
    {
        mainCam = Camera.main;
        uiPanel.SetActive(false);
        
        // --- CHANGE ---
        // Hide the GameObject, not just the component
        if (promptKey != null)
        {
            promptKey.gameObject.SetActive(false);
        }
        // -------------
    }

    private void LateUpdate()
    {
        var rotation = mainCam.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
    }

    public void setUp(string prompttext, Sprite icon)
    {
        promptText.text = prompttext;

        // --- UPDATED LOGIC ---
        if (promptKey != null)
        {
            // If we have an icon, show the key and set its sprite
            if (icon != null)
            {
                promptKey.sprite = icon;
                promptKey.gameObject.SetActive(true);
            }
            // If no icon is passed in, hide the key
            else
            {
                promptKey.gameObject.SetActive(false);
            }
        }
        // ---------------------

        uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        // --- CHANGE ---
        if (promptKey != null)
        {
            Debug.Log("Door close: InteractionPromptUI.Close() called. uiPanel active? " + (uiPanel != null && uiPanel.activeSelf));

            promptKey.gameObject.SetActive(false);
        }
        // -------------
        
        uiPanel.SetActive(false);
        isDisplayed = false;
    }
}