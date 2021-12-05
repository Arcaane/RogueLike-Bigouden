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
        //waveSpawner.WaveCompleted();
    }

    private void SkipEnemies()
    {
        foreach (var _e in waveSpawner.EnnemiesSpawned)
        {
            Debug.Log(_e.name + " Skipped !");
            _e.GetComponent<EnnemyStatsManager>().TakeDamage(100);
        }
    } 
}
