using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeurreDoux : MonoBehaviour
{
    private TrailRenderer m_trail;
    private GameObject player;
    
    private void Start()
    {
        m_trail = GetComponent<TrailRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(player)
            gameObject.transform.position = player.transform.position;

        if (!player)
            player = GameObject.FindGameObjectWithTag("Player");
        
        
    }
}
