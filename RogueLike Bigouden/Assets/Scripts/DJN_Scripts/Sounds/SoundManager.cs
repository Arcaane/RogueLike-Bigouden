using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Sound[] soundList;

    public void PlaySound(string soundName)
    {
        
    }
    
    
    [Serializable]
    public struct Sound
    {
        public string soundName;
        public AudioClip sound;
        public AudioMixerGroup mixerGroup;
        public float pitch;
        public bool loop;

    }
}
