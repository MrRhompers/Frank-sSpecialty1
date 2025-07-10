using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Movement movementScript;
    private ShrinkPill shrinkPillScript;
    private RevealPill revealPillScript;
   

    private bool canPress1;
    private bool canPress2;
    private bool canPress3;
    private bool canPress4;
    private bool canPress5;
    private bool canPress6;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementScript = FindAnyObjectByType<Movement>();
        revealPillScript = FindAnyObjectByType<RevealPill>();
        shrinkPillScript = FindAnyObjectByType<ShrinkPill>();
       
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
    }

    void UsePills()
    {
        if (canPress1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                movementScript.PillReadySlick = true;
            }
        }

        if (canPress2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                movementScript.PillReadyFloat = true;
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
    }
    
}
