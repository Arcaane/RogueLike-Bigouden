using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Inventory inventory = (Inventory) target;
        if (GUILayout.Button("Update Items"))
        {
            inventory.UpdateItems();
        }
    }
}
