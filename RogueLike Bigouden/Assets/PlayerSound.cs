using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private SoundManager _soundManager;

    private void Start()
    {
        _soundManager = FindObjectOfType<SoundManager>();
    }

    public void PlaySound(string soundName)
    {
        _soundManager.PlaySound(soundName);
    }
}
