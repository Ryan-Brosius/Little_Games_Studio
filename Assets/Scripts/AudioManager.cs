using UnityEngine;
using System;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1.0f;

        [Range(0.5f, 1.5f)]
        public float pitch = 1.0f;

        public bool loop = false;

        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + soundName);
            return;
        }
        s.source.Play();
    }

    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound not found: " + soundName);
            return;
        }
        s.source.Stop();
    }

    public Sound getSound(string soundName)
    {
        return Array.Find(sounds, sound => sound.name == soundName);
    }
}

