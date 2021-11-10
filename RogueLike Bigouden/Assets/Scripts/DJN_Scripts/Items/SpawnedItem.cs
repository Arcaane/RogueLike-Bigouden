using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    public Sprite spawnedSprite;

    [Header("Effect")]
    public float amountChange;
    public ValueToMod valueToMod;
    public Target target;
    public Type type;
    
    [Header("Overtime")]
    public bool onTime;
    public float onTimeDuration;
    private bool onTimeStart;
    private float overtimeDurationActual;
    
    private bool onCD;
    public float onCdDuration;
    private float cd;
    
   
    
    public enum ValueToMod{ Health, ShieldPoint, DamageX, DamageY, DamageProjectile, BonusSpeed}
    public enum Target{Player, Enemy}
    public enum Type {Bonus, Malus}

    private PlayerStatsManager playerStats;
    private EnnemyStatsManager enemyStats;

    private void Awake()
    {
        overtimeDurationActual = onTimeDuration;
        cd = onCdDuration;
        
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
            Debug.Log(other.name);
            playerStats = other.GetComponent<PlayerStatsManager>();
        }

        if (other.GetComponent<EnnemyStatsManager>())
        {
            Debug.Log("Enemy on it");
            enemyStats = other.GetComponent<EnnemyStatsManager>();
        }

        ItemObjectEffect();
    }

    private void Update()
    {
        if (onTimeStart)
        {
            overtimeDurationActual -= Time.deltaTime;

            if (overtimeDurationActual <= 0)
            {
                overtimeDurationActual = onTimeDuration;
                onTimeStart = false;
                onCD = true;
                ItemObjectEffect();
            }

            
        }

        if (onCD)
        {
            cd -= Time.deltaTime;

            if (cd <= 0)
            {
                onCD = false;
                cd = onCdDuration;
            }
        }
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
                                    if (!onTimeStart && !onCD)
                                    {
                                        onTimeStart = true;
                                        playerStats.lifePoint += Mathf.FloorToInt(amountChange);

                                    }

                                    if (!onTimeStart && onCD)
                                    {
                                        playerStats.lifePoint = playerStats.playerData.lifePointsSO;
                                    }
                                }
                                else
                                {
                                    playerStats.lifePoint += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                            case ValueToMod.BonusSpeed:
                                if (onTime)
                                {
                                    if (!onTimeStart && !onCD)
                                    {
                                        onTimeStart = true;
                                        playerStats.bonusSpeed += Mathf.FloorToInt(amountChange);

                                    }

                                    if (!onTimeStart && onCD)
                                    {
                                        playerStats.bonusSpeed = playerStats.playerData.bonusSpeedSO;
                                    }
                                }
                                else
                                {
                                    playerStats.bonusSpeed += amountChange;
                                }
                                break;
                            
                            case ValueToMod.ShieldPoint:
                                if (onTime)
                                {

                                    if (!onTimeStart && !onCD)
                                    {
                                        onTimeStart = true;
                                        playerStats.shieldPoint += Mathf.FloorToInt(amountChange);

                                    }

                                    if (!onTimeStart && onCD)
                                    {
                                        playerStats.shieldPoint = playerStats.playerData.shieldPointSO;
                                    }

                                }
                                else
                                {
                                    playerStats.shieldPoint += Mathf.FloorToInt(amountChange);
                                }
        
                                break;
                            
                            case ValueToMod.DamageX:
                                if (onTime)
                                {
                                    if (!onTimeStart && !onCD)
                                    {
                                        onTimeStart = true;
                                        playerStats.damageX += Mathf.FloorToInt(amountChange);

                                    }

                                    if (!onTimeStart && onCD)
                                    {
                                        playerStats.damageX = playerStats.playerData.damageXSO;
                                    }
                                }
                                else
                                {
                                    playerStats.damageX += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                            case ValueToMod.DamageY:
                                if (onTime)
                                {
                                    if (!onTimeStart && !onCD)
                                    {
                                        onTimeStart = true;
                                        playerStats.damageY += Mathf.FloorToInt(amountChange);

                                    }

                                    if (!onTimeStart && onCD)
                                    {
                                        playerStats.damageY = playerStats.playerData.damageYSO;
                                    }
                                }
                                else
                                {
                                    playerStats.damageY += Mathf.FloorToInt(amountChange);
                                }
                                break;
                                
                            case ValueToMod.DamageProjectile:
                                if (onTime)
                                {
                                    if (!onTimeStart && !onCD)
                                    {
                                        onTimeStart = true;
                                        playerStats.damageProjectile += Mathf.FloorToInt(amountChange);

                                    }

                                    if (!onTimeStart && onCD)
                                    {
                                        playerStats.damageProjectile = playerStats.playerData.damageProjectileSO;
                                    }
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
                                }
                                else
                                {
                                    enemyStats.lifePoint += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                            case ValueToMod.BonusSpeed:
                                if (onTime)
                                {
                                }
                                else
                                {
                                    enemyStats.movementSpeed += amountChange;
                                }
                                break;
                            
                            case ValueToMod.ShieldPoint:
                                if (onTime)
                                {
                                }
                                else
                                {
                                    enemyStats.shieldPoint += Mathf.FloorToInt(amountChange);
                                }
        
                                break;
                            
                            case ValueToMod.DamageX: //DEALT
                                if (onTime)
                                {
                                }
                                else
                                {
                                    enemyStats.damageDealt += Mathf.FloorToInt(amountChange);
                                }
                                break;
                            
                            case ValueToMod.DamageY: //AOE
                                if (onTime)
                                {
                                }
                                else
                                {
                                    enemyStats.damageAoe += Mathf.FloorToInt(amountChange);
                                }
                                break;
                                
                            case ValueToMod.DamageProjectile: //AFTER EXPLOSION
                                if (onTime)
                                {
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

    public IEnumerator DestroyObject(float duration)
    {
        yield return new WaitForSeconds(duration);
        onTimeStart = false;
        onCD = true;
        ItemObjectEffect();
        yield return new WaitForSeconds(0.001f);
        Destroy(gameObject);
    }
    



   


    
 
    
    
}
