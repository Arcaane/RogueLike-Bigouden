using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterRoom : MonoBehaviour
{
    private GameObject Player;
    public GameObject spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var p in PlayerInstance.playerInstance.Player)
        {
            p.transform.position = new Vector2(spawnPoint.transform.position.x, spawnPoint.transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
