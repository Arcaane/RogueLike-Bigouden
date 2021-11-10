using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    public Sprite spawnedSprite;
    public float amountChange;
    public bool onTime;
    public float onTimeDuration;
    public bool somethingOnIt;
    public ValueToMod valueToMod;
    public Target target;
    public Type type;
    public enum ValueToMod{ Health, ShieldPoint, DamageX, DamageY, DamageProjectile, BonusSpeed}
    public enum Target{Player, Enemy}
    public enum Type {Bonus, Malus}

    public PlayerStatsManager playerStats;
    public EnnemyStatsManager enemyStats;

    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStatsManager>();

        switch (type)
        {
            case Type.Bonus:
                amountChange = amountChange;
                break;
                            
            case Type.Malus:
                amountChange = -amountChange;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerStatsManager>())
        {
            Debug.Log("Player on it");
            playerStats = other.GetComponent<PlayerStatsManager>();
        }

        if (other.GetComponent<EnnemyStatsManager>())
        {
            Debug.Log("Enemy on it");
            enemyStats = other.GetComponent<EnnemyStatsManager>();
        }
        
        ItemObjectEffect();
        
    }

    void ItemObjectEffect()
    {
        switch (target)
                    {
                    case Target.Player:
        
                        switch (valueToMod)
                        {
                            case ValueToMod.Health:
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, playerStats.lifePoint, amountChange,
                                        playerStats.lifePoint));
                                }
                                else
                                {
                                    playerStats.lifePoint += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                            case ValueToMod.BonusSpeed:
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, playerStats.bonusSpeed, amountChange,
                                        playerStats.bonusSpeed));
                                }
                                else
                                {
                                    playerStats.bonusSpeed += amountChange;
                                }
                                break;
                            
                            case ValueToMod.ShieldPoint:
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, playerStats.shieldPoint, amountChange,
                                        playerStats.shieldPoint));
                                }
                                else
                                {
                                    playerStats.shieldPoint += Mathf.FloorToInt(amountChange);
                                }
        
                                break;
                            
                            case ValueToMod.DamageX:
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, playerStats.damageX, amountChange,
                                        playerStats.damageX));
                                }
                                else
                                {
                                    playerStats.damageX += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                            case ValueToMod.DamageY:
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, playerStats.damageY, amountChange,
                                        playerStats.damageY));
                                }
                                else
                                {
                                    playerStats.damageY += Mathf.FloorToInt(amountChange);
                                }
                                break;
                                
                            case ValueToMod.DamageProjectile:
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, playerStats.damageProjectile, amountChange,
                                        playerStats.damageProjectile));
                                }
                                else
                                {
                                    playerStats.damageProjectile += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                        }
                        
                        break;
                    
                    
                    case Target.Enemy:
        
                        switch (valueToMod)
                        {
                            case ValueToMod.Health:
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, enemyStats.lifePoint, amountChange,
                                        enemyStats.lifePoint));
                                }
                                else
                                {
                                    enemyStats.lifePoint += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                            case ValueToMod.BonusSpeed:
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, enemyStats.movementSpeed, amountChange,
                                        enemyStats.movementSpeed));
                                }
                                else
                                {
                                    enemyStats.movementSpeed += amountChange;
                                }
                                break;
                            
                            case ValueToMod.ShieldPoint:
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, enemyStats.shieldPoint, amountChange,
                                        enemyStats.shieldPoint));
                                }
                                else
                                {
                                    enemyStats.shieldPoint += Mathf.FloorToInt(amountChange);
                                }
        
                                break;
                            
                            case ValueToMod.DamageX: //DEALT
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, enemyStats.damageDealt, amountChange,
                                        enemyStats.damageDealt));
                                }
                                else
                                {
                                    enemyStats.damageDealt += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                            case ValueToMod.DamageY: //AOE
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, enemyStats.damageAoe, amountChange,
                                        enemyStats.damageAoe));
                                }
                                else
                                {
                                    enemyStats.damageAoe += Mathf.FloorToInt(amountChange);
                                }
                                break;
                                
                            case ValueToMod.DamageProjectile: //AFTER EXPLOSION
                                if (onTime)
                                {
                                    StartCoroutine(OnTimeEffect(onTimeDuration, enemyStats.damageAoeAfterExplosion, amountChange,
                                        enemyStats.damageAoeAfterExplosion));
                                }
                                else
                                {
                                    enemyStats.damageAoeAfterExplosion += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                        }
                        
                        break;
        
                    }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        somethingOnIt = false;
    }

    IEnumerator OnTimeEffect(float duration, float modValue, float modAmount, float backup)
    {
        modValue += modAmount;
        yield return new WaitForSeconds(duration);
        modValue = backup;
    }
}
