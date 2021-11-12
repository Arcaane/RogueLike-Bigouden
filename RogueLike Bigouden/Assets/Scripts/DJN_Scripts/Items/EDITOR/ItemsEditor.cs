using System;
using System.Reflection.Emit;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;


/*public class AssetHandle
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        Items itm = EditorUtility.InstanceIDToObject(instanceId) as Items;

        if (itm != null)
        {
            ItemsWindowEditor.Open(itm);
            return true;
        }{}

        return false;
    }
    
} */


[Serializable]
[CustomEditor(typeof(Items))] 
public class ItemsEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        Items items = (Items) target;

        items.itemID = EditorGUILayout.IntField("Item ID", items.itemID);
        items.itemName = EditorGUILayout.TextField("Item name", items.itemName);
        items.description = EditorGUILayout.TextField("Description", items.description);
        items.price = EditorGUILayout.IntField("Item price", items.price);
        items.rarity = (Items.Rarity) EditorGUILayout.EnumPopup("Rarity", items.rarity);
        GUILayout.Space(20);
        GUILayout.Label("Item Icon");
        items.image = (Sprite) EditorGUILayout.ObjectField(items.image, typeof(Sprite), false);
        
        GUILayout.Space(20);

        items.type = (Items.Type) EditorGUILayout.EnumPopup("Type", items.type);
        items.condition = (Items.Condition) EditorGUILayout.EnumPopup("Condition", items.condition);
        
        switch (items.condition)
        {
            case Items.Condition.Action:
                items.action = (Items.Action) EditorGUILayout.EnumPopup("Action List", items.action);
                items.conditionActionMustBeDone = EditorGUILayout.Toggle("Condition State", items.conditionActionMustBeDone);
                break;
            
            case Items.Condition.Value:
                items.value = (Items.Value) EditorGUILayout.EnumPopup("Value List", items.value);
                items.conditionValueToReach = EditorGUILayout.IntField("Value Condition", items.conditionValueToReach);
                break;
            
            case Items.Condition.State:
                items.state = (Items.State) EditorGUILayout.EnumPopup("State list", items.state);
                items.conditionActionMustBeDone = EditorGUILayout.Toggle("Condition State", items.conditionActionMustBeDone);
                break;
        }
        
        GUILayout.Space(20);

        items.effectOn = (Items.Effect) EditorGUILayout.EnumPopup("Effect On", items.effectOn);
        switch (items.effectOn)
        {
            case Items.Effect.Variable:
                items.augmentation = (Items.Augmentation) EditorGUILayout.EnumPopup("Variable List", items.augmentation);
                items.onCurrent = EditorGUILayout.Toggle("On Current Value", items.onCurrent);
                items.modAmount = EditorGUILayout.IntField("Amount modification", items.modAmount);
                GUILayout.Space(10);
                GUILayout.Label("Activation Rate");
                items.rate = EditorGUILayout.Slider(items.rate, 0, 100, GUILayout.Width(300));
                break;
            
            case Items.Effect.Object:
                items.alteration = (Items.Alteration) EditorGUILayout.EnumPopup("Object List", items.alteration);
                items.objectPrefab = (GameObject) EditorGUILayout.ObjectField(items.objectPrefab, typeof(GameObject), true);
                items.spawnPoint = (Transform) EditorGUILayout.ObjectField(items.spawnPoint, typeof(Transform), true);
                items.spawnAmount = EditorGUILayout.IntField("Spawn Amount", items.spawnAmount);
                GUILayout.Space(10);
                GUILayout.Label("Object Proprieties");
                items.mustBeActivated = EditorGUILayout.Toggle("Must be Activated", items.mustBeActivated);
                if (items.mustBeActivated)
                {
                    items.action = (Items.Action) EditorGUILayout.EnumPopup("Action List", items.action);
                    items.conditionActionMustBeDone = EditorGUILayout.Toggle("Condition State", items.conditionActionMustBeDone);
                }
                GUILayout.Space(10);
                items.onCurrent = EditorGUILayout.Toggle("On Current Value", items.onCurrent);
                items.modAmount = EditorGUILayout.IntField("Amount modification", items.modAmount);
                GUILayout.Space(10);
                GUILayout.Label("Activation Rate");
                items.rate = EditorGUILayout.Slider(items.rate, 0, 100, GUILayout.Width(300));
                items.activeOnlyOnIt = EditorGUILayout.Toggle("Active Effect Only On Object", items.activeOnlyOnIt);
                items.spawnTime = EditorGUILayout.FloatField("Spawn Active Time", items.spawnTime);
                break;
            
        }
        
        GUILayout.Space(20);
        items.target = (Items.Target) EditorGUILayout.EnumPopup("Target List", items.target);
        switch (items.target)
        {
            case Items.Target.Player:
                items.player = (Items.Player) EditorGUILayout.EnumPopup("Player List", items.player);
                break;
            
            case Items.Target.Enemy:
                items.enemy = (Items.Enemy) EditorGUILayout.EnumPopup("Enemy List", items.enemy);
                break;
        }
        
        GUILayout.Space(20);
        items.overTime = EditorGUILayout.Toggle("Is Overtime ?", items.overTime);
        if (items.overTime)
        {
            items.overTimeDuration = EditorGUILayout.FloatField("Overtime Duration", items.overTimeDuration);
        }

        
        GUILayout.Space(50);
        if (GUILayout.Button("Open Editor Window"))
        {
            ItemsWindowEditor.Open((Items)target);
        }
        GUILayout.Label("Fenêtre non fonctionnel");
    }
}
