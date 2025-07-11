using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int checkpointIndex;
    private Movement movementScript;
    private ShrinkPill shrinkPillScript;
    private RevealPill revealPillScript;
    private TitaniumPill titaniumPillScript;

    private void Start()
    {
        //movementScript = FindAnyObjectByType<Movement>();
        //shrinkPillScript = FindAnyObjectByType<ShrinkPill>();
        //revealPillScript = FindAnyObjectByType<RevealPill>();
        //titaniumPillScript = FindAnyObjectByType<TitaniumPill>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //movementScript.SlickPillTimer = 0f;
            //movementScript.FloatPillTimer = 0f;
            //shrinkPillScript.UnShrink();
            //revealPillScript.ResetPillEffects();
            //titaniumPillScript.ResetPillEffects();
            CheckPointManager manager = FindAnyObjectByType<CheckPointManager>();
            manager.UpdateCheckpoint(checkpointIndex);
        }
    }
}
