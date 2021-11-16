using System;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "new Item", menuName = "Item", order = 1)]
public class Items : ScriptableObject
{
    //items informations
    [HideInInspector] public int itemID;
    [HideInInspector]public string itemName;
    [HideInInspector]public string description;
    [HideInInspector]public Sprite image;
    [HideInInspector]public int price;
    [HideInInspector]public Rarity rarity;
    
    //items conditon to active
    [HideInInspector]public Condition condition;
    public enum Condition { None, Action, Value, State }
    [HideInInspector] public Action action;

  
    public enum Rarity { Commun, Rare, Epic }
    
    [HideInInspector] public Target target;
    [HideInInspector] public Type type;

    [HideInInspector] public Effect effectOn;
    [HideInInspector] public Augmentation augmentation;
    [HideInInspector]public Value value;
    [HideInInspector] public State state;
    [HideInInspector] public Player player;
    [HideInInspector] public Enemy enemy;
    [HideInInspector] public Alteration alteration;
    [HideInInspector]public ActionTarget actionTarget;
    [HideInInspector]public ActionPlayer actionPlayer;
    [HideInInspector] public ActionEnemy actionEnemy;
    [HideInInspector]public VariableTarget variableTarget;
    [HideInInspector] public Operator _operator;
    [HideInInspector] public SpawnPoint spawnPoint;
    [HideInInspector] public PlayerSpawn playerSpawn;
    [HideInInspector]public EnemySpawn enemySpawn;
    
    
    [HideInInspector]public bool conditionActionMustBeDone;
    [HideInInspector] public int conditionValueToReach;
    
    [HideInInspector]public bool onCurrent;
    [HideInInspector] public int modAmount;
    [HideInInspector] public bool modIsAnotherVariable;
    [HideInInspector] [Range(0,1)] public float anotherVariableModPourcentage;
    [HideInInspector] [Range(0,100)] public float rate;
    
    [HideInInspector]public GameObject objectPrefab;
    [HideInInspector] public Transform specialSpawnPoint;
    [HideInInspector] public int spawnAmount;
    [HideInInspector] public bool mustBeActivated;
    [HideInInspector] public float spawnTime;
    
    [HideInInspector] public bool overTime; 
    [HideInInspector]public float overTimeDuration;

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

