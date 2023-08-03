using UnityEngine.Audio;
using System;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource foodSource;
    [SerializeField] AudioSource playerSource;
    [SerializeField] AudioSource zombieSource;
    [SerializeField] AudioSource barrierSource;
    [SerializeField] AudioSource[] sounds;
    [SerializeField] List<AudioClip> soundSources = new List<AudioClip>();

    void Awake()
    {
        instance = this;

        PlayMorningAfternoonAudio(0);
    }

    public void PlayMorningAfternoonAudio(int index)
    {
        if (index >= 0 && index < sounds.Length)
        {
            sounds[index].Play();           
        }
    }

    public void PlayNightAudio(int index)
    {
        if (index >= 0 && index < sounds.Length)
        {
            sounds[0].Stop();
            sounds[index].Play();
        }
    }

    public void PlayFoodCookAudio()
    {
        AudioClip clip = soundSources[0];
        foodSource.PlayOneShot(clip, 0.4f);
    }

    public void PlayFoodPreparedAudio()
    {
        AudioClip clip = soundSources[1];
        foodSource.PlayOneShot(clip, 0.4f);
    }

    public void PlayFoodServeAudio()
    {
        AudioClip clip = soundSources[2];
        playerSource.PlayOneShot(clip);
    }

    public void PlayFoodThrowAudio()
    {
        AudioClip clip = soundSources[3];
        playerSource.PlayOneShot(clip);
    }

    public void PlayNormalZombieAudio()
    {
        AudioClip clip = soundSources[4];
        zombieSource.PlayOneShot(clip);
    }

    public void PlayAcidZombieAudio()
    {
        AudioClip clip = soundSources[5];
        zombieSource.PlayOneShot(clip);
    }

    
    public void PlayBruteZombieAudio()
    {
        AudioClip clip = soundSources[6];
        zombieSource.PlayOneShot(clip);
    }

    public void PlayClownZombieAudio()
    {
        AudioClip clip = soundSources[7];
        zombieSource.PlayOneShot(clip);
    }

    public void PlayClownZombieJumpScareAudio()
    {
        AudioClip clip = soundSources[8];
        zombieSource.PlayOneShot(clip);
    }

    public void PlayBarrierDestroyAudio()
    {
        AudioClip clip = soundSources[9];
        barrierSource.PlayOneShot(clip);
    }

    public void PlayZombieAttackBarrierAudio()
    {
        AudioClip clip = soundSources[10];
        zombieSource.PlayOneShot(clip);
    }

    public void PlayZombieEatAudio()
    {
        AudioClip clip = soundSources[11];
        zombieSource.PlayOneShot(clip);
    }

    public void PlayBarrierNextDamageStateAudio()
    {
        AudioClip clip = soundSources[11];
        barrierSource.PlayOneShot(clip);
    }




}
