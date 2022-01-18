using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Instrumentation;
using System.Timers;
using UnityEngine;
using Random = System.Random;

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

    public bool mediKit;
    public int mediKitHeal;
    
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

        BossStatsManager objBoss = objTrigger.GetComponent<BossStatsManager>();
        
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
                    if (objTrigger.GetComponent<IACac>())
                    {
                        objEnnemy.GetComponent<IACac>().StartCoroutine(objEnnemy.GetComponent<IACac>().ResetStun());
                    }
                    else if (objTrigger.GetComponent<IARunner>())
                    {
                        objEnnemy.GetComponent<IARunner>().StartCoroutine(objEnnemy.GetComponent<IARunner>().ResetStun());
                    }
                    else if (objTrigger.GetComponent<IAShooter>())
                    {
                        objEnnemy.GetComponent<IAShooter>().StartCoroutine(objEnnemy.GetComponent<IAShooter>().ResetStun());
                    }
                    else if (objTrigger.GetComponent<IABarman>())
                    {
                        objEnnemy.GetComponent<IABarman>().StartCoroutine(objEnnemy.GetComponent<IABarman>().ResetStun());
                    }
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
        

        if (_projectilePath.isAttacking && trigger2D.gameObject.CompareTag("Destructible"))
        {
            Destroy(trigger2D.gameObject);
            NavMeshUpdater.instance.UpdateSurface();

            if (mediKit)
            {
                int roll = UnityEngine.Random.Range(0, 100);

                if (roll < 30)
                {
                    Debug.Log("Heal by medic with " + roll + " roll");
                    PlayerStatsManager.playerStatsInstance.lifePoint += mediKitHeal;
                    UIManager.instance.RefreshUI();
                }
            }
        }

        if (_projectilePath.isAttacking && trigger2D.gameObject.CompareTag("Projectile"))
        {
            trigger2D.gameObject.GetComponent<Rigidbody2D>().velocity =
                - trigger2D.gameObject.GetComponent<Rigidbody2D>().velocity;

            trigger2D.GetComponent<BulletBehaviour>().isReverse = true;
        }
        
        //Boss
        if (objTrigger.CompareTag("Pillier") && _projectilePath.isAttacking)
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

        if (objTrigger.CompareTag("Boss") && _projectilePath.isAttacking)
        {
            if (_projectilePath.launchFirstAttack)
            {
                objBoss.TakeDamage(PlayerStatsManager.playerStatsInstance.damageFirstX);
            }

            if (_projectilePath.launchSecondAttack)
            {
                objBoss.TakeDamage(PlayerStatsManager.playerStatsInstance.damageSecondX);
            }

            if (_projectilePath.launchAttackY)
            {
                objBoss.TakeDamage(PlayerStatsManager.playerStatsInstance.damageY);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D trigger2D)
    {
        _target = 0;
        isDetect--;
    }
}