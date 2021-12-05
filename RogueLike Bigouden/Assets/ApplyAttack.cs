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
        //Mannequin
        MannequinStatsManager objMannequin = objTrigger.GetComponent<MannequinStatsManager>();
        //Ennemy
        EnnemyStatsManager objEnnemy = objTrigger.GetComponent<EnnemyStatsManager>();
        //Props
        Props_EnvironnementManager objProps = objTrigger.GetComponent<Props_EnvironnementManager>();
        
        //Mannequin and Ennemy
        if (_projectilePath.isAttacking && trigger2D.gameObject.CompareTag("Ennemy"))
        {
            isDetect++;
            posTarget = objTrigger.transform.position;
            if (objTrigger.name == "Turret")
            {
                TimeManager._timeManager.SlowDownGame(1);
                objMannequin.TakeDamage(1);
                //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<MannequinStatsManager>().lifePoint);
            }
            else
            {
                objEnnemy.TakeDamage(1);
                //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<EnnemyStatsManager>().lifePoint);
            }

            Debug.Log("Ennemy damaged : " + trigger2D.gameObject.name);
        }

        ////Props Environnement
        if (objTrigger.CompareTag("Beam"))
        {
            objProps.TakeDamagePropsDestruction(1);
        }
    }

    private void OnTriggerExit2D(Collider2D trigger2D)
    {
        _target = 0;
        isDetect--;
    }
}