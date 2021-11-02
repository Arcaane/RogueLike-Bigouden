/*using UnityEditor;
using UnityEngine;
using UnityEditor.AnimatedValues;


[CustomEditor(typeof(PlayerData))]
public class EditorForScriptable : Editor
{
    private PlayerData playerData;
    private AnimBool showBoolParameter;

    public bool readyToAttackX; // Peut utiliser l'attaque X
    public bool readyToAttackY; // Peut utiliser l'attaque Y
    public bool readyToAttackB; // Peut utiliser l'attaque projectile
    public bool isDashing;
    public bool readyToDash;
    public bool onButter;

    // Start is called before the first frame update
    void OnEnable()
    {
        showBoolParameter = new AnimBool(false);
        showBoolParameter.valueChanged.AddListener(Repaint);
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Player Data", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        showBoolParameter.target = EditorGUILayout.ToggleLeft("Customize Values", showBoolParameter.target);

        switch (EditorGUILayout.BeginFadeGroup(showBoolParameter.faded))
        {
            case true:
                EditorGUI.indentLevel++;
                readyToAttackX = EditorGUILayout.Toggle("readyToAttackX ?", readyToAttackX);
                readyToAttackY = EditorGUILayout.Toggle("readyToAttackY ?", readyToAttackY);
                readyToAttackB = EditorGUILayout.Toggle("readyToAttackB ?", readyToAttackB);
                isDashing = EditorGUILayout.Toggle("isDashing ?", isDashing);
                readyToDash = EditorGUILayout.Toggle("readyToDash ?", readyToDash);
                onButter = EditorGUILayout.Toggle("onButter ?", onButter);
                break;

            case false:
                readyToAttackX = false;
                readyToAttackY = false;
                readyToAttackB = false;
                isDashing = false;
                readyToDash = false;
                onButter = false;
                break;
        }

        EditorGUILayout.EndFadeGroup();
    }
}*/