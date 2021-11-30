using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtility
{
    public static void SetPosX(this Transform t, float posX)
    {
        t.position = new Vector3(posX, t.position.y, 0f);
    }

    public static void SetEulerZ(this Transform t, float rotZ)
    {
        t.rotation = Quaternion.Euler(0f, 0f ,rotZ);
    }
}