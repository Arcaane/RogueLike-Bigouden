using System;
using System.Collections;
using System.Collections.Generic;
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

    //private
    private int lifePointObject;
    

    // Update is called once per frame
    void Update()
    {
        if (isDying)
        {
            LaunchRespawn(spawnRefUI[0].gameObject, showNumber[0].gameObject);
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
            showNumber[0].gameObject.SetActive(false);
            spawnRefUI[0].gameObject.SetActive(false);
            isDying = true;
        }
    }

    public void LaunchRespawn(GameObject obj, GameObject textUI)
    {
        counterTimer += Time.deltaTime;
        if (counterTimer > counterBeforeRespawn)
        {
            isDying = false;
            counterTimer = 0;
            Debug.Log("Hello");
            textUI.SetActive(true);
            obj.SetActive(true);
            spawnRefUI[0].lifePoint = spawnRefUI[0].ennemyData.lifePointSO;
        }
    }
}