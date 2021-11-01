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
    public float duration;
    public float progress;
    public float speed = 4f;
    public bool lookForward;
    public SplineWalkerMode mode;
    public bool goingForward = true;


    private void Update()
    {
        Path();
        OnMovement(spline.arrayVector[0].pointAttack);
    }

    public void Path()
    {
        if (goingForward)
        {
            progress += (Time.deltaTime / duration) * speed;
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                    goingForward = false;
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
            progress = 0;
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