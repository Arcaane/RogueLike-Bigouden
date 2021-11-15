using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSkip : MonoBehaviour
{
    public WaveSpawner waveSpawner;

    public void SkipWave()
    {
        SkipEnemies();
        waveSpawner.WaveCompleted();
    }

    private void SkipEnemies()
    {
        int i = 0;
        foreach (var _e in waveSpawner.EnnemiesSpawned)
        {
            i++;
            Debug.Log(_e.name + " Skipped !");
            _e.GetComponent<EnnemyStatsManager>().TakeDamage(999);
            
        } 
        Debug.Log(i);
    }
}
