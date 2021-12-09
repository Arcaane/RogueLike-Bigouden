using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeurreDoux : MonoBehaviour
{
    private TrailRenderer m_trail;
    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;
    private GameObject player;
    
    private void Start()
    {
        m_trail = GetComponent<TrailRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(player)
            gameObject.transform.position = player.transform.position;
        
        m_trail.BakeMesh(_meshFilter.mesh, Camera.main, true);
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }

    
}
