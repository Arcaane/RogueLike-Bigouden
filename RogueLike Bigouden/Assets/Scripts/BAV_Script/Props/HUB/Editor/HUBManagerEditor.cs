using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HUB_Manager))]
public class HUBManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HUB_Manager changeSprite = (HUB_Manager) target;
        if (GUILayout.Button("Resample Asset on Props"))
        {
            //changeSprite.TchekDoublon();
        }
    }
}
