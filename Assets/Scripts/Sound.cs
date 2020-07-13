using UnityEngine;

/// <summary>
/// Represent a sound to be played within the app.
/// </summary>
[System.Serializable]
public class Sound
{
    [Tooltip("Name that sound should be refered as.")]
    public string name;

    [Tooltip("Sound file to be played")]
    public AudioClip clip;
    [Tooltip("Volume the sound must be played")]
    [Range(0f, 1f)]
    public float volume = 1f;
    [Tooltip("Pitch the sound must be played")]
    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Tooltip("Must this sound be played in loop ?")]
    public bool loop;

    /// <summary>
    /// AudioSource playing the sound
    /// </summary>
    [HideInInspector]
    public AudioSource source;
}
