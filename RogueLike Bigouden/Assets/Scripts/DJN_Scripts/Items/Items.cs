using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item", order = 1)]
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

    public bool conditionActionMustBeDone;
    public int conditionValueToReach;
    public bool onCurrent;
    public int modAmount;
    [Range(0,100)] public float rate;
    public GameObject objectPrefab;
    public Transform spawnPoint;
    public int spawnAmount;
    public bool activeOnlyOnIt;
    public bool mustBeActivated;
    public bool overTime; 
    public float overTimeDuration;

        public enum Effect { Variable, Object }
        public enum Type { Bonus, Malus }

        public enum Condition { None, Action, Value, State }

        public enum Action { AttackX, AttackY, AttackDistance, AttackUltime, Dash, GetHurt, Death }

        public enum Value
        { Health, Energy, Money }

        public enum Target{Player, Enemy}
        
        public enum Player {Everyone, CurrentPlayer}

        public enum Enemy{All, Barman, Cac, Rush, Tir}

        public enum Augmentation { DamageX, DamageY, DamageB, DamageUlt, AttackRangeX, AttackRangeY, AttackRangeB, AttackRangeUlt, AttackSpeedX, AttackSpeedY, AttackSpeedB, AttackSpeedUlt, AddMoney, MoreMoney, Health, Speed, DashRange, Energy }

        public enum Alteration { Creation, Destruction }
        
        public enum State{ Alive, Dead }

}

