using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private GameObject player;

    public Transform[] checkpoints;
    public Transform[] respawnPoints;

    private int currentRespawnPoint = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckIfFallen();
    }

    void CheckIfFallen()
    {
        if (player.transform.position.y <= -20)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        player.transform.position = respawnPoints[currentRespawnPoint].position;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    public void UpdateCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex > currentRespawnPoint)
        {
            currentRespawnPoint = checkpointIndex;
        }
    }
}
