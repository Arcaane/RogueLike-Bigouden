using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterRoom : MonoBehaviour
{
    
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Player 0").transform.position = spawnPoint.transform.position;
        GameObject.Find("Player 1").transform.position = spawnPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
