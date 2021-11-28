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
    [SerializeField] bool isDying;
    [SerializeField] private float counterBeforeRespawn = 10f;
    [SerializeField] private float counterTimer;
    [SerializeField] private float timerLimit = 3f;

    private float timerCounterSlow;
    private bool returnTime;

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
        if (returnTime)
        {
            TimeManager.SlowDownGame();
            CounterTimer();
        }
    }


    void TakeDamageUI()
    {
        TextMeshProUGUI lifePoint = showNumber[0].GetComponent<TextMeshProUGUI>();
        lifePoint.text = spawnRefUI[0].lifePoint.ToString();
        if (spawnRefUI[0].lifePoint == 0)
        {
            Invoke("Destroy", 0.5f);
            isDying = true;
        }

        if (isDying)
        {
            returnTime = true;
        }
    }

    public void LaunchRespawn()
    {
        counterTimer += Time.deltaTime;
        if (counterTimer > counterBeforeRespawn)
        {
            isDying = false;
            counterTimer = 0;
            Invoke("Respawn", 0.5f);
            Debug.Log("Hello");
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

    public void CounterTimer()
    {
        TimeManager.SlowDownGame();
        timerCounterSlow += Time.deltaTime;
        if (timerCounterSlow >= timerLimit)
        {
            returnTime = false;
            timerCounterSlow = 0;
        }
    }
}