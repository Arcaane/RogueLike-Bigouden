using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using Vector3 = UnityEngine.Vector3;


[Serializable]
public class ArrayVector
{
    public Color color = new Color(1, 1, 1, 1);
    public Vector3[] pointAttack;
}

public class AttackSystemSpline : MonoBehaviour
{
    public ArrayVector[] arrayVector;

    public float radiusAttack;

    public bool addPoint = false;


    public int CurveCount
    {
        get { return ((arrayVector[0].pointAttack.Length - 1) / 3); }
    }

    public Vector3 GetPoint(float t, Vector3[] pointsAttack)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = pointsAttack.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int) t;
            t -= i;
            i *= 3;
        }

        return transform.TransformPoint(Bezier.GetPoint(
            new Vector3(pointsAttack[i].x, pointsAttack[i].y, 0f) * (radiusAttack),
            new Vector3(pointsAttack[i + 1].x, pointsAttack[i + 1].y, 0f) * (radiusAttack),
            new Vector3(pointsAttack[i + 2].x, pointsAttack[i + 2].y, 0f) * (radiusAttack),
            new Vector3(pointsAttack[i + 3].x, pointsAttack[i + 3].y, 0f) * (radiusAttack),
            t));
    }

    public Vector3 GetVelocity(float t, Vector3[] pointsAttack)
    {
        int i;
        if (t >= 1f)
        {
            t = 1f;
            i = pointsAttack.Length - 4;
        }
        else
        {
            t = Mathf.Clamp01(t) * CurveCount;
            i = (int) t;
            t -= i;
            i *= 3;
        }

        return transform.TransformPoint(Bezier.GetFirstDerivative(
            new Vector3(pointsAttack[i].x, pointsAttack[i].y, 0f) * (radiusAttack),
            new Vector3(pointsAttack[i + 1].x, pointsAttack[i + 1].y, 0f) * (radiusAttack),
            new Vector3(pointsAttack[i + 2].x, pointsAttack[i + 2].y, 0f) * (radiusAttack),
            new Vector3(pointsAttack[i + 3].x, pointsAttack[i + 3].y, 0f) * (radiusAttack),
            t));
    }

    public Vector3 GetDirection(float t, Vector3[] pointsAttack)
    {
        return GetVelocity(t, pointsAttack).normalized * -radiusAttack;
    }

    public void Reset()
    {
        radiusAttack = 1f;
        int numberArray = 1;
        arrayVector = new ArrayVector[numberArray];
        for (int i = 0; i < arrayVector.Length; i++)
        {
            arrayVector[i] = new ArrayVector();

            switch (i)
            {
                case 0:
                    arrayVector[0].pointAttack = new[]
                    {
                        new Vector3(-1f, 0f, 0f) * radiusAttack,
                        new Vector3(-1f, 0f, 0f) * radiusAttack,
                        new Vector3(-1f, 1f, 0f) * radiusAttack,
                        new Vector3(0f, 1f, 0f) * radiusAttack,
                        new Vector3(1f, 1f, 0f) * radiusAttack,
                        new Vector3(1f, 0f, 0f) * radiusAttack,
                        new Vector3(1f, 0f, 0f) * radiusAttack
                    };
                    arrayVector[0].color = Color.green;
                    break;
            }

            /*
            switch (i)
            {
                case 0:
                case 1:
                    arrayVector[1].pointAttack = new[]
                    {
                        new Vector3(1f, 0f, 0f) * radiusAttack,
                        new Vector3(1f, 0f, 0f) * radiusAttack,
                        new Vector3(1f, 1f, 0f) * radiusAttack,
                        new Vector3(0f, 1f, 0f) * radiusAttack
                    };
                    arrayVector[1].color = Color.blue;
                    break;
                case 2:
                    arrayVector[2].pointAttack = new[]
                    {
                        new Vector3(0f, 1f, 0f) * radiusAttack,
                        new Vector3(0f, 1.25f, 0f) * radiusAttack,
                        new Vector3(0f, 1.5f, 0f) * radiusAttack,
                        new Vector3(0f, 2f, 0f) * radiusAttack
                    };
                    arrayVector[2].color = Color.red;
                    break;
            }
            */
        }
    }

    public void AddCurve(float value)
    {
        switch (value)
        {
            case 0:
                Vector3 point1 = arrayVector[0].pointAttack[arrayVector[0].pointAttack.Length - 1];
                Array.Resize(ref arrayVector[0].pointAttack, arrayVector[0].pointAttack.Length + 3);
                point1.x += 1f;
                arrayVector[0].pointAttack[arrayVector[0].pointAttack.Length - 3] = point1;
                point1.x += 1f;
                arrayVector[0].pointAttack[arrayVector[0].pointAttack.Length - 2] = point1;
                point1.x += 1f;
                arrayVector[0].pointAttack[arrayVector[0].pointAttack.Length - 1] = point1;
                break;
            case 1:
                switch (arrayVector[1].pointAttack != null)
                {
                    case true:
                        Vector3 point2 = arrayVector[1].pointAttack[arrayVector[1].pointAttack.Length - 1];
                        Array.Resize(ref arrayVector[1].pointAttack, arrayVector[1].pointAttack.Length + 3);
                        point2.x += 1f;
                        arrayVector[1].pointAttack[arrayVector[1].pointAttack.Length - 3] = point2;
                        point2.x += 1f;
                        arrayVector[1].pointAttack[arrayVector[1].pointAttack.Length - 2] = point2;
                        point2.x += 1f;
                        arrayVector[1].pointAttack[arrayVector[1].pointAttack.Length - 1] = point2;
                        break;
                    case false:
                        return;
                }

                break;
            case 2:
                switch (arrayVector[2].pointAttack != null)
                {
                    case true:
                        Vector3 point3 = arrayVector[2].pointAttack[arrayVector[2].pointAttack.Length - 1];
                        Array.Resize(ref arrayVector[2].pointAttack, arrayVector[2].pointAttack.Length + 3);
                        point3.x += 1f;
                        arrayVector[2].pointAttack[arrayVector[2].pointAttack.Length - 3] = point3;
                        point3.x += 1f;
                        arrayVector[2].pointAttack[arrayVector[2].pointAttack.Length - 2] = point3;
                        point3.x += 1f;
                        arrayVector[2].pointAttack[arrayVector[2].pointAttack.Length - 1] = point3;
                        break;
                    case false:
                        return;
                }

                break;
        }
    }
}

public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }
}