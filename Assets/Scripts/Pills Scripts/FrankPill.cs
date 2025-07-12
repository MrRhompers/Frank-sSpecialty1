using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class FrankPill : MonoBehaviour
{
    private GameObject Player;

    public float pillLifetime;


    public GameObject Icon;
    

    public GameObject Renderer;
    private Collider Collider;

    private Frank frankRef;

    public bool hasFrank = false;

    public bool timerStarted = false;
    public float elapsedTime;

    public GameObject slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        frankRef = FindAnyObjectByType<Frank>();
        Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
         if (hasFrank)
        {
            slider.SetActive(true);
            frankRef.PlayFrankPill();

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
            hasFrank = true;

            if (hasFrank)
            {
                Renderer.SetActive(false);
                Collider.enabled = false;
                frankRef.PlayFrankPill();
                Invoke ("ResetPillEffects", pillLifetime);
                Icon.SetActive(true);
            }
        }
    }

    void ResetPillEffects()
    {
        frankRef.StopAudio();
        slider.SetActive(false);
    }
}
