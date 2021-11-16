using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
[CustomEditor(typeof(SpawnedItem))]
public class SpawnItemEditor : Editor
{
   public override void OnInspectorGUI()
   {
      SpawnedItem spawnedItem = (SpawnedItem) target;

      spawnedItem.spawnedSprite = (Sprite) EditorGUILayout.ObjectField(spawnedItem.spawnedSprite, typeof(Sprite), false);
      GUILayout.Label("Effect");
      spawnedItem.target = (SpawnedItem.Target) EditorGUILayout.EnumPopup("Target", spawnedItem.target);
      spawnedItem._operator = (SpawnedItem.Operator) EditorGUILayout.EnumPopup("Operator", spawnedItem._operator);
      spawnedItem.valueToMod = (SpawnedItem.ValueToMod) EditorGUILayout.EnumPopup("Variable", spawnedItem.valueToMod);
      spawnedItem.modAmount = EditorGUILayout.FloatField("Mod Amount", spawnedItem.modAmount);
      GUILayout.Space(10);
      GUILayout.Label("Overtime");
      spawnedItem.onTime = EditorGUILayout.Toggle("Does it's on time ?", spawnedItem.onTime);

      if (spawnedItem.onTime)
      {
         spawnedItem.onTimeDuration = EditorGUILayout.FloatField("On time duration", spawnedItem.onTimeDuration);
      }
      
      
       
      if (GUI.changed)
      {
         EditorUtility.SetDirty(spawnedItem);
      }
   }
}
