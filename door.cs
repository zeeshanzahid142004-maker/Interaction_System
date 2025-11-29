using UnityEngine;
using UnityEngine.Events;

public class door : MonoBehaviour, Iinteractable
{
    [Header("Interaction")]
    [SerializeField] private Sprite sprite;
    bool isOpen = false;

    public string interactionPrompt => isOpen ? "Close" : "Open";
    public Sprite interactionSprite => sprite;

    [Header("Component References")]
    [SerializeField] private toggleDoor doorMover;
    [SerializeField] private DoorSoundPlayer doorSound;

    [SerializeField] private UnityEvent _onInteract;

    
    public bool Interact(Interactor interactor)
    {
        // --- DEBUG ---
        Debug.LogWarning("door.Interact() CALLED!", gameObject);
        // -----------

        isOpen = !isOpen;
        interactor.RefreshPrompt();

        if (doorMover != null)
        {
            doorMover.SetState(isOpen);
        }
        else
        {
            // --- DEBUG ---
            Debug.LogError("Door Mover is NULL!", gameObject);
            // -----------
        }

        if (doorSound != null)
        {
            doorSound.Play(isOpen);
        }

        _onInteract?.Invoke();
        return true;
    }
}