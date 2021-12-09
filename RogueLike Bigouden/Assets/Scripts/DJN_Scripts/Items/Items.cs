using System;
using UnityEditor;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "items", menuName = "items", order = 1)]
public class Items : ScriptableObject
{
    //items informations
    [HideInInspector][SerializeField]public int itemID;
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


