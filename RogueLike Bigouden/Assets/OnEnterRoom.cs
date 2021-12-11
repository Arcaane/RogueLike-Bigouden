using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterRoom : MonoBehaviour
{
    public GameObject spawnPoint;
    private void Awake()
    {
        LoadManager.LoadManagerInstance.launchAnimator.SetBool("Enter", false);
        GameObject.Find("Player 0").transform.position = spawnPoint.transform.position;
    }
}
