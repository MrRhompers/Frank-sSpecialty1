using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Movement movementScript;
    private ShrinkPill shrinkPillScript;
    private RevealPill revealPillScript;
    private TitaniumPill TitaniumPillScript;

    private bool canPress1;
    private bool canPress2;
    private bool canPress3;
    private bool canPress4;
    private bool canPress5;
    private bool canPress6;

    public bool canUsePill = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementScript = FindAnyObjectByType<Movement>();
        revealPillScript = FindAnyObjectByType<RevealPill>();
        shrinkPillScript = FindAnyObjectByType<ShrinkPill>();
        TitaniumPillScript = FindAnyObjectByType<TitaniumPill>();
       
    }

    // Update is called once per frame
    void Update()
    {
        CheckPills();
        UsePills();
     
    }

    void CheckPills()
    {
        if (movementScript.hasSlickPill)
        {
            canPress1 = true;
        }
        if (movementScript.hasFloatPill)
        {
            canPress2 = true;
        }
        if (shrinkPillScript.hasShrunk)
        {
            canPress3 = true;
        }
        if (revealPillScript.isRevealed)
        {
            canPress4 = true;
        }
        if (TitaniumPillScript.hasTitaniumPill)
        {
            canPress5 = true;
            
        }
    }

    void UsePills()
    {
        if (canPress1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !movementScript.PillReadySlick)
            {
                movementScript.slickEffectStarted = false;
                movementScript.hasSlickPill = true;
                movementScript.PillReadySlick = true;
                movementScript.SlickPill();
            }
        }

        if (canPress2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2) && !movementScript.PillReadyFloat)
            {
                movementScript.floatEffectStarted = false;
                movementScript.hasFloatPill = true;
                movementScript.PillReadyFloat = true;   
                movementScript.FloatPill();
            }
        }

        if (canPress3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
               shrinkPillScript.hasShrunk = true;
                
            }
        }

        if (canPress4)
        {
          
            if (Input.GetKeyUp(KeyCode.Alpha4))
            {
               
                revealPillScript.isRevealed = true;
               
            }
        }

        if (canPress5)
        {

            if (Input.GetKeyUp(KeyCode.Alpha5) && !TitaniumPillScript.canUsePill)
            {

                TitaniumPillScript.hasTitaniumPill = true;
                TitaniumPillScript.effectStarted = true;
                TitaniumPillScript.timerStarted = true;
                canUsePill = true;
            }
        }
    }
    
}
