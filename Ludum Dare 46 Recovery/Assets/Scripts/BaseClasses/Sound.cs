using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    [HideInInspector]public AudioSource source;
    public string name;
    [Range(0f, 1f)] public float volume = 1;
    [Range(0.1f, 3f)] public float pitch = 1;
    public bool loop;
}
