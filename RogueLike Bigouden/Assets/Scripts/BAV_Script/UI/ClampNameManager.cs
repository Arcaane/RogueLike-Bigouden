using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    //private
    private int lifePointObject;

    public void Start()
    {
        lifePointObject = spawnRefUI[0].lifePoint;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 namePos = camera.WorldToScreenPoint(spawnRefUI[0].gameObject.transform.position + offsetPosition);
        showNumber[0].transform.position = namePos;
        TakeDamageUI();
        if (isDying)
        {
            LaunchRespawn(spawnRefUI[0].gameObject);
        }
    }

    void TakeDamageUI()
    {
        TextMeshProUGUI lifePoint = showNumber[0].GetComponent<TextMeshProUGUI>();
        lifePoint.text = spawnRefUI[0].lifePoint.ToString();

        if (spawnRefUI[0].lifePoint == 0)
        {
            showNumber[0].gameObject.SetActive(false);
            spawnRefUI[0].gameObject.SetActive(false);
            isDying = true;
        }
    }

    public void LaunchRespawn(GameObject gameObject)
    {
        counterTimer += Time.deltaTime;
        if (counterTimer >= counterBeforeRespawn)
        {
            counterTimer = 0;
            isDying = false;
            spawnRefUI[0].lifePoint = lifePointObject;
            showNumber[0].gameObject.SetActive(true);
            gameObject.SetActive(true);
        }
    }
}