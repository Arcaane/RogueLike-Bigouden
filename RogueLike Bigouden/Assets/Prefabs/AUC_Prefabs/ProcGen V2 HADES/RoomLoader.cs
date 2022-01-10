using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RoomLoader : MonoBehaviour
{
    // Scene State
    public GameObject[] doors;
    public LayerMask isEnnemy;
    public GameObject _waveManager;
    private bool stopCheckEnemies;

    private bool isDoorOpen;
    public List<GameObject> LightsObj;
    // PostProcess
    public Volume volume;
    private Bloom bloom;
    private ChromaticAberration _chromaticAberration;
    private float blurTime = 3f;
    
    public List<GameObject> enemyList = new List<GameObject>();

    private void Update()
    {
        if (isDoorOpen)
        {
            RoomIntensity();
        }
    }

    private void Awake()
    {
        _waveManager.SetActive(false);
    }

    private void Start()
    {
        volume.profile.TryGet<Bloom>(out bloom);
        volume.profile.TryGet<ChromaticAberration>(out _chromaticAberration);

        InvokeRepeating(nameof(CheckforEnnemies), 1, 2f);
    }
    
    private void CheckforEnnemies()
    {
        if (stopCheckEnemies)
        {
            return;
        }
        else
        {
            enemyList.Clear();
            Collider2D[] ennemyInRoom = Physics2D.OverlapCircleAll(transform.position, 10f, isEnnemy);
            foreach (var ctx in ennemyInRoom)
            {
                enemyList.Add(ctx.gameObject);
            }

            foreach (var _e in enemyList)
            {
                if (_e.GetComponent<EnnemyStatsManager>().lifePoint <= 0)
                {
                    enemyList.Remove(_e);
                }
            }

            if (enemyList.Count == 0)
            {
                stopCheckEnemies = true;
                _waveManager.SetActive(true);
            }
        }
    }

    public void ClearedRoom()
    {
        isDoorOpen = true;
        foreach (var d in doors)
        {
            d.GetComponent<DoorInteractions>().isRoomClear = true;
        }
        
    }
    
    private void RoomIntensity()
    {
        blurTime -= Time.deltaTime;
        if (blurTime <= 0)
        {
            blurTime = 0;
            isDoorOpen = false;
        }
        
        foreach (var light in LightsObj)
        {
            light.GetComponent<Light2D>().intensity = Mathf.Lerp(light.GetComponent<Light2D>().intensity, 5f, blurTime);
        }
        _chromaticAberration.intensity.value = Mathf.Lerp(0, 0.6f, blurTime);
        bloom.intensity.value = Mathf.Lerp(1, 5, blurTime);
    }
}