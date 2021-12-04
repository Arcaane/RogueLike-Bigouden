using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using System.Timers;
using UnityEngine;

public class ApplyAttack : MonoBehaviour
{
    public GameObject _projectilePathHolder;
    private ProjectilePath _projectilePath;
    private Transform pivotCam;

    [SerializeField] float current;
    [SerializeField] private float _target;
    public float duration = 0.5f;
    [SerializeField] public int isDetect;
    public Vector3 posTarget;

    private void Start()
    {
        _projectilePath = _projectilePathHolder.GetComponent<ProjectilePath>();
    }


    private void OnTriggerEnter2D(Collider2D trigger2D)
    {
        GameObject objTrigger = trigger2D.gameObject;
        if (_projectilePath.isAttacking && trigger2D.gameObject.CompareTag("Ennemy"))
        {
            isDetect++;
            posTarget = objTrigger.transform.position;
            if (objTrigger.name == "Turret")
            {
                TimeManager.SlowDownGame(1);
                objTrigger.GetComponent<MannequinStatsManager>().TakeDamage(1);
                //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<MannequinStatsManager>().lifePoint);
            }
            else
            {
                objTrigger.GetComponent<EnnemyStatsManager>().TakeDamage(1);
                //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<EnnemyStatsManager>().lifePoint);
            }

            Debug.Log("Ennemy damaged : " + trigger2D.gameObject.name);
        }

        if (objTrigger.CompareTag("Beam"))
        {
            //objTrigger.GetComponent<Props_EnvironnementManager>().TakeDamage(1);
            Debug.Log("Ennemy Damaged" + objTrigger.name);
        }
    }

    private void OnTriggerExit2D(Collider2D trigger2D)
    {
        _target = 0;
        isDetect--;
    }
}