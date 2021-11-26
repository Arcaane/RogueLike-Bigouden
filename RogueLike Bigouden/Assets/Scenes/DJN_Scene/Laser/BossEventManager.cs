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
    public int selectedPillard;
    public bool stopPillard;
    public float waitTime;

    private void Start()
    {
        laserBeam = laser[selectedPillard].GetComponent<Beam>();
        laserBeam.ghostTarget.position = laserBeam.originTarget.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stopPillard = !stopPillard;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            laserBeam.isActive = true;
            laserBeam = laser[selectedPillard].GetComponent<Beam>();
            laserBeam.ghostTarget.position = laserBeam.originTarget.position;
            
        }


        if (stopPillard && groupCount < Pillars[selectedPillard].flameStrikes.Length)
        {
            StartCoroutine(FlameStrikeTimer());
        }
        
        if (!stopPillard || groupCount == Pillars[selectedPillard].flameStrikes.Length)
        {
            groupCount = 0;
            stopPillard = false;
            StopCoroutine(FlameStrikeTimer());
        }
        
        
    }

    IEnumerator FlameStrikeTimer()
    {
        StartFlameStrike();
        yield return new WaitForSeconds(waitTime);
        groupCount++;
        

    }

    void StartFlameStrike()
    {
        for (int i = 0; i < Pillars[selectedPillard].flameStrikes[groupCount]._dalles.Length; i++)
        {
            Pillars[selectedPillard].flameStrikes[groupCount]._dalles[i].GetComponent<FlameStrike>().LoadTint();
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
