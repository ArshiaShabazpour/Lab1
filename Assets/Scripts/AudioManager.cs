using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip fruitPickupClip;
    public AudioClip enemyHitClip;
    public AudioClip enemyStompClip;
    public float volume = 1f;
    private AudioSource audioSource;

    public override void Awake()
    {
        base.Awake();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }


    public void Play(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip, volume);
    }


    public void PlayFruitPickup()
    {
        Play(fruitPickupClip);
    }


    public void PlayEnemyHit()
    {
        Play(enemyHitClip);
    }


    public void PlayEnemyStomp()
    {
        Play(enemyStompClip);
    }
}
