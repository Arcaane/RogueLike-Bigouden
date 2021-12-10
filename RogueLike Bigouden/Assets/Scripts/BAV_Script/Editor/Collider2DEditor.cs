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
            edge = (EdgeCollider2D) EditorGUILayout.ObjectField("EdgeCollider2D to edit", edge, typeof(EdgeCollider2D),
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