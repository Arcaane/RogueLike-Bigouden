using System;
using UnityEditor;
using UnityEngine;

public class ItemsWindowEditor : ExtentedEditorWindow
{
    public static void Open(Items items)
    {
        ItemsWindowEditor window = GetWindow<ItemsWindowEditor>("Items Window Editor");
        window.serializedObject = new SerializedObject(items);
    }

    private void OnGUI()
    {
        currentProperty = serializedObject.FindProperty("item");
        DrawProperties(currentProperty, true);
    }
}

