using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DoorSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    /// <summary>
    /// Plays the correct sound based on the door's state.
    /// </summary>
    public void Play(bool isOpen)
    {
        // If the door is now open, play the open sound
        if (isOpen)
        {
            if (openSound != null)
            {
                audioSource.PlayOneShot(openSound);
            }
        }
        // If the door is now closed, play the close sound
        else
        {
            if (closeSound != null)
            {
                audioSource.PlayOneShot(closeSound);
            }
        }
    }
}