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

    private void OnTriggerEnter2D(Collider2D trigger2D)
    {
        if (_projectilePath.isAttacking && trigger2D.gameObject.CompareTag("Ennemy"))
        {
            GameObject o = trigger2D.gameObject;
            o.GetComponent<EnnemyStatsManager>().TakeDamage(1);
            Debug.Log("Ennemy damaged : " + o.name);
        }
    }
}
