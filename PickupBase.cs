using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public abstract class PickupBase : MonoBehaviour, Iinteractable, IHighlightable
{
    [Header("Interaction")]
    [SerializeField] private string prompt;
    [SerializeField] private Sprite sprite;
    
    [Header("Component References")]
    [SerializeField] private BubbleMaker bubbleHighlighter; 
    [SerializeField] private PickupAnimator pickupAnimator;
    [SerializeField] private PickupSoundPlayer pickupSoundPlayer;

    private bool isInteracting = false;
    private Collider col;

    // --- SHARED METHODS ---

    // 'protected virtual' means child classes can add to this method if they need to
    protected virtual void Start()
    {
        col = GetComponent<Collider>();
        if (bubbleHighlighter == null) bubbleHighlighter = GetComponentInChildren<BubbleMaker>();
        if (pickupAnimator == null) pickupAnimator = GetComponentInChildren<PickupAnimator>();
        if (pickupSoundPlayer == null) pickupSoundPlayer = GetComponentInChildren<PickupSoundPlayer>();
    }

    // --- INTERFACE IMPLEMENTATION (SHARED) ---
    public string interactionPrompt => prompt;
    public Sprite interactionSprite => sprite;

    [SerializeField] private UnityEvent _onInteract;

    public void OnHighlight()
    {
        if (bubbleHighlighter != null) bubbleHighlighter.Highlight();
    }

    public void OnUnhighlight()
    {
        if (bubbleHighlighter != null) bubbleHighlighter.Unhighlight();
    }
    
   
    public bool Interact(Interactor interactor)
    {
        if (isInteracting) return false;

        // --- 1. Call the UNIQUE logic from the child class ---
        // We check if the pickup logic (e.g., adding ammo) was successful
        if (!OnPickup(interactor))
        {
           
            isInteracting = false;
            return false;
        }

        // --- 2. Run all the SHARED logic ---
        isInteracting = true;
        col.enabled = false;

        if (pickupSoundPlayer) pickupSoundPlayer.Play();

        if (pickupAnimator != null)
        {
            // Pass the interactor's transform for the "fly-to-player" effect
            pickupAnimator.PlayAndDestroy(interactor.transform);
        }
        else
        {
            Destroy(gameObject);
        }

        _onInteract?.Invoke();
        return true;
    }

  
    protected abstract bool OnPickup(Interactor interactor);
}