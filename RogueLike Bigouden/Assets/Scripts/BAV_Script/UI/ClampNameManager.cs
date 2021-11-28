using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ClampNameManager : MonoBehaviour
{
    [Header("UI Damage")] public List<TextMeshProUGUI> showNumber;
    public List<MannequinStatsManager> spawnRefUI;
    public Vector3 offsetPosition;
    public Camera camera;


    //Private Action
    [SerializeField] bool returnTime;
    [SerializeField] bool isDying;
    [SerializeField] private float counterBeforeRespawn = 10f;
    [SerializeField] private float counterTimer;
    [SerializeField] private float timerLimit = 2f;


    [SerializeField] float timerCounterSlow;

    //private
    private int lifePointObject;


    // Update is called once per frame
    void Update()
    {
        if (isDying)
        {
            LaunchRespawn();
        }


        Vector3 namePos = camera.WorldToScreenPoint(spawnRefUI[0].gameObject.transform.position + offsetPosition);
        showNumber[0].transform.position = namePos;
        TakeDamageUI();
    }


    void TakeDamageUI()
    {
        TextMeshProUGUI lifePoint = showNumber[0].GetComponent<TextMeshProUGUI>();
        lifePoint.text = spawnRefUI[0].lifePoint.ToString();
        if (spawnRefUI[0].lifePoint == 0)
        {
            isDying = true;
            Invoke(("Destroy"), 0.5f);
        }
    }

    public void LaunchRespawn()
    {
        counterTimer += Time.deltaTime;
        if (counterTimer < timerLimit)
        {
            TimeManager.SlowDownGame();
        }
        if (counterTimer > counterBeforeRespawn)
        {
            isDying = false;
            counterTimer = 0;
            Invoke(("Respawn"), 0.5f);
            spawnRefUI[0].lifePoint = spawnRefUI[0].ennemyData.lifePointSO;
        }
    }

    public void Respawn()
    {
        spawnRefUI[0].gameObject.SetActive(true);
        showNumber[0].gameObject.SetActive(true);
    }

    public void Destroy()
    {
        spawnRefUI[0].gameObject.SetActive(false);
        showNumber[0].gameObject.SetActive(false);
    }
}