using UnityEngine;

public class RevealPill : MonoBehaviour
{
    public bool isRevealed = false;
    public GameObject mainCamera;

    [SerializeField] private float pillLifetime;

    private Renderer meshRenderer;
    private Collider Collider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

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

                Invoke("ResetPillEffects", pillLifetime);
            }
        }
    }


    void ResetPillEffects()
    {
        isRevealed = false;
        mainCamera.SetActive(true);

    }
}
