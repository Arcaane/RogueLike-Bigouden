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
        
        //MusicZone();
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        s.source.Play();
    }

    private void MusicZone()
    {
        //if level.type = beginning
        PlaySound(lowLevelMusic);
        
        //if level.type = middle
        PlaySound(mediumLevelMusic);
        
        //if level.type = end
        PlaySound(highLevelMusic);
        
        //if level.type = boss room
        PlaySound(bossLevelMusic);
    }
}
