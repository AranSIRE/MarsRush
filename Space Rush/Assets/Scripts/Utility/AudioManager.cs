using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public Slider VolumeSlider;
    public Slider PitchSlider;    

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;           
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public float AudioClipLength(String name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.clip.length;
    }


    public void SetVolume()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = VolumeSlider.value;
        }
    }

}