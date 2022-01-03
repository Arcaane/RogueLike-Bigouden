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
    private Camera cam;
    
    private void Start()
    {
        m_trail = GetComponent<TrailRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        m_trail.BakeMesh(_meshFilter.mesh, cam, true);
        _meshCollider.sharedMesh = _meshFilter.mesh;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("JE SUIS SUR DU BEURRE");
    }
}
