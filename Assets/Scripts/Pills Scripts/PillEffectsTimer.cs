using UnityEngine;
using UnityEngine.UI;

public class PillEffectsTimer : MonoBehaviour
{
    private Movement movementScript;
    private ShrinkPill shrinkPillScript;
    private RevealPill revealPillScript;
    private TitaniumPill titaniumPillScript;
    private FrankPill FrankPillScript;

    public float totalSlickTime;
    public float totalFloatTIme;

    public Slider slick;
    public Slider floating;
    public Slider shrink;
    public Slider reveal;
    public Slider titanium;
    public Slider Frank;

    private void Start()
    {
        movementScript = FindAnyObjectByType<Movement>();
        shrinkPillScript = FindAnyObjectByType<ShrinkPill>();
        revealPillScript = FindAnyObjectByType<RevealPill>();
        titaniumPillScript = FindAnyObjectByType<TitaniumPill>();
        FrankPillScript = FindAnyObjectByType<FrankPill>(); 
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSliders();
    }


    void UpdateSliders()
    {

        slick.value =  movementScript.SlickPillTimer / totalSlickTime;
        floating.value = movementScript.FloatPillTimer / totalFloatTIme;
        shrink.value = 1 - (shrinkPillScript.elapsedTime / shrinkPillScript.pillLifetime);
        reveal.value = 1 - (revealPillScript.elapsedTime /revealPillScript.pillLifetime);
        titanium.value = 1 - (titaniumPillScript.elapsedTime / titaniumPillScript.pillLifetime);
        Frank.value = 1 - (FrankPillScript.elapsedTime /FrankPillScript.pillLifetime);
    }
}
