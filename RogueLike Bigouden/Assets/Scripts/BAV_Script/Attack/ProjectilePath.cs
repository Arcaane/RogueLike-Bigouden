using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SplineWalkerMode
{
    Once,
    Loop,
    PingPong
}

public class ProjectilePath : MonoBehaviour
{
    public AttackSystemSpline spline;
    public GameObject projectile;
    private float duration;
    public float progress;

    [Header("Attack Speed du joueur"), Range(0.1f, 8f)]
    public float speed = 4f;

    private bool lookForward;
    public SplineWalkerMode mode;
    public int goingForward = 0;


    private void Update()
    {
        Path();
        OnMovement(spline.arrayVector[0].pointAttack);
    }


    /// <summary>///
    /// Fonction permettant au projectile de ce déplacer sur la curve.
    /// /// </summary>
    public void Path()
    {
        if (goingForward == 1)
        {
            projectile.SetActive(true);
            progress += (Time.deltaTime / (speed / 10));
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                    goingForward = 2;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                    goingForward = 0;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = 0;
                }
            }
        }
        else
        {
            if (goingForward == 0)
            {
                progress -= (Time.deltaTime / (speed / 10));
                if (progress < 0f)
                {
                    progress = -progress;
                    goingForward = 1;
                }
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
/*public void Path()
{
    if (goingForward)
    {
        progress += (Time.deltaTime / duration) * speed;
        if (progress > 1f)
        {
            if (mode == SplineWalkerMode.Once)
            {
                progress = 1f;
            }
            else if (mode == SplineWalkerMode.Loop)
            {
                progress -= 1f;
            }
            else
            {
                progress = 2f - progress;
                goingForward = false;
            }
        }
    }
    else
    {
        progress -= (Time.deltaTime / duration) * speed;
        if (progress < 0f)
        {
            progress = -progress;
            goingForward = true;
        }
    }
}*/