using UnityEngine;

public class FakeObject : MonoBehaviour
{
    [SerializeField] private Material originalMaterial;
    [SerializeField] private Material revealedMaterial;

    private RevealPill capsuleScript;
    private Collider Collider;
    private Renderer Renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        capsuleScript = FindAnyObjectByType<RevealPill>();
        Collider = GetComponent<Collider>();
        Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (capsuleScript.isRevealed)
        {
            Collider.enabled = false;
            Renderer.material = revealedMaterial;
        }
        else if (!capsuleScript.isRevealed)
        {
            Collider.enabled = true;
            Renderer.material = originalMaterial;
        }


    }
}
