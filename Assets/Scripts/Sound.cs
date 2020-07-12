using UnityEngine;

/// <summary>
/// Represent a sound to be played within the app.
/// </summary>
[System.Serializable]
public class Sound
{
    /// <summary>
    /// Name that sound should be refered as.
    /// </summary>
    public string name;

    /// <summary>
    /// Sound file to be played
    /// </summary>
    public AudioClip clip;

    /// <summary>
    /// Volume the sound must be played
    /// </summary>
    [Range(0f, 3f)]
    public float volume = 1f;
    /// <summary>
    /// Pitch the sound must be played
    /// </summary>
    [Range(.1f, 3f)]
    public float pitch = 1f;
    /// <summary>
    /// Must this sound be played in loop ?
    /// </summary>
    public bool loop;

    /// <summary>
    /// AudioSource playing the sound
    /// </summary>
    [HideInInspector]
    public AudioSource source;
}
