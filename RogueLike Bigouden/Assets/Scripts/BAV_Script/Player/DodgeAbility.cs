using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DodgeAbility : MonoBehaviour
{
    [SerializeField] private float radiusBeforeDash;
    [SerializeField] List<Transform> target;
    private Vector3 posPlayer;

    public void Awake()
    {
        //target.Clear();
        posPlayer = transform.position;
    }

    public void Update()
    {
        DodgeAbility_T();
    }

    public void DodgeAbility_T()
    {
        foreach (Transform obj in target)
        {
            Vector3 objPos = obj.position;
            float range = Vector2.Distance(posPlayer, objPos);
            if (range <= 0)
            {
                range *= -1;
            }
            
            if (range < radiusBeforeDash)
            {
                //TimeManager._timeManager.SlowDownGame(3);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(posPlayer, radiusBeforeDash);
    }
}