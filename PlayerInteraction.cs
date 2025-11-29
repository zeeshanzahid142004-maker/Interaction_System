using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

// This line ensures the PlayerInput component is available
[RequireComponent(typeof(PlayerInput))]
public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Interactor interactor;

    private List<Iinteractable> interactablesInRange = new List<Iinteractable>();
    private Iinteractable closestInteractable;

    private PlayerInput playerInput;

    private const float detectRadius = 2f; // same range as your trigger

    // --- SETUP ---
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        if (playerInput != null)
        {
            playerInput.actions["Interact"].performed += OnInteractPressed;
        }
    }

    void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.actions["Interact"].performed -= OnInteractPressed;
        }
    }

    // --- CORE UPDATE LOOP ---
    void Update()
    {
        FindClosestInteractable();

        if (closestInteractable != null)
        {
            interactor.ShowPrompt(closestInteractable);
        }
        else
        {
            interactor.ClearPrompt();
        }
    }

    // --- INTERACT KEY PRESSED ---
    private void OnInteractPressed(InputAction.CallbackContext context)
    {
        Debug.LogWarning("INTERACT KEY PRESSED!");

        if (closestInteractable != null)
        {
            Debug.Log("Telling " + (closestInteractable as MonoBehaviour).gameObject.name + " to Interact!");
            closestInteractable.Interact(interactor);
        }
        else
        {
            Debug.Log("Pressed Interact, but closestInteractable is null.");
        }
    }

    // --- TRIGGER ENTER / EXIT ---
    private void OnTriggerEnter(Collider other)
    {
        Iinteractable interactable = other.GetComponent<Iinteractable>();
        if (interactable != null && !interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
            Debug.Log("Trigger ENTER: Added " + other.gameObject.name, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Iinteractable interactable = other.GetComponent<Iinteractable>();
        if (interactable != null && interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Remove(interactable);
            Debug.Log("Trigger EXIT: Removed " + other.gameObject.name, other.gameObject);
        }
    }

    // --- CORE DETECTION (WITH FIX) ---
    private void FindClosestInteractable()
    {
        closestInteractable = null;
        float minDistance = float.MaxValue;

        // CLEAN UP OR INVALIDATE OLD ONES
        for (int i = interactablesInRange.Count - 1; i >= 0; i--)
        {
            Iinteractable interactable = interactablesInRange[i];

            if (interactable == null)
            {
                interactablesInRange.RemoveAt(i);
                continue;
            }

            MonoBehaviour mb = interactable as MonoBehaviour;

            if (mb == null || !mb.gameObject.activeInHierarchy)
            {
                interactablesInRange.RemoveAt(i);
                continue;
            }

            Collider col = mb.GetComponent<Collider>();
            if (col == null || !col.enabled)
            {
                interactablesInRange.RemoveAt(i);
                continue;
            }
        }

        // ---------------------------------------------------------
        // ✨ FIX: REDETECT RE-ENABLED COLLIDERS USING OVERLAPSPHERE
        // ---------------------------------------------------------
        Collider[] hits = Physics.OverlapSphere(transform.position, detectRadius);

        foreach (Collider hit in hits)
        {
            Iinteractable interactCheck = hit.GetComponent<Iinteractable>();
            if (interactCheck == null) continue;

            MonoBehaviour mbCheck = interactCheck as MonoBehaviour;
            if (mbCheck == null || !mbCheck.enabled) continue;

            Collider colCheck = mbCheck.GetComponent<Collider>();
            if (colCheck == null || !colCheck.enabled) continue;

            if (!interactablesInRange.Contains(interactCheck))
            {
                interactablesInRange.Add(interactCheck);
                Debug.Log("Re-detected interactable: " + mbCheck.name);
            }
        }
        // ---------------------------------------------------------

        // FIND THE CLOSEST VALID INTERACTABLE
        foreach (Iinteractable interact in interactablesInRange)
        {
            MonoBehaviour mb = interact as MonoBehaviour;
            if (mb == null) continue;

            float distance = Vector3.Distance(transform.position, mb.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestInteractable = interact;
            }
        }
    }
}
