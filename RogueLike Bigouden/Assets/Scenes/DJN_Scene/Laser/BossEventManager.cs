using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventManager : MonoBehaviour
{
    public Pillar[] Pillars;
    private Beam laserBeam;
    public GameObject[] laser;

    [Header("Flame Strike")]
    public int groupCount;
    public bool stopPillard;
    public float waitTime;

  
    public void LoadBeam(int pillardSelect)
    {
        laserBeam = laser[pillardSelect].GetComponent<Beam>();
        laserBeam.ghostTarget.position = laserBeam.originTarget.position;
        laserBeam.isActive = true;
       
    }

    public void LoadFS(int pillardSelect)
    {
        StartCoroutine(FlameStrikeTimer(0, pillardSelect));
    }

    IEnumerator FlameStrikeTimer(int toActive, int selectedPillard)
    {
        StartFlameStrike(toActive, selectedPillard);
        toActive++;
        
        yield return new WaitForSeconds(waitTime);
        
        if (toActive >= Pillars[selectedPillard].flameStrikes.Length)
        {
            StopCoroutine(FlameStrikeTimer(toActive, selectedPillard));
        }
        
        if (toActive < Pillars[selectedPillard].flameStrikes.Length)
        {
            StartCoroutine(FlameStrikeTimer(toActive, selectedPillard));
        }
        
     
    }

    void StartFlameStrike(int toActive, int selectedPillard)
    {
        for (int i = 0; i < Pillars[selectedPillard].flameStrikes[toActive]._dalles.Length; i++)
        {
            Pillars[selectedPillard].flameStrikes[toActive]._dalles[i].GetComponent<FlameStrike>().LoadTint();
        }
    }

    [Serializable]
    public struct Pillar
    {
        public GameObject pilarGO;
        public FSGroups[] flameStrikes;

        [Serializable]
        public struct FSGroups
        {
            public GameObject[] _dalles;
        }
        
    }
    
}
