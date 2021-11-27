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

    private bool isDetect;
    private float current;
    private float _target = 10;
    public float duration = 0.5f;

    private void Start()
    {
        _projectilePath = _projectilePathHolder.GetComponent<ProjectilePath>();
    }

    public void Update()
    {
        current = Mathf.MoveTowards(current, _target, duration * TimeManager.CustomDeltaTimeAttack);
    }


    private void OnTriggerEnter2D(Collider2D trigger2D)
    {
        current = 1;
        
        if (_projectilePath.isAttacking && trigger2D.gameObject.CompareTag("Ennemy"))
        {

            GameObject objTrigger = trigger2D.gameObject;
            Vector3 objTriggerVect = objTrigger.transform.position;
            if (trigger2D.gameObject.name == "Turret")
            {
                objTrigger.GetComponent<MannequinStatsManager>().TakeDamage(1);
                PlayerFeedBack.instance.CameraMovement(_projectilePathHolder.transform.position, objTriggerVect, current);
                Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<MannequinStatsManager>().lifePoint);
            }
            else
            {
                objTrigger.GetComponent<EnnemyStatsManager>().TakeDamage(1);
                PlayerFeedBack.instance.CameraMovement(_projectilePathHolder.transform.position, objTriggerVect, current);
                Debug.Log("Ennemy damaged : " + trigger2D.gameObject.GetComponent<EnnemyStatsManager>().lifePoint);
            }

            Debug.Log("Ennemy damaged : " + trigger2D.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D trigger2D)
    {
        current = 0;
        PlayerFeedBack.instance.ResetPosCam(current);
    }
}