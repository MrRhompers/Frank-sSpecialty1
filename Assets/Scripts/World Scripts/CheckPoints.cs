using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public int checkpointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPointManager manager = FindAnyObjectByType<CheckPointManager>();
            manager.UpdateCheckpoint(checkpointIndex);
        }
    }
}
