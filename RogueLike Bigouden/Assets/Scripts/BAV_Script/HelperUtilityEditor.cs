using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class HelperUtilityEditor
{
    /*
    public static void SetPosX(this Transform t, float posX)
    {
        t.position = new Vector3(posX, t.position.y, 0f);
    }

    public static void SetEulerZ(this Transform t, float rotZ)
    {
        t.rotation = Quaternion.Euler(0f, 0f ,rotZ);
    }
    */
    
    public static void DrawUILine(this Rect r ,Color color, int thickness = 1, int padding = 10)
    {
        r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
        r.height = thickness;
        r.y += padding / 2;
        r.x -= 10;
        r.width += 6;
        EditorGUI.DrawRect(r, color);
    }

}