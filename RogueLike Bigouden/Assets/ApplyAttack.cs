using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
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
            if (trigger2D.gameObject.name == "Turret")
            {
                trigger2D.gameObject.GetComponent<MannequinStatsManager>().TakeDamage(1);
                Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<MannequinStatsManager>().lifePoint);
            }
            else
            {
                trigger2D.gameObject.GetComponent<EnnemyStatsManager>().TakeDamage(1);
                Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<EnnemyStatsManager>().lifePoint);
            }
            Debug.Log("Ennemy damaged : " + trigger2D.gameObject.name);
        }
    }
}