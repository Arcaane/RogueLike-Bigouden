using System;
using UnityEditor;
using UnityEngine;
using static ItemsWindowEditor.ItemsData;

[Serializable]
public class ItemsWindowEditor : EditorWindow
{
    [SerializeField] private Items itemsData;
    [SerializeField] private ItemsData items;

    [Serializable]
    public struct ItemsData
    { [HideInInspector][SerializeField]public int itemID;
    [HideInInspector][SerializeField]public string itemName;
    [HideInInspector][SerializeField]public string description;
    [HideInInspector][SerializeField]public Sprite image;
    [HideInInspector][SerializeField]public int price;
    [HideInInspector][SerializeField]public Rarity rarity;
    
    //items conditon to active
    [HideInInspector][SerializeField]public Condition condition;
    public enum Condition { None, Action, Value, State }
    [HideInInspector] [SerializeField]public Action action;

  
    public enum Rarity { Commun, Rare, Epic }
    
    [HideInInspector] [SerializeField]public Target target;
    [HideInInspector][SerializeField] public Type type;

    [HideInInspector][SerializeField] public Effect effectOn;
    [HideInInspector] [SerializeField]public Augmentation augmentation;
    [HideInInspector][SerializeField]public Value value;
    [HideInInspector][SerializeField] public State state;
    [HideInInspector][SerializeField] public Player player;
    [HideInInspector][SerializeField] public Enemy enemy;
    [HideInInspector][SerializeField] public Alteration alteration;
    [HideInInspector][SerializeField]public ActionTarget actionTarget;
    [HideInInspector][SerializeField]public ActionPlayer actionPlayer;
    [HideInInspector][SerializeField] public ActionEnemy actionEnemy;
    [HideInInspector][SerializeField]public VariableTarget variableTarget;
    [HideInInspector][SerializeField] public Operator _operator;
    [HideInInspector] [SerializeField]public SpawnPoint spawnPoint;
    [HideInInspector][SerializeField] public PlayerSpawn playerSpawn;
    [HideInInspector][SerializeField]public EnemySpawn enemySpawn;
    
    
    [HideInInspector][SerializeField]public bool conditionActionMustBeDone;
    [HideInInspector] [SerializeField]public int conditionValueToReach;
    
    [HideInInspector][SerializeField]public bool onCurrent;
    [HideInInspector][SerializeField] public int modAmount;
    [HideInInspector] [SerializeField]public bool modIsAnotherVariable;
    [HideInInspector][SerializeField] [Range(0,1)] public float anotherVariableModPourcentage;
    [HideInInspector][SerializeField] [Range(0,100)] public float rate;
    
    [HideInInspector][SerializeField]public GameObject objectPrefab;
    [HideInInspector][SerializeField] public Transform specialSpawnPoint;
    [HideInInspector][SerializeField] public int spawnAmount;
    [HideInInspector][SerializeField] public bool mustBeActivated;
    [HideInInspector][SerializeField] public float spawnTime;
    
    [HideInInspector][SerializeField] public bool overTime; 
    [HideInInspector][SerializeField]public float overTimeDuration;

    public enum Operator
    {
        Add,
        Substract,
        Multiplie
    }

    public enum Effect { Variable, Object }
        public enum Type { Bonus, Malus }


        public enum Action { AttackX, AttackY, AttackDistance, AttackUltime, Dash, GetHurt, Death, KillOrDestroy }

        public enum ActionTarget{ None, Player, Enemy, Props}
        
        public enum ActionPlayer{ CurrentPlayer, Everyone}
        public enum ActionEnemy{All, Barman, Cac, Rush, Tir}
    
        public enum SpawnPoint{ Own, Player, Enemy, Special}
        public enum PlayerSpawn{ CurrentPlayer, Everyone}
        public enum EnemySpawn{Target, All, Barman, Cac, Rush, Tir}

        public enum Value
        { Health, Energy, Money }

        public enum Target{Player, Enemy, Props}
        
        public enum Player {CurrentPlayer, Everyone}

        public enum Enemy{All, Barman, Cac, Rush, Tir}

        public enum Augmentation { Shield, Damage, DamageX, DamageY, DamageB, DamageUlt, AttackRange, AttackRangeX, AttackRangeY, AttackRangeB, AttackRangeUlt, AttackSpeed, AttackSpeedX, AttackSpeedY, AttackSpeedB, AttackSpeedUlt, AddMoney, MoreMoney, Health, Speed, DashRange, Energy }

        public enum VariableTarget{Health, Energy, Money}
        public enum Alteration { Creation, Destruction }
        
        public enum State{ Alive, Dead }
    }
    
    [MenuItem("Window/Items Window Editor")]
    public static void ShowWindow()
    {
        ItemsWindowEditor window = GetWindow<ItemsWindowEditor>(false, "Items Window Editor", true);
    }

    private void OnGUI()
    {
        itemsData = CreateInstance<Items>();
        
        #region EDITOR
        items.itemID = EditorGUILayout.IntField("Item ID", items.itemID);
        items.itemName = EditorGUILayout.TextField("Item name", items.itemName);
        items.description = EditorGUILayout.TextField("Description", items.description);
        items.price = EditorGUILayout.IntField("Item price", items.price);
        items.rarity = (ItemsData.Rarity) EditorGUILayout.EnumPopup("Rarity", items.rarity);
        GUILayout.Space(20);
        GUILayout.Label("Item Icon");
        items.image = (Sprite) EditorGUILayout.ObjectField(items.image, typeof(Sprite), false);
        
        GUILayout.Space(20);

        items.type = (ItemsData.Type) EditorGUILayout.EnumPopup("Type", items.type);
        items.condition = (ItemsData.Condition) EditorGUILayout.EnumPopup("Condition", items.condition);
        

        switch (items.condition)
        {
            case Condition.Action:
                items.action = (ItemsData.Action) EditorGUILayout.EnumPopup("Action List", items.action);
                items.conditionActionMustBeDone = EditorGUILayout.Toggle("Condition State", items.conditionActionMustBeDone);
                items.actionTarget = (ItemsData.ActionTarget) EditorGUILayout.EnumPopup("Action Target", items.actionTarget);
                switch (items.actionTarget)
                {
                    case ActionTarget.Player:
                        items.actionPlayer = (ItemsData.ActionPlayer) EditorGUILayout.EnumPopup("Player Action Target", items.actionPlayer);
                        break;
                    
                    case ActionTarget.Enemy:
                        items.actionEnemy =
                            (ItemsData.ActionEnemy) EditorGUILayout.EnumPopup("Enemy Action Target", items.actionEnemy);
                        break;
                }
                
                break;
            
            case Condition.Value:
                items.value = (ItemsData.Value) EditorGUILayout.EnumPopup("Value List", items.value);
                items.conditionValueToReach = EditorGUILayout.IntField("Value Condition", items.conditionValueToReach);
                break;
            
            case Condition.State:
                items.state = (ItemsData.State) EditorGUILayout.EnumPopup("State list", items.state);
                items.conditionActionMustBeDone = EditorGUILayout.Toggle("Condition State", items.conditionActionMustBeDone);
                break;
        }
        
         
        GUILayout.Space(10);
        GUILayout.Label("Activation Rate");
        items.rate = EditorGUILayout.Slider(items.rate, 0, 100, GUILayout.Width(300));
        
        GUILayout.Space(20);

        items.effectOn = (ItemsData.Effect) EditorGUILayout.EnumPopup("Effect On", items.effectOn);
        switch (items.effectOn)
        {
            case Effect.Variable:
                items.augmentation = (ItemsData.Augmentation) EditorGUILayout.EnumPopup("Variable List", items.augmentation);
                items._operator = (ItemsData.Operator) EditorGUILayout.EnumPopup("Operator", items._operator);
                items.onCurrent = EditorGUILayout.Toggle("On Current Value", items.onCurrent);
                items.modIsAnotherVariable = EditorGUILayout.Toggle("Is Mod Amount is From Another Variable ?", items.modIsAnotherVariable);
                
                if (items.modIsAnotherVariable)
                { 
                    items.variableTarget = (ItemsData.VariableTarget) EditorGUILayout.EnumPopup("Variable Target", items.variableTarget);
                    GUILayout.Label("Pourcentage");
                    items.anotherVariableModPourcentage = EditorGUILayout.Slider(items.anotherVariableModPourcentage, 0, 1);
                }
                else
                {
                    items.modAmount = EditorGUILayout.IntField("Amount modification", items.modAmount);
                }

                GUILayout.Space(10);

                items.target = (ItemsData.Target) EditorGUILayout.EnumPopup("Target List", items.target);
                switch (items.target)
                {
                    case Target.Player:
                        items.player = (ItemsData.Player) EditorGUILayout.EnumPopup("Player List", items.player);
                        break;
            
                    case Target.Enemy:
                        items.enemy = (ItemsData.Enemy) EditorGUILayout.EnumPopup("Enemy List", items.enemy);
                        break;
                }
                
                break;
            
            case Effect.Object:
                items.alteration = (ItemsData.Alteration) EditorGUILayout.EnumPopup("Object List", items.alteration);
                items.objectPrefab = (GameObject) EditorGUILayout.ObjectField(items.objectPrefab, typeof(GameObject), true);
                items.spawnPoint = (ItemsData.SpawnPoint) EditorGUILayout.EnumPopup("Spawn Point", items.spawnPoint);

                switch (items.spawnPoint)
                {

                    case SpawnPoint.Player:
                        items.playerSpawn = (ItemsData.PlayerSpawn) EditorGUILayout.EnumPopup("Player Spawn", items.playerSpawn);
                        break;
                    
                    case SpawnPoint.Enemy:
                        items.enemySpawn = (ItemsData.EnemySpawn) EditorGUILayout.EnumPopup("Enemy Spawn", items.enemySpawn);
                         break;
                    
                    case SpawnPoint.Special:
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
        #endregion

        #region ITEMSDATA

        itemsData.itemID = items.itemID;
        itemsData.itemName = items.itemName;
        itemsData.description = items.description;
        itemsData.image = items.image;
        items.price = items.price;
        itemsData.rarity = (Items.Rarity) ((int) items.rarity);
        itemsData.condition = (Items.Condition) ((int) items.condition);
        itemsData.action = (Items.Action) ((int) items.action);
        itemsData.target = (Items.Target) ((int) items.target);
        itemsData.type = (Items.Type) ((int) items.type);
        itemsData.effectOn = (Items.Effect) ((int) items.effectOn);
        itemsData.augmentation = (Items.Augmentation) ((int) items.augmentation);
        itemsData.value = (Items.Value) ((int) items.value);
        itemsData.state = (Items.State) ((int) items.state);
        itemsData.player = (Items.Player) ((int) items.player);
        itemsData.enemy = (Items.Enemy) ((int) items.enemy);
        itemsData.alteration = (Items.Alteration) ((int) items.alteration);
        itemsData.actionTarget = (Items.ActionTarget) ((int) items.actionTarget);
        itemsData.actionPlayer = (Items.ActionPlayer) ((int) items.actionPlayer);
        itemsData.actionEnemy = (Items.ActionEnemy) ((int) items.actionEnemy);
        itemsData.variableTarget = (Items.VariableTarget) ((int) items.variableTarget);
        itemsData._operator = (Items.Operator) ((int) items._operator);
        itemsData.spawnPoint = (Items.SpawnPoint) ((int) items.spawnPoint);
        itemsData.playerSpawn = (Items.PlayerSpawn) ((int) items.playerSpawn);
        itemsData.enemySpawn = (Items.EnemySpawn) ((int) items.enemySpawn);
        itemsData.conditionActionMustBeDone = items.conditionActionMustBeDone;
        itemsData.conditionValueToReach = items.conditionValueToReach;
        itemsData.onCurrent = items.onCurrent;
        itemsData.modAmount = items.modAmount;
        itemsData.modIsAnotherVariable = items.modIsAnotherVariable;
        itemsData.anotherVariableModPourcentage = items.anotherVariableModPourcentage;
        itemsData.rate = items.rate;
        itemsData.objectPrefab = items.objectPrefab;
        itemsData.specialSpawnPoint = items.specialSpawnPoint;
        itemsData.spawnAmount = items.spawnAmount;
        itemsData.mustBeActivated = items.mustBeActivated;
        itemsData.spawnTime = items.spawnTime;
        itemsData.overTime = items.overTime;
        itemsData.overTimeDuration = items.overTimeDuration;
 
        #endregion
        
        if (GUILayout.Button("Create Item"))
        {
            Debug.Log(items.description);
            switch (itemsData.type)
            {
                case Items.Type.Bonus:
                    AssetDatabase.CreateAsset(itemsData, "Assets/Resources/Items/"+ itemsData.itemID + "_" + itemsData.itemName + ".asset");
                    break;
                
                case Items.Type.Malus:
                    AssetDatabase.CreateAsset(itemsData, "Assets/Resources/Malus/"+ itemsData.itemID + "_" + itemsData.itemName + ".asset");
                    break;
            }
            
        }
        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(itemsData);
            base.SaveChanges();
        }
    }

   
}

