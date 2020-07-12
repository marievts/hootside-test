using UnityEngine;
using System;

/// <summary>
/// Manager all the sounds of the app
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Instance of itself to get a singleton pattern
    /// </summary>
    public static AudioManager instance;
    /// <summary>
    /// All sound effects needed for the app
    /// </summary>
    public Sound[] soundsFX;

    /// <summary>
    /// Singleton pattern :
    /// Instanciate a new AudioManager if no existing instance, 
    /// otherwise take the already existing instance.
    /// 
    /// Initialize an AudioSource for each Sound from the <see cref="soundsFX"/> list.
    /// </summary>
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

    /// <summary>
    /// Init Audio Source with the given sound and parameters it has
    /// </summary>
    /// <param name="s"></param>
    /// <param name="source"></param>
    private void InitAudioSource(Sound s, AudioSource source)
    {
        source.clip = s.clip;
        source.volume = s.volume;
        source.pitch = s.pitch;
        source.loop = s.loop;
    }

    /// <summary>
    /// Play a sound effect
    /// </summary>
    /// <param name="name">Name of the sound to be played</param>
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

