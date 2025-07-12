using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Frank : MonoBehaviour
{
    public AudioClip[] FallingDie;
    public AudioClip[] CrushedDie;
    public AudioClip[] SpikeDie;
    public AudioClip[] FireDie;
    public AudioClip[] FrankPill;
    public AudioClip[] WaterDeath;
    public AudioSource audioPlay;

    private Queue<AudioClip> clipQueue = new Queue<AudioClip>();
    private bool isPlaying = false;

    void Start()
    {
        StartCoroutine(PlayQueue());
    }

    public void PlayfallDeath()
    {
        EnqueueClip(FallingDie);
    }

    public void PlaycrushedDeath()
    {
        EnqueueClip(CrushedDie);
    }

    public void PlaySpikeDeath()
    {
        EnqueueClip(SpikeDie);
    }

    public void PlayfireDeath()
    {
        EnqueueClip(FireDie);
    }

    public void PlayFrankPill()
    {
        EnqueueClip(FrankPill);
    }

    public void Playwaterdeath()
    {
        EnqueueClip(WaterDeath);
    }

    private void EnqueueClip(AudioClip[] clipArray)
    {
        if (clipArray == null || clipArray.Length == 0) return;

        AudioClip selected = clipArray[Random.Range(0, clipArray.Length)];
        clipQueue.Enqueue(selected);
    }

    private IEnumerator PlayQueue()
    {
        while (true)
        {
            if (!isPlaying && clipQueue.Count > 0)
            {
                AudioClip nextClip = clipQueue.Dequeue();
                audioPlay.clip = nextClip;
                audioPlay.Play();
                isPlaying = true;

                yield return new WaitForSeconds(nextClip.length);
                isPlaying = false;
            }

            yield return null;
        }
    }

    public void PlayQueuedClip(AudioClip clip)
    {
        if (clip != null)
            clipQueue.Enqueue(clip);
    }

    public void StopAudio()
    {
        audioPlay.Stop();
        isPlaying = false;
    }
}
