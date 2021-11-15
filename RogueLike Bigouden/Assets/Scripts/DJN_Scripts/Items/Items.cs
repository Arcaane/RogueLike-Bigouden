using System;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "new Item", menuName = "Item", order = 1)]
public class Items : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string description;
    public Sprite image;
    public int price;
    public Rarity rarity;
    public enum Rarity { Commun, Rare, Epic }
    
    public Target target;
    public Type type;
    public Condition condition;
    public Effect effectOn;
    public Action action;
    public Augmentation augmentation;
    public Value value;
    public State state;
    public Player player;
    public Enemy enemy;
    public Alteration alteration;
    public ActionTarget actionTarget;
    public ActionPlayer actionPlayer;
    public ActionEnemy actionEnemy;
    public VariableTarget variableTarget;
    public Operator _operator;
    public SpawnPoint spawnPoint;
    public PlayerSpawn playerSpawn;
    public EnemySpawn enemySpawn;
    
    
    public bool conditionActionMustBeDone;
    public int conditionValueToReach;
    
    public bool onCurrent;
    public int modAmount;
    public bool modIsAnotherVariable;
    [Range(0,1)] public float anotherVariableModPourcentage;
    [Range(0,100)] public float rate;
    
    public GameObject objectPrefab;
    public Transform specialSpawnPoint;
    public int spawnAmount;
    public bool mustBeActivated;
    public float spawnTime;
    
    public bool overTime; 
    public float overTimeDuration;

    public enum Operator
    {
        Add,
        Substract,
        Multiplie
    }

    public enum Effect { Variable, Object }
        public enum Type { Bonus, Malus }

        public enum Condition { None, Action, Value, State }

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

