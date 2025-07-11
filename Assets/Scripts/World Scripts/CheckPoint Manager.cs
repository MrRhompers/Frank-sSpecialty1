using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private GameObject player;

    public Transform[] checkpoints;
    public Transform[] respawnPoints;

    private CharacterController characterController;
    private int currentRespawnPoint = 0;
    Frank frankRef;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterController = player.GetComponent<CharacterController>();
        frankRef = FindAnyObjectByType<Frank>();
    }

    void Update()
    {
        CheckIfFallen();
    }

    void CheckIfFallen()
    {
        if (player.transform.position.y <= -20)
        {
            frankRef.PlayfallDeath();
            Respawn();
        }
    }

   public void Respawn()
    {
       
        if (characterController != null)
            characterController.enabled = false;

        player.transform.position = respawnPoints[currentRespawnPoint].position;

        if  (characterController != null)
            characterController.enabled = true;
    }

    public void UpdateCheckpoint(int checkpointIndex)
    {
        if (checkpointIndex > currentRespawnPoint)
        {
            currentRespawnPoint = checkpointIndex;
        }
    }
}
