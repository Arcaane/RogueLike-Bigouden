using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossEventManager : MonoBehaviour
{
    public static BossEventManager instance;
    [HideInInspector] public int currentPillar;

    [Header("Laser")]
    private Beam laserBeam;
    public GameObject[] laser;
    public Animator[] laserAnimator;

    [Header("Flame Strike")] 
    [SerializeField] private GameObject[] _dalles;
    [HideInInspector] public List<PillarsStatsManager> pillars;
    public float waitTime;

    [Header("Rotation Laser")] 
    public GameObject[] rotationLaser;
    public Animator rotationAnimator;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject); // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
    }

    private void Start()
    {
        pillars.AddRange(FindObjectsOfType<PillarsStatsManager>());

        /* if (player)
         {
             cinIsEnable = true;
             StartCoroutine(StartCinematic());
         } */
    }

    private void Update()
    {
        //Update Pillars
        foreach (PillarsStatsManager p in pillars)
        {
            if (p.isDestroyed)
            {
                pillars.Remove(p);
            }
        }
    }
    
    #region ABILITIES
    public void LoadBeam(int pillardSelect)
    {
        currentPillar = pillardSelect;
        laserBeam = laser[pillardSelect].GetComponent<Beam>();
        laserAnimator[pillardSelect].Play("TSpot_Turn");
        laserBeam.ghostTarget.position = laserBeam.originTarget.position;
        laserBeam.isActive = true;
    }

    public void LoadFS()
    {
        StartCoroutine(FlameStrikeTimer());
    }

    IEnumerator FlameStrikeTimer()
    {
        StartFlameStrike();
        yield return new WaitForSeconds(waitTime);
    }

    void StartFlameStrike()
    {
       int rFS = UnityEngine.Random.Range(0, _dalles.Length);
        
       _dalles[rFS].GetComponent<FlameStrike>().LoadTint();
    }
    
    #endregion

  
  
}
