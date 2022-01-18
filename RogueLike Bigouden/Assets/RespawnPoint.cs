using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = FindObjectOfType<PlayerStatsManager>().gameObject;

        player.transform.position = gameObject.transform.position;
    }
}
