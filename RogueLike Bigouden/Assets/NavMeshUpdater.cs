using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdater : MonoBehaviour
{
    public static NavMeshUpdater instance;
    
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject); 

        instance = this;
    }
    
    public void UpdateSurface()
    {
        NavMeshSurface2d[] allChildren = GetComponentsInChildren<NavMeshSurface2d>();

        for (int i = 0; i < allChildren.Length; i++)
            allChildren[i].BuildNavMesh();
    }
}
