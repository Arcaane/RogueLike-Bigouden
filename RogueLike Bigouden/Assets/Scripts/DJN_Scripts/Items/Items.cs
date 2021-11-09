using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item", order = 1)]
public class Items : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite image;
    public int price;
    public Rarity rarity;

    public enum Rarity
    {
        Commun,
        Rare,
        Epic
    }

    public ItemEffect[] itemEffects;

    [Serializable]
    public struct ItemEffect
    {

        [Header("Configuration")]
        [Tooltip("Choisissez sur qui va s'effectuer la modification")]
        public Target target;
        [Tooltip("Choisissez si cela altére positivement ou négativement la cible")]
        public Type type;
        [Tooltip("Choisissez le type de condition à remplir pour l'activer")]
        public Condition condition;
        [Tooltip("Choisissez sur quel élement de la cible s'effectue l'effet")]
        public Effect effectOn;
        
        [Header("Action Condition")]
        [Tooltip("Selectionnez l'action a effectué")]
        public Action action;
        [Tooltip("Est-ce que la condition doit avoir lui ou non ?")]
        public bool conditionActionMustBeDone;
        
        [Header("Value Condition")]
        [Tooltip("Selectionnez la valeur à atteindre")]
        public Value value;
        [Tooltip("Quel montant cette valeur doit atteindre pour que l'effet s'active ?")]
        public int conditionValueToReach;
        
        [Header("On Variable")]
        [Tooltip("Choississez la valeur à modifier")]
        public Augmentation augmentation;
        [Tooltip("Est-ce que c'est sur le montant actuel ? Si faux, c'est sur le maximum")]
        public bool onCurrent;
        [Tooltip("De quel montant cette valeur est-elle modifié ?")]
        public int amount;
        [Range(0,100)]
        public float rate;
        
        [Header("On Object")]
        [Tooltip("Choisissez le type d'altération que subit la cible")]
        public Alteration alteration;
        [Tooltip("Choisissez l'objet de la cible qui subira la modification")]
        public GameObject targetObject;

        public int destructEffect;
        [Tooltip("Choisissez, le cas écheant, le point d'apparition de l'objet")]
        public Transform pointOfAppear;
        [Tooltip("Choisissez le nombre d'objet qui doit apparaître")]
        public float appearAmount;
        [Range(0,100)]
        public float appearRate;
        
        [Header("Is Over Time ?")]
        [Tooltip("L'effet est-il sur la durée ?")]
        public bool overTime;
        [Tooltip("Choisissez, le cas échéant, la durée de l'effet")]
        public float overTimeDuration;

   
        public enum Effect
        {
            Variable,
            Object
        }
        public enum Type
        {
            Bonus,
            Malus
        }

        public enum Condition
        {
            None,
            Action,
            Value,
            BrokeSomething

        }

        public enum Action
        {
            AttackX,
            AttackY,
            AttackDistance,
            AttackUltime,
            Dash,
            GetHurt,
            Death
        }

        public enum Value
        {
            Health,
            Energy,
            Money
        }

        public enum Target
        {
            Player,
            Enemy,
            PlayerProjectile,
            EnemyProjectile
        }

        public enum Augmentation
        {
            None,
            Damage,
            AttackRange,
            AttackSpeed,
            Money,
            Health,
            Speed,
            Dash,
            Energy
        }

        public enum Alteration
        {
            Creation,
            Destruction
        }

    }
}
