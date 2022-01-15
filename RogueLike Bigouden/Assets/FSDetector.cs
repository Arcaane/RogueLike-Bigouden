using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSDetector : MonoBehaviour
{
    public FlameStrike fsScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other is BoxCollider2D && other.transform.GetComponent<PlayerStatsManager>())
        {
            fsScript.playerOnIt = false;
            fsScript.player = null;
        }
    }
}
