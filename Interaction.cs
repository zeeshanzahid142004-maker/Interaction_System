using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float interactDistance = 2.5f;
    [SerializeField] private float sphereRadius = 0.25f;
    [SerializeField] private float overlapRadius = 0.8f;
    [SerializeField] private float overlapHeightOffset = 0.4f;

    private PlayerInput playerInput;
    private Transform _transform;
    private Interactor interactor;
    private Iinteractable currentInteractable;
    private RaycastHit hit;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _transform = transform;
        interactor = GetComponent<Interactor>();
    }

    private void OnEnable()
    {
        playerInput.actions["Interact"].performed += DoInteract;
    }

    private void OnDisable()
    {
        playerInput.actions["Interact"].performed -= DoInteract;
    }

    private void Update()
    {
        Iinteractable foundInteractable = null;

        // First try SphereCast forward with QueryTriggerInteraction.Collide
        bool hitSomething = Physics.SphereCast(
            _transform.position + Vector3.up * 0.3f,
            sphereRadius,
            _transform.forward,
            out hit,
            interactDistance,
            interactableMask,
            QueryTriggerInteraction.Collide
        );

        if (hitSomething)
        {
            // Check if collider is enabled and gameObject is active
            if (hit.collider != null && hit.collider.enabled && hit.collider.gameObject.activeInHierarchy)
            {
                foundInteractable = hit.transform.GetComponent<Iinteractable>() ??
                                    hit.transform.GetComponentInParent<Iinteractable>();
            }
        }

        // If no valid interactable found, use OverlapSphere as fallback for pickups at feet
        if (foundInteractable == null)
        {
            Vector3 overlapCenter = _transform.position + Vector3.up * overlapHeightOffset;
            Collider[] colliders = Physics.OverlapSphere(overlapCenter, overlapRadius, interactableMask, QueryTriggerInteraction.Collide);

            float closestDistance = float.MaxValue;
            foreach (Collider col in colliders)
            {
                // Check if collider is enabled and gameObject is active
                if (col == null || !col.enabled || !col.gameObject.activeInHierarchy)
                    continue;

                Iinteractable interactable = col.GetComponent<Iinteractable>() ??
                                             col.GetComponentInParent<Iinteractable>();

                if (interactable != null)
                {
                    float distance = Vector3.Distance(_transform.position, col.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        foundInteractable = interactable;
                    }
                }
            }
        }

        // Update current interactable and prompt (Interactor handles highlighting)
        if (foundInteractable != null)
        {
            currentInteractable = foundInteractable;
            interactor.ShowPrompt(foundInteractable);
        }
        else
        {
            currentInteractable = null;
            interactor.ClearPrompt();
        }

        // Debug visualization
        Debug.DrawRay(_transform.position + Vector3.up * 0.3f, _transform.forward * interactDistance,
            hitSomething ? Color.green : Color.red);
        Debug.DrawLine(_transform.position + Vector3.up * 0.3f, hit.point, Color.cyan);
    }

    private void DoInteract(InputAction.CallbackContext context)
    {
        if (currentInteractable == null) return;

        currentInteractable.Interact(interactor);
    }
}
