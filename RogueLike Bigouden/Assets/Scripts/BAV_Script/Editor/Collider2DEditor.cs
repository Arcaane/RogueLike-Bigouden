using UnityEditor;
using UnityEngine;
using System;
using System.Diagnostics;

public class Collider2DEditor : EditorWindow
{
    [MenuItem("Window/Collider Manager Snap")]
    public static void ShowWindow()
    {
        EditorWindow _window = GetWindow(typeof(Collider2DEditor));
        _window.maxSize = new Vector2(500, 500);
        _window.minSize = new Vector2(250, 100);
        _window.Show();
    }

    //Choice Between Collider and Position Object
    int toolbarInt = 0;
    string[] toolbarStrings = {"Collider", "Position Object"};
    
    //Editor Button Size--------------
    Vector2 sizeBoxW_H = new Vector2(10f, 80f);

    //Collider Section
    BoxCollider2D box;
    EdgeCollider2D edge;

    //BooleanForType
    private bool useEdge;
    private bool useBox;

    //BoxColliderSection
    Vector2 offsetBox = new Vector2();
    Vector2 offsetBox_Save = new Vector2();
    Vector2 size = new Vector2();

    Vector2 size_Save = new Vector2();

    //For Camera Only
    Vector2 scrollPosition = Vector2.zero;

    //EdgeColliderModifier
    Vector2[] verticesEdge = Array.Empty<Vector2>();
    Vector2[] vertices_Save = Array.Empty<Vector2>();
    Vector2 offsetEdge = new Vector2();
    Vector2 offsetEdge_Save = new Vector2();

    //Float----------
    const float gridSnap = 0.53333f;
    const float gridSnapSmooth = 0.53333f;

    void OnGUI()
    {
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);


        switch (toolbarInt)
        {
            case 0:

                #region BoxCollider

                useBox = EditorGUILayout.Toggle("Use BoxCollider2D ", useBox);
                if (useBox)
                {
                    GUILayout.Label("BoxCollider2D point editor", EditorStyles.boldLabel);
                    box = (BoxCollider2D) EditorGUILayout.ObjectField("BoxCollider to edit", box, typeof(BoxCollider2D),
                        true);

                    if (box != null)
                    {
                        EditorGUI.BeginChangeCheck();
                        size = EditorGUILayout.Vector2Field("Size :", new Vector2(size.x, size.y));
                        offsetBox = EditorGUILayout.Vector2Field("Box Offset :", new Vector2(offsetBox.x, offsetBox.y));
                        if (EditorGUI.EndChangeCheck())
                        {
                            ApplyValue();
                        }
                    }
                }

                #endregion BoxCollider

                #region EdgeCollider

                useEdge = EditorGUILayout.Toggle("Use EdgeCollider2D ", useEdge);
                if (useEdge)
                {
                    GUILayout.Label("EdgeCollider2D point editor", EditorStyles.boldLabel);
                    offsetEdge = EditorGUILayout.Vector2Field("Offset Edge :", new Vector2(offsetEdge.x, offsetEdge.y));
                    edge = (EdgeCollider2D) EditorGUILayout.ObjectField("EdgeCollider2D to edit", edge,
                        typeof(EdgeCollider2D),
                        true);

                    EditorGUI.BeginChangeCheck();
                    if (verticesEdge.Length != 0)
                    {
                        for (int i = 0; i < verticesEdge.Length; ++i)
                        {
                            verticesEdge[i] = (Vector2) EditorGUILayout.Vector2Field("Element " + i, verticesEdge[i]);
                        }
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        ApplyValue();
                    }
                }

                #endregion EdgeCollider


                if (useBox || useEdge)
                {
                    if (GUILayout.Button("Retrieve Parameter of Obj"))
                    {
                        RecupValue();
                    }

                    if (GUILayout.Button("Set New Parameter on Obj"))
                    {
                        ApplyValue();
                    }

                    /*
                    if (GUILayout.Button("Save Parameter of the Object"))
                    {
                        SaveOldValue();
                    }
        
                    if (GUILayout.Button("Place Saved Parameter"))
                    {
                        PlaceOldValue();
                    }
                    */
                }

                break;
            case 1:
                 ////--------------------BUTTON POSITION FIRST LINE----------------------////
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    //trap.electrikPrefab.Add(objTrap);
                }

                if (GUILayout.Button("Top\nPosition", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    //trap.InstantiatePrefab(0);
                }

                GUILayout.Button("", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y));
                GUILayout.EndHorizontal();
                 

                ////--------------------BUTTON POSITION SECOND LINE----------------------////

                GUILayout.BeginHorizontal();
                 //--------------------LEFT BUTTON----------------------//
                if (GUILayout.Button("Left\nPosition", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    //trap.InstantiatePrefab(1);
                }

                //--------------------ORIGIN BUTTON----------------------//
                if (GUILayout.Button("Origin\nPosition", GUILayout.ExpandWidth(false),
                    GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    //TchekObj();
                }

                //--------------------RIGHT BUTTON----------------------//
                if (GUILayout.Button("Right\nPosition", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                    GUILayout.MaxWidth(sizeBoxW_H.y)))
                {
                    //trap.InstantiatePrefab(2);
                }
                 GUILayout.EndHorizontal();
                 
                 ////--------------------BUTTON POSITION THIRD LINE----------------------////

                 GUILayout.BeginHorizontal();

                 GUILayout.Button("", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                     GUILayout.MaxWidth(sizeBoxW_H.y));
                 if (GUILayout.Button("Down\nPosition", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                     GUILayout.MaxWidth(sizeBoxW_H.y)))
                 {
                     //trap.InstantiatePrefab(3);
                 }

                 GUILayout.Button("", GUILayout.ExpandWidth(false), GUILayout.MaxHeight(sizeBoxW_H.y),
                     GUILayout.MaxWidth(sizeBoxW_H.y));

                 GUILayout.EndHorizontal();
                 

                 ////--------------------BUTTON POSITION SECOND LINE----------------------////
                 //DrawUILine(Color.black);

                 //--------------------USE BASIC ORIGIN----------------------//
                 /*
                 using (new EditorGUI.DisabledScope(useCustomOrigin))
                 {
                     GUILayout.Label("Use Basic Origin", EditorStyles.boldLabel);
                     useBasicOrigin = EditorGUILayout.Toggle("Use Basic Origin", useBasicOrigin);
                 }*/

                 //DrawUILine(Color.black);

                 //--------------------USE CUSTOM ORIGIN----------------------//
                 GUILayout.BeginHorizontal();
                 GUILayout.Label("Custom Origin Point", EditorStyles.boldLabel);
                 GUILayout.EndHorizontal();
                break;
        }


        GUILayout.EndScrollView();
    }


    void RecupValue()
    {
        if (box && useBox)
        {
            offsetBox = box.offset;
            size = box.size;
        }

        if (edge && useEdge)
        {
            verticesEdge = edge.points;
        }
    }

    void ApplyValue()
    {
        if (box && useBox)
        {
            box.offset = offsetBox;
            box.size = size;
        }

        if (edge && useEdge)
        {
            edge.offset = offsetEdge;
            edge.points = verticesEdge;
        }
    }

    /*
    void SaveOldValue()
    {
        offsetBox = offsetBox_Save;
        size = size_Save;
        edge.offset = offsetEdge_Save;
        edge.points = vertices_Save;
    }

    void PlaceOldValue()
    {
        if (box && useBox)
        {
            box.offset = offsetBox_Save;
            box.size = size_Save;
        }

        if (edge && useEdge)
        {
            edge.offset = offsetEdge_Save;
            edge.points = vertices_Save;
        }
    }
    */

/*
	void OnSelectionChange() {
		if (Selection.gameObjects.Length == 1) {
			EdgeCollider2D aux = Selection.gameObjects[0].GetComponent<EdgeCollider2D>();
			
			if (aux) {
				edge = aux;
				vertices = edge.points;
			}
		}
	}
*/
}