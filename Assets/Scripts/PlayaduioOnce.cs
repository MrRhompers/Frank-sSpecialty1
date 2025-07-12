using UnityEngine;

public class PlayaduioOnce : MonoBehaviour
{
    public AudioClip audioClip; // Assign the audio clip in the Inspector
    private bool hasPlayed = false; // Flag to prevent multiple plays

    private void OnTriggerEnter(Collider other)
    {
        // Check if the audio has already played
        if (!hasPlayed)
        {
            // Play the audio clip
            AudioSource.PlayClipAtPoint(audioClip, transform.position);

            // Set the flag to true
            hasPlayed = true;

            // Destroy the object
            Destroy(gameObject);
        }
    }
}
