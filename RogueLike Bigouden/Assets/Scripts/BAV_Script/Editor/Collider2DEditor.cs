using UnityEditor;
using UnityEngine;
using System;

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

    //Collider Section
    BoxCollider2D box;
    EdgeCollider2D edge;

    //BooleanForType
    private bool useEdge;
    private bool useBox;

    //BoxColliderSection
    Vector2 offset = new Vector2();
    Vector2 offset_Save = new Vector2();
    Vector2 size = new Vector2();

    Vector2 size_Save = new Vector2();

    //For Camera Only
    Vector2 scrollPosition = Vector2.zero;

    //EdgeColliderModifier
    Vector2[] vertices = Array.Empty<Vector2>();
    Vector2[] vertices_Save = Array.Empty<Vector2>();

    void OnGUI()
    {
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true);

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
                size = EditorGUILayout.Vector2Field("Offset :", new Vector2(size.x, size.y));
                offset = EditorGUILayout.Vector2Field("Offset :", new Vector2(offset.x, offset.y));
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
            edge = (EdgeCollider2D) EditorGUILayout.ObjectField("EdgeCollider2D to edit", edge, typeof(EdgeCollider2D),
                true);

            EditorGUI.BeginChangeCheck();
            if (vertices.Length != 0)
            {
                for (int i = 0; i < vertices.Length; ++i)
                {
                    vertices[i] = (Vector2) EditorGUILayout.Vector2Field("Element " + i, vertices[i]);
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
            if (GUILayout.Button("Retrieve"))
            {
                RecupValue();
            }

            if (GUILayout.Button("Set"))
            {
                ApplyValue();
            }
            
            if (GUILayout.Button("Save Parameter on Slot 1"))
            {
                ApplyValue();
            }
            
            if (GUILayout.Button("Save Parameter on Slot 2"))
            {
                ApplyValue();
            }
        }

        GUILayout.EndScrollView();
    }

    void RecupValue()
    {
        if (box && useBox)
        {
            offset = box.offset;
            size = box.size;
        }

        if (edge && useEdge)
        {
            vertices = edge.points;
        }
        
        if (edge && useEdge)
        {
            offset_Save = box.offset;
            size_Save = box.size;
            vertices_Save = edge.points;
        }
    }

    void ApplyValue()
    {
        if (box && useBox)
        {
            box.offset = offset;
            box.size = size;
        }

        if (edge && useEdge)
        {
            edge.points = vertices;
        }
    }

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