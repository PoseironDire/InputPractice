using System;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 260)]
    public int priority;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 10f)]
    public float pitch;
    public bool loop;

    [HideInInspector] public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public bool soundtrack;
    public bool ambience;
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.priority = s.priority;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        if (soundtrack)
            Play("Soundtrack");
        if (ambience)
            Play("Ambience");
    }

    void Update()
    {
        AudioSource[] sources = gameObject.GetComponents<AudioSource>();
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = sources[i];
            sounds[i].source.clip = sounds[i].clip;

            sounds[i].source.priority = sounds[i].priority;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
        }
    }

    public void Volume(float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Soundtrack");
        s.volume = volume;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); if (s == null) return;
        s.source.Play();
    }
}
