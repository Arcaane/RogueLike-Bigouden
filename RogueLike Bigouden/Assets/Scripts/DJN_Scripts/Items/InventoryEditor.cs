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
       GUILayout.Space(20);
        if (GUILayout.Button("Check Items"))
        {
            inventory.CheckItemCondition();
        }
    }
}
