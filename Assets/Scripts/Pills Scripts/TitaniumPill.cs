using UnityEngine;
using System.Collections;

public class TitaniumPill : MonoBehaviour
{
    public float pillLifetime;

    public GameObject Icon;

    public bool hasTitaniumPill = false;

    private Movement movementScript;
    private Inventory inventoryScript;

    private Renderer meshRenderer;
    private Collider Collider;


    public bool timerStarted = false;
    public bool effectStarted = false;
    public bool canUsePill = false;
    private float elapsedTime;
    public AudioSource audioplay;
    public AudioClip titaniumcollect;

    private void Start()
    {
        movementScript = FindAnyObjectByType<Movement>();
        inventoryScript = FindAnyObjectByType<Inventory>();
        meshRenderer = GetComponent<Renderer>();
        Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (hasTitaniumPill && inventoryScript.canUsePill && effectStarted)
        {
            meshRenderer.enabled = false;
            Collider.enabled = false;

            KeyCode[] directions = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
            ShuffleArray(directions);

            movementScript.SetScrambledControls(directions[0], directions[2], directions[1], directions[3]);

            effectStarted = false;
            canUsePill = true;
      
        }

        if (timerStarted)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= pillLifetime)
            {
                ResetPillEffects();
            }

        }

    }

    public void RandomControls()
    {
        meshRenderer.enabled = false;
        Collider.enabled = false;

        KeyCode[] directions = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
        ShuffleArray(directions);

        movementScript.SetScrambledControls(directions[0], directions[2], directions[1], directions[3]);

        Invoke("ResetPillEffects", pillLifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           Icon.SetActive(true);
           hasTitaniumPill = true;
           RandomControls();
            audioplay.PlayOneShot(titaniumcollect);
        }
    }

    private void ShuffleArray(KeyCode[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rand = Random.Range(i, array.Length);
            KeyCode temp = array[i];
            array[i] = array[rand];
            array[rand] = temp;
        }
    }

    public void ResetPillEffects()
    {
        hasTitaniumPill = false;
        movementScript.ResetControls();
        elapsedTime = 0;
        inventoryScript.canUsePill = false;
        timerStarted = false;
        canUsePill = false;
    }

   
}
