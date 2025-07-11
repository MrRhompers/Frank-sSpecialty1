using UnityEngine;

public class Frank : MonoBehaviour
{
    public AudioClip[] FallingDie;
    public AudioClip[] CrushedDie;
    public AudioClip[] SpikeDie;
    public AudioClip[] FireDie;
    public AudioClip[] FrankPill;
    public AudioSource audioplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void PlayfallDeath() 
    {
        AudioClip RandomDeath = FallingDie[Random.Range(0, FallingDie.Length)];
        audioplay.PlayOneShot(RandomDeath);
    }
    public void PlaycrushedDeath()
    {
        AudioClip RandomDeath = CrushedDie[Random.Range(0, CrushedDie.Length)];
        audioplay.PlayOneShot(RandomDeath);
    }
    public void PlaySpikeDeath()
    {
        AudioClip RandomDeath = SpikeDie[Random.Range(0, SpikeDie.Length)];
        audioplay.PlayOneShot(RandomDeath);
    }
    public void PlayfireDeath()
    {
        AudioClip RandomDeath = FireDie[Random.Range(0, FireDie.Length)];
        audioplay.PlayOneShot(RandomDeath); 
    }
    public void PlayFrankPill()
    {
        AudioClip RandomDeath = FrankPill[Random.Range(0, FrankPill.Length)];
        audioplay.PlayOneShot(RandomDeath);
    }
}
