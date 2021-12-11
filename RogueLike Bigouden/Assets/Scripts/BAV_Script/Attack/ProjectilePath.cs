using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ProjectilePath : MonoBehaviour
{
    public AttackSystemSpline spline;
    public GameObject projectile;
    private float duration;
    public float progress;
    public bool isAttacking;

    [Header("Attack Speed du joueur"), Range(0.1f, 8f)]
    public float speed = 4f;

    private bool lookForward;
    public int countAttack = 0;
    public bool launchFirstAttack;
    public bool launchSecondAttack;
    public bool launchAttackY;

    private void Start()
    {
        isAttacking = false;
        projectile.SetActive(false);
    }

    private void Update()
    {
        if (progress < 1 && progress > 0)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }

    private void FixedUpdate()
    {
        OnMovement(spline.arrayVector[0].pointAttack);
    }

    /// <summary>///
    /// Fonction permettant au projectile de ce déplacer sur la curve.
    /// /// </summary>
    public void Path()
    {
        if (launchFirstAttack
            && !launchSecondAttack
            && !launchAttackY)
        {
            projectile.SetActive(true);
            progress += (TimeManager._timeManager.CustomDeltaTimeProjectile / (speed / 10));
            if (progress > 1f)
            {
                progress = 1f;
                launchFirstAttack = false;
                projectile.SetActive(false);
            }
        }

        if (launchSecondAttack
            && !launchFirstAttack
            && !launchAttackY)
        {
            projectile.SetActive(true);
            progress -= (TimeManager._timeManager.CustomDeltaTimeProjectile / (speed / 10));
            if (progress < 0f)
            {
                progress = 0f;
                projectile.SetActive(false);
                launchFirstAttack = false;
                launchSecondAttack = false;
            }
        }

        if (launchAttackY
            && !launchFirstAttack
            && !launchSecondAttack)
        {
            projectile.SetActive(true);
        }
        else
        {
            projectile.SetActive(false);
        }
    }


    public void OnMovement(Vector3[] pointAttack)
    {
        Vector3 position = spline.GetPoint(progress, pointAttack);
        projectile.transform.position = position;
        if (lookForward)
        {
            transform.LookAt(spline.GetDirection(progress, pointAttack));
        }
    }

    public void Reset()
    {
        spline = GetComponent<AttackSystemSpline>();
        duration = 1f;
        speed = 1f;
    }
}