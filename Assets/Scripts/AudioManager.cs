using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] soundsFX;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in soundsFX)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            InitAudioSource(s, s.source);
        }
    }

    private void InitAudioSource(Sound s, AudioSource source)
    {
        source.clip = s.clip;
        source.volume = s.volume;
        source.pitch = s.pitch;
        source.loop = s.loop;
    }


    public void PlaySFX(string name)
    {
        Sound s = Array.Find(soundsFX, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("SoundFX named " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}

