using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound[] musics;
     public string lowLevelMusic;
     public int roomToLoadLow;
     public string mediumLevelMusic;
     public int roomToLoadMedium;
     public string highLevelMusic;
     public int roomToLoadHigh;
     public string bossLevelMusic;
     public int roomToLoadBoss;
    
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject); // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
        
        foreach (Sound s in sounds)
        {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.loop = s.loop;
                s.source.pitch = s.pitch;
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

    public void ResetSound()
    {
        StopMusic(lowLevelMusic);
        StopMusic(mediumLevelMusic);
        StopMusic(highLevelMusic);
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        s.source.Play();
    }

    private void LoadMusic(string name)
    {
        Sound m = Array.Find(musics, music => music.soundName == name);
        m.source.Play();
    }

    private void StopMusic(string name)
    {
        Sound m = Array.Find(musics, music => music.soundName == name);
        m.source.Stop();
    }

    public void StartMusic()
    {
        if (LoadManager.LoadManagerInstance.currentRoom == roomToLoadLow)
        {
            LoadMusic(lowLevelMusic);
        }
        
        if (LoadManager.LoadManagerInstance.currentRoom == roomToLoadMedium)
        {
            LoadMusic(mediumLevelMusic);
            StopMusic(lowLevelMusic);
            
        }
        
        if (LoadManager.LoadManagerInstance.currentRoom == roomToLoadHigh)
        {
            LoadMusic(highLevelMusic);
            StopMusic(mediumLevelMusic);
        }
        
        if (LoadManager.LoadManagerInstance.currentRoom == roomToLoadBoss)
        {
            LoadMusic(bossLevelMusic);
            StopMusic(highLevelMusic);
        }
    }
    
}
