using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PropsGeneratorData))]
public class PropsGeneratorData_Editor : Editor
{
    //Use the Animation property 
    private bool useAnimationForProps;

    //private----------
    private Rect rect;
    private Color colorHit;
    private Color colorReset;
    private SerializedProperty propsListAnim_1;
    private SerializedProperty propsListAnim_2;

    public override void OnInspectorGUI()
    {
        propsListAnim_1 = serializedObject.FindProperty("spriteAnimatoSO");
        propsListAnim_2 = serializedObject.FindProperty("spriteAnimatoSecondaySO");

        GUILayout.Label("Basic Prop", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("spriteSwapSO"), true);

        rect.DrawUILine(Color.black);
        
        GUILayout.Label("Props With Animation", EditorStyles.boldLabel);
        useAnimationForProps = EditorGUILayout.Toggle("Animation with the Props ?", useAnimationForProps);
        using (new EditorGUI.DisabledScope(!useAnimationForProps))
        {
            EditorGUILayout.PropertyField(propsListAnim_1, true);
            EditorGUILayout.PropertyField(propsListAnim_2, true);
        }

        rect.DrawUILine(Color.black);

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }
}