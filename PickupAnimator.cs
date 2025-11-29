using UnityEngine;
using System.Collections;

public class PickupAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [Tooltip("How high above the player's root (feet) to fly towards.")]
    [SerializeField] private float targetHeightOffset = 1.2f; // Flies to 1.2m above the feet
    [SerializeField] private float animationTime = 0.25f; // Total time for the animation
    [SerializeField] private float rotationSpeed = 720f; // Degrees per second

    /// <summary>
    /// This is the public method the pickup script (Ammo, Health, etc.) will call.
    /// It now requires a 'target' (the player) to fly towards.
    /// </summary>
    public void PlayAndDestroy(Transform target)
    {
        // Start the animation, passing in the player's transform as the target
        StartCoroutine(FlyAndShrinkSequence(target));
    }

    private IEnumerator FlyAndShrinkSequence(Transform target)
    {
        float timer = 0f;
        Vector3 startPosition = transform.position;
        Vector3 startScale = transform.localScale;

        while (timer < animationTime)
        {
            float t = timer / animationTime; // A value from 0.0 to 1.0
            
            // --- THIS IS THE FIX ---
            // Get the target's current position and add the offset
            Vector3 targetPosition = target.position + (Vector3.up * targetHeightOffset);
            // -----------------------

            // Move towards the new target position
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Shrink down to nothing
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

            // Add rotation for extra flair
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

            timer += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Now that the animation is done, destroy the object
        // We destroy the *root* of the prefab
        Destroy(transform.root.gameObject);
    }
}