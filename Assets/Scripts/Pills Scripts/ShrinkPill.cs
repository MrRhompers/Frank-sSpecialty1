using UnityEngine;

public class ShrinkPill : MonoBehaviour
{
    [SerializeField] private float pillLifetime;

    private GameObject Player;

    private Renderer meshRenderer;
    private Collider Collider;

    private Vector3 originalSize;
    private Vector3 shrunkSize = new Vector3(0.3f, 0.3f, 0.3f);

    private bool hasShrunk = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        meshRenderer = GetComponent<Renderer>();
        Collider = GetComponent<Collider>();
        originalSize = Player.transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hasShrunk = true;
            if (hasShrunk)
            {
                meshRenderer.enabled = false;
                Collider.enabled = false;
                Player.transform.localScale = shrunkSize;
                Invoke("UnShrink", pillLifetime);
            }

        }
    }

    void UnShrink()
    {
        hasShrunk = false;
        Player.transform.localScale = originalSize;
    }
}
