using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float interactDistance = 2.5f;
    [SerializeField] private float sphereRadius = 0.25f;

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
        bool hitSomething = Physics.SphereCast(
            _transform.position + Vector3.up * 0.3f,
            sphereRadius,
            _transform.forward,
            out hit,
            interactDistance,
            interactableMask
        );

        if (hitSomething)
        {
            Iinteractable interactable = hit.transform.GetComponent<Iinteractable>() ??
                                         hit.transform.GetComponentInParent<Iinteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                interactor.ShowPrompt(interactable);
            }
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
