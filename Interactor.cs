using UnityEngine;

public class Interactor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InteractionPromptUI promptUI;
    [SerializeField] private float hideDelay = 0.3f; // small delay to prevent flicker

    private Iinteractable currentInteractable;
    private IHighlightable currentHighlightable; // Your highlight logic is correct
    private float hideTimer;

    public InteractionPromptUI PromptUI => promptUI;

    private void Update()
    {
        // This logic is perfect, leave it as is.
        if (currentInteractable == null && promptUI.isDisplayed)
        {
            hideTimer += Time.deltaTime;
            if (hideTimer >= hideDelay)
            {
                promptUI.Close();
                hideTimer = 0f;
            }
        }
    }
    
    // --- THIS IS THE NEW, MISSING FUNCTION ---
    /// <summary>
    /// Called by PlayerInteraction when the interact key is pressed.
    /// </summary>
    public void PerformInteraction()
    {
        if (currentInteractable != null)
        {
            // Pass 'this' (the Interactor) to the Interact method
            currentInteractable.Interact(this);
        }
    }
    // --- END OF NEW FUNCTION ---

    /// <summary>
    /// Called by PlayerInteraction when a new interactable is detected.
    /// </summary>
    public void ShowPrompt(Iinteractable interactable)
    {
        if (interactable == null) return;

        if (currentInteractable != interactable)
        {
            // Unhighlight the old one (this is synced)
            if (currentHighlightable != null)
            {
                currentHighlightable.OnUnhighlight();
            }

            // Check if the new one is highlightable (this is synced)
            currentHighlightable = (interactable as MonoBehaviour)?.GetComponent<IHighlightable>();

            if (currentHighlightable != null)
            {
                currentHighlightable.OnHighlight();
            }
            
            currentInteractable = interactable;
            promptUI.setUp(interactable.interactionPrompt, interactable.interactionSprite);
        }

        hideTimer = 0f;
    }

    /// <summary>
    /// Called by PlayerInteraction when nothing interactable is in front.
    /// </summary>
    public void ClearPrompt()
    {
        // Unhighlight (this is synced)
        if (currentHighlightable != null)
        {
            currentHighlightable.OnUnhighlight();
            currentHighlightable = null;
        }

Debug.Log("DorrClose: Interactor.ClearPrompt() called. currentInteractable: " + currentInteractable);
promptUI?.Close();



        currentInteractable = null;



    }
 
    /// <summary>
    /// Directly used by interactables (like your door) to update the prompt text.
    /// </summary>
    public void RefreshPrompt()
    {
        // This is synced and used correctly by your door.cs
        if (currentInteractable != null)
        {
            promptUI.setUp(currentInteractable.interactionPrompt, currentInteractable.interactionSprite);
        }
    }
}