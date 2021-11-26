using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
   public string soundName;
   public AudioClip clip;
   public float volume;
   public float pitch;
   public bool loop;

   public AudioMixerGroup mixerGroup;

   [HideInInspector] public AudioSource source;
   

}
