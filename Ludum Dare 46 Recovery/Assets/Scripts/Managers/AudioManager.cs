using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] soundFX;
    public GameObject soundFXList;
    public Sound[] musicFX;
    public GameObject musicFXList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(this.gameObject);


        foreach (Sound sound in soundFX)
        {
            sound.source = soundFXList.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = 1;
        }
        foreach (Sound sound in musicFX)
        {
            sound.source = musicFXList.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.spatialBlend = 1;
        }

        Play("Violin_Music", false,1,0.1f,false,0,0);
        Play("Ruin_Atmos", true, 1, 0.05f, false, 0,0);
    }

    public void Play(string name, bool soundType)//0 is SFX and 1 is MFX
    {
        Sound s = FindSoundInList(name, soundType);
        if(s.source.isPlaying == false)
            s.source.Play();
        Debug.Log("Played Sound " + name);
    }

    public void PlaySFX(string name)//0 is SFX and 1 is MFX
    {
        Sound s = FindSoundInList(name,true);
        if (s.source.isPlaying == false)
            s.source.Play();
    }

    public void ForcePlay(string name, bool soundType)//0 is SFX and 1 is MFX
    {
        Sound s = FindSoundInList(name, soundType);
            s.source.Play();
        Debug.Log("Played Sound " + name);
    }

    public void Play(string name, bool soundType, int priority, float volume, bool delay, float delayTime, int spacialBlend)//0 is SFX and 1 is MFX
    {
        Sound s = FindSoundInList(name, soundType);
        s.source.volume = volume;
        s.source.priority = priority;
        s.source.spatialBlend = spacialBlend;
        if (!delay && s.source.isPlaying == false)
            s.source.Play();
        else if (delay && s.source.isPlaying == false)
            s.source.PlayDelayed(delayTime);
        Debug.Log("Played Sound " + name);
    }

    public void Stop(string name, bool soundType)//0 is SFX and 1 is MFX
    {
        Sound s = FindSoundInList(name, soundType);
        s.source.Stop();
    }

    public void Pause(string name, bool soundType)//0 is SFX and 1 is MFX
    {
        Sound s = FindSoundInList(name, soundType);
        s.source.Pause();
    }
    
    public void ChangeVolume(float value, string name, bool soundType) // 0 for mute
    {
        Sound s = FindSoundInList(name, soundType);
        if (value > 0)
            s.source.volume = value;
        else
            s.source.mute = true;
    }

    private Sound FindSoundInList(string name, bool soundType)
    {
        Sound s;
        if (soundType) { s = Array.Find(musicFX, sound => sound.name == name); }
        else { s = Array.Find(soundFX, sound => sound.name == name); }
        if (s == null)
            return null;
        return s;
    }

}
