using UnityEngine;

public class RevealPill : MonoBehaviour
{
    public bool isRevealed = false;
    public GameObject mainCamera;
    public GameObject Icon;
    public GameObject slider;
    public float pillLifetime;

    private Renderer meshRenderer;
    private Collider Collider;

    public bool timerStarted = false;
    public float elapsedTime;
    public AudioSource audioplay;
    public AudioClip revealcollect;

    private Frank frankRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        Collider = GetComponent<Collider>();
        frankRef = FindAnyObjectByType<Frank>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRevealed)
        {
            slider.SetActive(true);
            mainCamera.SetActive(false);
            timerStarted = true;

            if (timerStarted)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime >= pillLifetime)
                {
                    ResetPillEffects();
                }
                
            }

    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isRevealed = true;
            
             if (isRevealed)
        {
                slider.SetActive(true);
                meshRenderer.enabled = false;
                Collider.enabled = false;
                mainCamera.SetActive(false);
                Icon.SetActive(true);
                Invoke("ResetPillEffects", pillLifetime);
                frankRef.PlayQueuedClip(revealcollect);
            }
        }
    }


   public void ResetPillEffects()
    {
        slider.SetActive(false);
        isRevealed = false;
        mainCamera.SetActive(true);
        timerStarted = false;
        elapsedTime = 0;
    }
}
