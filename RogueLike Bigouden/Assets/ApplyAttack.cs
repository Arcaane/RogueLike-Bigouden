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

        PillarsStatsManager objPillar = objTrigger.GetComponent<PillarsStatsManager>();

        //Mannequin and Ennemy
        if (_projectilePath.isAttacking && trigger2D.gameObject.CompareTag("Ennemy"))
        {
            //Launch First Attack
            if (_projectilePath.launchFirstAttack)
            {
                isDetect++;
                posTarget = objTrigger.transform.position;
                if (objTrigger.name == "Turret")
                {
                    TimeManager._timeManager.SlowDownGame(1);
                    objMannequin.TakeDamage(PlayerStatsManager.playerStatsInstance.damageFirstX);
                    //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<MannequinStatsManager>().lifePoint);
                }
                else
                {
                    objEnnemy.TakeDamage(PlayerStatsManager.playerStatsInstance.damageFirstX);

                    //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<EnnemyStatsManager>().lifePoint);
                }
            }

            //Launch Second Attack
            if (_projectilePath.launchSecondAttack)
            {
                isDetect++;
                posTarget = objTrigger.transform.position;
                if (objTrigger.name == "Turret")
                {
                    TimeManager._timeManager.SlowDownGame(1);
                    objMannequin.TakeDamage(PlayerStatsManager.playerStatsInstance.damageSecondX);
                    //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<MannequinStatsManager>().lifePoint);
                }
                else
                {
                    objEnnemy.TakeDamage(PlayerStatsManager.playerStatsInstance.damageSecondX);
                    //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<EnnemyStatsManager>().lifePoint);
                }
            }

            //Launch Attack Y
            if (_projectilePath.launchAttackY)
            {
                isDetect++;
                posTarget = objTrigger.transform.position;
                if (objTrigger.name == "Turret")
                {
                    TimeManager._timeManager.SlowDownGame(1);
                    objMannequin.TakeDamage(PlayerStatsManager.playerStatsInstance.damageY);
                    //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<MannequinStatsManager>().lifePoint);
                }
                else
                {
                    objEnnemy.TakeDamage(PlayerStatsManager.playerStatsInstance.damageY);
                    //Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<EnnemyStatsManager>().lifePoint);
                }
            }

            Debug.Log("Ennemy damaged : " + trigger2D.gameObject.name);

            FindObjectOfType<SoundManager>().PlaySound("P_Hit2");
        }

        ////Props Environnement
        if (objTrigger.CompareTag("Beam") && PlayerStatsManager.playerStatsInstance.isAttackingX)
        {
            objProps.TakeDamagePilarDestruction(1);
        }

        /*
        if (objTrigger.CompareTag("Pillier") && PlayerStatsManager.playerStatsInstance.isAttackingX)
        {
            if (_projectilePath.launchFirstAttack)
            {
                objPillar.TakeDamage(PlayerStatsManager.playerStatsInstance.damageFirstX);
            }

            if (_projectilePath.launchSecondAttack)
            {
                objPillar.TakeDamage(PlayerStatsManager.playerStatsInstance.damageSecondX);
            }

            if (_projectilePath.launchAttackY)
            {
                objPillar.TakeDamage(PlayerStatsManager.playerStatsInstance.damageY);
            }
        }
        */

        if (_projectilePath.isAttacking && trigger2D.gameObject.CompareTag("Destructible"))
        {
            Destroy(trigger2D.gameObject);
            NavMeshUpdater.instance.UpdateSurface();
        }
    }
    
    private void OnTriggerExit2D(Collider2D trigger2D)
    {
        _target = 0;
        isDetect--;
    }
}