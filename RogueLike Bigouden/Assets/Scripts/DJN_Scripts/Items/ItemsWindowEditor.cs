using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;

public class ItemsWindowEditor : EditorWindow
{
    [MenuItem("Window/ItemCustomEditor")]
    public static void ShowWindow()
    {
        EditorWindow window = EditorWindow.GetWindow(typeof(ItemsWindowEditor));
    }
    

    private void OnGUI()
    {
    }
}

