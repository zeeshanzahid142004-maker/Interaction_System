using UnityEngine;

public class PickupSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] [Range(0f, 1f)] private float volume = 1.0f;

    public void Play()
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
        }
    }
}