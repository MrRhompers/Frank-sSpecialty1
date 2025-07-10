using UnityEngine;

public class RevealPill : MonoBehaviour
{
    public bool isRevealed = false;
    public GameObject mainCamera;
    public GameObject Icon;

    [SerializeField] private float pillLifetime;

    private Renderer meshRenderer;
    private Collider Collider;

    private bool timerStarted = false;
    private float elapsedTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRevealed)
        {
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
                meshRenderer.enabled = false;
                Collider.enabled = false;
                mainCamera.SetActive(false);
                Icon.SetActive(true);
                Invoke("ResetPillEffects", pillLifetime);
        }
        }
    }


    void ResetPillEffects()
    {
        isRevealed = false;
        mainCamera.SetActive(true);
        timerStarted = false;
        elapsedTime = 0;
    }
}
