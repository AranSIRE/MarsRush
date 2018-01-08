using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3.0f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
    
    public void setVolume(float v)
    {
        volume = v;
    }


}