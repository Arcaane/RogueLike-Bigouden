using System;
using UnityEditor;
using UnityEngine;

[Serializable]
[CustomEditor(typeof(Items))] 
public class ItemsEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        Items items = (Items)target;

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
                items.actionTarget = (Items.ActionTarget) EditorGUILayout.EnumPopup("Action Target", items.actionTarget);
                switch (items.actionTarget)
                {
                    case Items.ActionTarget.Player:
                        items.actionPlayer = (Items.ActionPlayer) EditorGUILayout.EnumPopup("Player Action Target", items.actionPlayer);
                        break;
                    
                    case Items.ActionTarget.Enemy:
                        items.actionEnemy =
                            (Items.ActionEnemy) EditorGUILayout.EnumPopup("Enemy Action Target", items.actionEnemy);
                        break;
                }
                
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
        
         
        GUILayout.Space(10);
        GUILayout.Label("Activation Rate");
        items.rate = EditorGUILayout.Slider(items.rate, 0, 100, GUILayout.Width(300));
        
        GUILayout.Space(20);

        items.effectOn = (Items.Effect) EditorGUILayout.EnumPopup("Effect On", items.effectOn);
        switch (items.effectOn)
        {
            case Items.Effect.Variable:
                items.augmentation = (Items.Augmentation) EditorGUILayout.EnumPopup("Variable List", items.augmentation);
                items._operator = (Items.Operator) EditorGUILayout.EnumPopup("Operator", items._operator);
                items.onCurrent = EditorGUILayout.Toggle("On Current Value", items.onCurrent);
                items.modIsAnotherVariable = EditorGUILayout.Toggle("Is Mod Amount is From Another Variable ?", items.modIsAnotherVariable);
                
                if (items.modIsAnotherVariable)
                { 
                    items.variableTarget = (Items.VariableTarget) EditorGUILayout.EnumPopup("Variable Target", items.variableTarget);
                    GUILayout.Label("Pourcentage");
                    items.anotherVariableModPourcentage = EditorGUILayout.Slider(items.anotherVariableModPourcentage, 0, 1);
                }
                else
                {
                    items.modAmount = EditorGUILayout.IntField("Amount modification", items.modAmount);
                }

                GUILayout.Space(10);

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
                
                break;
            
            case Items.Effect.Object:
                items.alteration = (Items.Alteration) EditorGUILayout.EnumPopup("Object List", items.alteration);
                items.objectPrefab = (GameObject) EditorGUILayout.ObjectField(items.objectPrefab, typeof(GameObject), true);
                items.spawnPoint = (Items.SpawnPoint) EditorGUILayout.EnumPopup("Spawn Point", items.spawnPoint);

                switch (items.spawnPoint)
                {

                    case Items.SpawnPoint.Player:
                        items.playerSpawn = (Items.PlayerSpawn) EditorGUILayout.EnumPopup("Player Spawn", items.playerSpawn);
                        break;
                    
                    case Items.SpawnPoint.Enemy:
                        items.enemySpawn = (Items.EnemySpawn) EditorGUILayout.EnumPopup("Enemy Spawn", items.enemySpawn);
                         break;
                    
                    case Items.SpawnPoint.Special:
                        items.specialSpawnPoint = (Transform) EditorGUILayout.ObjectField(items.specialSpawnPoint, typeof(Transform), true);
                        break;
                }
                
                items.spawnAmount = EditorGUILayout.IntField("Spawn Amount", items.spawnAmount);
                items.spawnTime = EditorGUILayout.FloatField("Spawn Active Time", items.spawnTime);
                break;
            
        }
        
        GUILayout.Space(20);
        
        
        GUILayout.Space(20);
        items.overTime = EditorGUILayout.Toggle("Is Overtime ?", items.overTime);
        if (items.overTime)
        {
            items.overTimeDuration = EditorGUILayout.FloatField("Overtime Duration", items.overTimeDuration);
        }
        
    }
}
