using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSound : MonoBehaviour
{
    private SoundManager m_soundManager;
    private void Awake()
    {
        m_soundManager = FindObjectOfType<SoundManager>();
    }

    public void PlaySound(string soundName)
    {
        m_soundManager.PlaySound(soundName);
    }
}

public static class AudioConstant
{
    #region Player
    public static string playerHit = "Player Hit";
    #endregion
}
