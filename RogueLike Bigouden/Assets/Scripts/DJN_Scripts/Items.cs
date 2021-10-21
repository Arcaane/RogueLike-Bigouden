using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item", order = 1)]
public class Items : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite image;
    public int price;
    public Rarity rarity;
    public enum Rarity{ Commun, Rare, Epic}

    public ItemEffect[] itemEffects;

    void Start()
    {
        
    }
    
    [Serializable]
  public struct ItemEffect
  {
      
      public Type type;
      public Condition condition;
      public float conditionValue;
      public bool conditionTrueOrFalse;
      public Target target;
      public Affected affected;
      public float affectValue;
      public bool onTime;
      public float onTimeDuration;
      
      public enum Type
      {
          Bonus,
          Malus
      }

      public enum Condition
      {
          None,
          AttackX,
          AttackY,
          AttackUlt,
          Dash,
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

      public enum Affected
      {
          Damage,
          Money,
          Health,
          Speed,
          Dash,
          Energy
      }
  }

}
