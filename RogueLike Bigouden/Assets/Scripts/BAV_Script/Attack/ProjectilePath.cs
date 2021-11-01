using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SplineWalkerMode
{
    Once,
    Loop,
    PingPong
}

public class ProjectilePath : MonoBehaviour
{
    public AttackSystemSpline spline;
    public float duration;
    private float progress;
    public float speed = 4f;
    public bool lookForward;
    public SplineWalkerMode mode;
    private bool goingForward = true;

    private void Update()
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

        if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        {
            Vector3 position = spline.GetPoint(progress, spline.arrayVector[0].pointAttack);
            transform.localPosition = position;
            if (lookForward)
            {
                transform.LookAt(position + spline.GetDirection(progress,spline.arrayVector[0].pointAttack));
            }
        }

        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0))
        {
            Vector3 position = spline.GetPoint(progress, spline.arrayVector[1].pointAttack);
            transform.localPosition = position;
            if (lookForward)
            {
                transform.LookAt(position + spline.GetDirection(progress, spline.arrayVector[1].pointAttack));
            }
        }
    }
}