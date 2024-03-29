using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    [SerializeField]
    public string bgmName;

    public AudioSource musicSource;
    
    public static AudioManager Instance;

    void Awake()
    {
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        PlayBGM(bgmName);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log(s.name);
        s.source.PlayOneShot(s.clip);
    }

    //public void Stop()
    //{
    //    AudioSource MusicAS = GetComponentInChildren(AudioSource);
    //}

    public void PlayBGM(string bgmName)
    {
        musicSource.Stop();
        Sound s = Array.Find(sounds, sound => sound.name == bgmName);
        musicSource.clip = s.clip;
        musicSource.Play();
    }

    public void NormZombSpawn()
    {
        int randomPair = UnityEngine.Random.Range(1, 5);

        if (randomPair == 1)
        {
            Play("NorSpawn1");
        }
        else if (randomPair == 2)
        {
            Play("NorSpawn2");
        }
        else if (randomPair == 3)
        {
            Play("NorSpawn3");
        }
        else if (randomPair == 4)
        {
            Play("NorSpawn4");
        }
        else
        {
            Play("NorSpawn5");
        }
    }

    public void Button()
    {
        Play("Button");
    }

    public void TutButton()
    {
        Play("TutButton");
    }
}