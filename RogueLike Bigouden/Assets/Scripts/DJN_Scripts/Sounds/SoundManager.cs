using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound[] musics;
    [SerializeField] private string lowLevelMusic;
    [SerializeField] private string mediumLevelMusic;
    [SerializeField] private string highLevelMusic;
    [SerializeField] private string bossLevelMusic;
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = s.mixerGroup;
        }
        
        foreach (Sound m in musics)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
            m.source.loop = m.loop;
            m.source.outputAudioMixerGroup = m.mixerGroup;
        }
        
    }
    

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        s.source.Play();
    }

    public void LoadMusic(string name)
    {
        Sound m = Array.Find(musics, music => music.soundName == name);
        m.source.Play();
        
    }
}
