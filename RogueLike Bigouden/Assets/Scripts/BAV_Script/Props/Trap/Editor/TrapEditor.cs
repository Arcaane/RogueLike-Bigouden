using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ElectrickTrap))]
public class TrapEditor : Editor
{
    int toolbarInt = 0;
    string[] toolbarStrings = {"Properties", "Controller"};

    //Collider Section
    Transform originTrap;
    Vector2 originTrapVect;

    //Editor Button Size--------------
    Vector2 sizeBoxW_H = new Vector2(10f, 80f);

    //BooleanForType
    bool useCustomOrigin;
    bool useSmoothMovement;
    bool useBasicOrigin;

    //Float----------
    const float gridSnap = 0.53333f;
    const float gridSnapSmooth = 0.53333f;

    //private----------
    private Rect rect;
    public override void OnInspectorGUI()
    {
        var trap = target as ElectrickTrap;
        Undo.RecordObject(trap, "RecordTst");

        //SHOW THE TOOLBAR----------
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);

        switch (toolbarInt)
        {
            case 0:
                base.OnInspectorGUI();

                if (GUILayout.Button("Clear the List"))
                {
                    ClearList();
                }

                break;

            case 1:

                ////--------------------BUTTON POSITION FIRST LINE----------------------////
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    GameObject objTrap = Instantiate(trap.basicTrap, trap.trapFolder.transform, true);
                    trap.electrikPrefab.Add(objTrap);
                }

                if (GUILayout.Button("Top\nPosition", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    trap.InstantiatePrefab(0);
                }

                GUILayout.Button("", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y));
                GUILayout.EndHorizontal();

                #region SECOND LINE BUTTON

                ////--------------------BUTTON POSITION SECOND LINE----------------------////

                GUILayout.BeginHorizontal();

                //--------------------LEFT BUTTON----------------------//
                if (GUILayout.Button("Left\nPosition", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    trap.InstantiatePrefab(1);
                }

                //--------------------ORIGIN BUTTON----------------------//
                if (GUILayout.Button("Origin\nPosition", GUILayout.ExpandWidth(false),
                    GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    TchekObj();
                }

                //--------------------RIGHT BUTTON----------------------//
                if (GUILayout.Button("Right\nPosition", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    trap.InstantiatePrefab(2);
                }

                GUILayout.EndHorizontal();

                #endregion

                #region THIRD LINE BUTTON

                ////--------------------BUTTON POSITION THIRD LINE----------------------////

                GUILayout.BeginHorizontal();

                GUILayout.Button("", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y));
                if (GUILayout.Button("Down\nPosition", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    trap.InstantiatePrefab(3);
                }

                GUILayout.Button("", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y));

                GUILayout.EndHorizontal();

                #endregion

                ////--------------------BUTTON POSITION SECOND LINE----------------------////
                rect.DrawUILine(Color.black);

                //--------------------USE BASIC ORIGIN----------------------//
                using (new EditorGUI.DisabledScope(useCustomOrigin))
                {
                    GUILayout.Label("Use Basic Origin", EditorStyles.boldLabel);
                    useBasicOrigin = EditorGUILayout.Toggle("Use Basic Origin", useBasicOrigin);
                }

                rect.DrawUILine(Color.black);

                //--------------------USE CUSTOM ORIGIN----------------------//
                GUILayout.BeginHorizontal();
                GUILayout.Label("Custom Origin Point", EditorStyles.boldLabel);
                GUILayout.EndHorizontal();

                using (new EditorGUI.DisabledScope(useBasicOrigin))
                {
                    useCustomOrigin = EditorGUILayout.Toggle("Use Custom Origin?", useCustomOrigin);

                    if (useCustomOrigin && !useBasicOrigin)
                    {
                        originTrap =
                            (Transform) EditorGUILayout.ObjectField("Origin point", originTrap, typeof(Transform),
                                true);
                        if (originTrap != null)
                        {
                            rect.DrawUILine(Color.white);
                            EditorGUI.BeginChangeCheck();
                            GUILayout.Label("Origin point Coordinate is", EditorStyles.boldLabel);
                            useSmoothMovement = EditorGUILayout.Toggle("Use SmoothMovement", useSmoothMovement);
                            originTrapVect = EditorGUILayout.Vector2Field("Position of the Origin Object :",
                                new Vector3(originTrapVect.x, originTrapVect.y));
                            if (EditorGUI.EndChangeCheck())
                            {
                                ApplyTransform();
                            }
                        }

                        if (GUILayout.Button("Retrieve Parameter of Obj"))
                        {
                            GetTransform();
                        }
                    }
                    else
                    {
                        originTrap = null;
                        originTrapVect = Vector2.zero;
                    }

                    ManagerBool();
                    break;
                }

            ////--------------------END AREA FOR THE POSITION OF THE CONTROLLER----------------------////
        }

        serializedObject.Update();
        serializedObject.ApplyModifiedProperties();
    }
    

    void GetTransform()
    {
        originTrapVect = originTrap.position;
    }

    void ApplyTransform()
    {
        if (useSmoothMovement)
        {
            originTrap.position = new Vector2(originTrapVect.x + gridSnapSmooth, originTrapVect.y + gridSnapSmooth);
        }
        else
        {
            originTrap.position = new Vector2(originTrapVect.x + gridSnap, originTrapVect.y + gridSnap);
        }
    }

    void ManagerBool()
    {
        if (useBasicOrigin)
        {
            useCustomOrigin = false;
        }

        if (useCustomOrigin)
        {
            useBasicOrigin = false;
        }
    }

    void TchekObj()
    {
        var trap = target as ElectrickTrap;
        if (trap == null) return;
        if (useBasicOrigin)
        {
            originTrap = trap.trapFolder.gameObject.transform;
        }

        if (originTrap != null)
        {
            if (useBasicOrigin)
            {
                Debug.Log(("Origin Position = " + originTrap.position));
            }

            else if (useCustomOrigin)
            {
                Debug.Log(("Origin Position = " + originTrap.position));
            }
        }
        else
        {
            Debug.Log("No Object");
        }
    }

    void ClearList()
    {
        var trap = target as ElectrickTrap;
        if (trap == null) return;
        trap.electrikPrefab.Clear();
        foreach (Transform child in trap.trapFolder.transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}