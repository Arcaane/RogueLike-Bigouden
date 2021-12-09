using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChoseSpriteDisplay))]
public class ChooseSpriteDisplayEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChoseSpriteDisplay choseSprite = (ChoseSpriteDisplay) target;
        if (GUILayout.Button("Resample Asset on Props"))
        {
            choseSprite.ChangeSprite();
        }
    }
}
