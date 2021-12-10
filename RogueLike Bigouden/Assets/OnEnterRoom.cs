using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterRoom : MonoBehaviour
{
    
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        LoadManager.LoadManagerInstance.launchAnimator.SetBool("Enter", false);
        GameObject.Find("Player 0").transform.position = spawnPoint.transform.position;
    }
}
