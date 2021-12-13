using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventManager : MonoBehaviour
{
    [Header("Laser")]
    private Beam laserBeam;
    public GameObject[] laser;

    [Header("Flame Strike")] 
    [SerializeField] private GameObject[] _dalles;
    [HideInInspector] public List<PillarsStatsManager> pillars;
    public float waitTime;

    [Header("Cinematic")] 
    public Transform targetPDir;
    public bool cinIsEnable;
    private GameObject player;
    [SerializeField] private float walkSpeed;
   
    
    private void Start()
    {
        pillars.AddRange(FindObjectsOfType<PillarsStatsManager>());
        player = GameObject.FindGameObjectWithTag("Player");
        
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
        laserBeam = laser[pillardSelect].GetComponent<Beam>();
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
    
    #region CINEMATIC

    IEnumerator StartCinematic()
    {
        yield return new WaitForSeconds(0.5f);
        player.transform.position =
            Vector3.MoveTowards(player.transform.position, targetPDir.position, walkSpeed * Time.deltaTime);
        
        if (player.transform.position == targetPDir.position)
        {
            UIManager uiManager = FindObjectOfType<UIManager>();
            uiManager.dialogueBox.SetActive(true);
            uiManager.dialogueText.text = "Let's go to the fight"; //placeholder
            yield return new WaitForSeconds(2f);
            cinIsEnable = false;
        }
    }
    
    #endregion
}
