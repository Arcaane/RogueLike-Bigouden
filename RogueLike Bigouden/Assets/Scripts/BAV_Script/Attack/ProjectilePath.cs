using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SplineWalkerMode
{
    Once,
    Loop,
    PingPong,
    FirstAttack,
    SecondAttack
}

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
    public SplineWalkerMode mode;
    public bool launchAttack;
    public bool launchSecondAttack;

    private void Start()
    {
        isAttacking = false;
    }

    private void Update()
    {
        if ( progress < 1 && progress > 0)
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
        if (launchAttack)
        {
            projectile.SetActive(true);
            progress += (TimeManager.CustomDeltaTimeAttack/ (speed / 10));
            if (progress > 1f)
            {
                switch (mode)
                {
                    case SplineWalkerMode.Once:
                        progress = 1f;
                        launchAttack = false;
                        break;
                    case SplineWalkerMode.Loop:
                        progress = 0f;
                        launchAttack = false;
                        break;
                    case SplineWalkerMode.PingPong:
                        progress = 2f - progress;
                        launchAttack = false;
                        break;
                    case SplineWalkerMode.FirstAttack:

                        progress = 1f;
                        projectile.SetActive(false);
                        launchAttack = false;
                        break;
                }
            }
        }
        else if (launchSecondAttack)
        {
            projectile.SetActive(true);
            progress -= (TimeManager.CustomDeltaTimeAttack / (speed / 10));
            if (progress < 0f)
            {
                progress = 0f;
                projectile.SetActive(false);
                launchSecondAttack = false;
                launchAttack = false;
            }
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