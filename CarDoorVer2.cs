using UnityEngine;
using UnityEngine.Events;

public class CarDoorVer2 : MonoBehaviour, Iinteractable
{
    [Header("Config")]
    [SerializeField] private string prompt = "Enter Car";
    [SerializeField] private Sprite sprite;
    
    [Header("References")]
    [SerializeField] private CarInteractionHandler carInteractionHandler;
    [SerializeField] private UnityEvent _onInteract;

    // Interface Implementation
    public string interactionPrompt => prompt;
    public Sprite interactionSprite => sprite;

    public bool Interact(Interactor interactor)
    {
        Debug.Log("CarDoor: Interaction Started");

        // 1. Force Close the UI locally (Backup)
        InteractionPromptUI myUI = GetComponentInChildren<InteractionPromptUI>();
        if (myUI != null)
        {
            Debug.Log("CarDoor: Found local UI, closing it.");
            myUI.Close();
        }

        // 2. Ask the Interactor to close (Standard)
        if (interactor != null)
        {
            Debug.Log("CarDoor: Asking Interactor to clear prompt.");
            interactor.ClearPrompt();
        }

        // 3. "Nuclear" Collider Disable
        // Disable ALL colliders on this object and its children so the player stops detecting it.
        Debug.Log("CarDoor: Disabling all colliders on door.");
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        
        foreach (Collider col in allColliders)
        {
            col.enabled = false;
        }

        // 4. Enter the car
        if (carInteractionHandler != null)
        {
            Debug.Log("CarDoor: Calling EnterCar.");
            carInteractionHandler.EnterCar();
        }
        else
        {
            Debug.LogError("CarDoor: CarInteractionHandler is not assigned!", this);
        }

        _onInteract?.Invoke();
        
        return true;
    }

    // Call this when the player EXITS the car to make the door interactable again
    public void EnableDoor()
    {
        Debug.Log("CarDoor: Re-enabling door colliders.");
        Collider[] allColliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in allColliders)
        {
            col.enabled = true;
        }
    }
}