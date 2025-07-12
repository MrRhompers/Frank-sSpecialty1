using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TakeThePill : MonoBehaviour
{
    public string levelName; // Name of the level to load
    public AudioSource audioSource; // Audio source component
    public float fadeDuration = 2f; // Duration of the fade to black

    private bool playerInTrigger = false;

    private void Update()
    {
        // Check if the player is in the trigger and presses the E key
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            // Play the audio
            audioSource.Play();

            // Wait for the audio to finish playing
            StartCoroutine(WaitForAudio());
        }
    }

    private IEnumerator WaitForAudio()
    {
        // Wait for the audio to finish playing
        yield return new WaitForSeconds(audioSource.clip.length);

        // Fade the camera to black
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        // Get the camera's renderer
        Renderer cameraRenderer = Camera.main.GetComponent<Renderer>();

        // If no renderer is found, add one
        if (!cameraRenderer)
        {
            cameraRenderer = Camera.main.gameObject.AddComponent<Renderer>();
        }

        // Create a new color with alpha 0 (transparent) to alpha 1 (black)
        Color color = new Color(0, 0, 0, 0);
        Color targetColor = new Color(0, 0, 0, 1);

        // Fade the camera to black
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            Camera.main.backgroundColor = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Load the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the trigger
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exited the trigger
        if (other.gameObject.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
