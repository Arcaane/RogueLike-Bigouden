using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAttack : MonoBehaviour
{
    public GameObject _projectilePathHolder;
    private ProjectilePath _projectilePath;

    private void Start()
    {
        _projectilePath = _projectilePathHolder.GetComponent<ProjectilePath>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_projectilePath.isAttacking && other.gameObject.CompareTag("Ennemy"))
        {
            other.gameObject.GetComponent<EnnemyStatsManager>().TakeDamage(1);
            Debug.Log("Ennemy damaged : " + other.gameObject.name);
        }
    }
}
