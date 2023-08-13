using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class NEWAudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public AudioSource musicSource;

    public static NEWAudioManager Instance;

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
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        Debug.Log(s.name);
        s.source.PlayOneShot(s.clip);
    }

    public void PlayNightBGM()
    {
        musicSource.Stop();
        Play("NightBGM");
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