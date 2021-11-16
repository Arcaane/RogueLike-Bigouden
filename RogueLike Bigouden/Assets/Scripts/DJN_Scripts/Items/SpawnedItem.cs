using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedItem : MonoBehaviour
{
    public Sprite spawnedSprite;

    [Header("Effect")]
    public float modAmount;
    public ValueToMod valueToMod;
    public Target target;
    public Operator _operator;
    
    [Header("Overtime")]
    public bool onTime;
    public float onTimeDuration;

    private bool onTrigger;

    public enum ValueToMod{ Health, ShieldPoint, DamageX, DamageY, DamageProjectile, BonusSpeed}
    public enum Target{Player, Enemy}
    //player target valueToMod enum
    //enemy target valueToMod enum
    
    public enum Operator{Add, Substract, Multiplie}

    private PlayerStatsManager playerStats;
    private EnnemyStatsManager enemyStats;

    private void Awake()
    {

        switch (_operator)
        {
            case Operator.Add:
                modAmount = modAmount;
                break;
            
            case Operator.Substract:
                modAmount = -modAmount;
                break;
            
            case Operator.Multiplie:
                //à voir
                break;
        }
    }

    private void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = spawnedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerStatsManager>())
        {
            Debug.Log("Player on it");
            Debug.Log(other.name);
            playerStats = other.GetComponent<PlayerStatsManager>();
            onTrigger = true;
        }

        if (other.GetComponent<EnnemyStatsManager>())
        {
            Debug.Log("Enemy on it");
            enemyStats = other.GetComponent<EnnemyStatsManager>();
            onTrigger = true;
        }

        ItemObjectEffect();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerStatsManager>())
        {
            onTrigger = false;
        }

        if (other.GetComponent<EnnemyStatsManager>())
        {
            onTrigger = false;
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
                               
                                float currentHealth = playerStats.lifePoint;
                                if (!onTrigger)
                                {
                                    playerStats.lifePoint = Mathf.FloorToInt(currentHealth);
                                }
                                
                                if (onTime)
                                {
                                    
                                    StartCoroutine(OnTimeEffect());
                                    
                                    IEnumerator OnTimeEffect()
                                    {
                                        playerStats.lifePoint += Mathf.FloorToInt(modAmount);
                                        yield return new WaitForSeconds(onTimeDuration);
                                        playerStats.lifePoint = Mathf.FloorToInt(currentHealth);
                                    }
                                
                                }
                                else
                                {
                                    playerStats.lifePoint += Mathf.FloorToInt(modAmount);
                                }
                                break;
                            
                            case ValueToMod.BonusSpeed:
                                
                                float currentBonusSpeed = playerStats.bonusSpeed;
                                if (!onTrigger)
                                {
                                    playerStats.bonusSpeed = Mathf.FloorToInt(currentBonusSpeed);
                                }
                                if (onTime)
                                {

                                    StartCoroutine(OnTimeEffect());
                                    
                                    IEnumerator OnTimeEffect()
                                    {
                                        playerStats.bonusSpeed += Mathf.FloorToInt(modAmount);
                                        yield return new WaitForSeconds(onTimeDuration);
                                        playerStats.bonusSpeed = Mathf.FloorToInt(currentBonusSpeed);
                                    }
                                }
                                else
                                {
                                    playerStats.bonusSpeed += modAmount;
                                }
                                break;
                            
                            case ValueToMod.ShieldPoint:
                               
                                float currentShieldPoint = playerStats.shieldPoint;
                                if (!onTrigger)
                                {
                                    playerStats.shieldPoint = Mathf.FloorToInt(currentShieldPoint);
                                }
                                
                                if (onTime)
                                {

                                    StartCoroutine(OnTimeEffect());
                                    
                                    IEnumerator OnTimeEffect()
                                    {
                                        playerStats.shieldPoint += Mathf.FloorToInt(modAmount);
                                        yield return new WaitForSeconds(onTimeDuration);
                                        playerStats.shieldPoint = Mathf.FloorToInt(currentShieldPoint);
                                    }
                                

                                }
                                else
                                {
                                    playerStats.shieldPoint += Mathf.FloorToInt(modAmount);
                                }
        
                                break;
                            
                            case ValueToMod.DamageX:
                                float currentDamageX = playerStats.damageX;
                                if (!onTrigger)
                                {
                                    playerStats.damageX = Mathf.FloorToInt(currentDamageX);
                                }
                                
                                if (onTime)
                                {

                                    StartCoroutine(OnTimeEffect());
                                    
                                    IEnumerator OnTimeEffect()
                                    {
                                        playerStats.damageX += Mathf.FloorToInt(modAmount);
                                        yield return new WaitForSeconds(onTimeDuration);
                                        playerStats.damageX = Mathf.FloorToInt(currentDamageX);
                                    }
                                }
                                else
                                {
                                    playerStats.damageX += Mathf.FloorToInt(modAmount);
                                }
                                break;
                            
                            case ValueToMod.DamageY:
                                
                                float currentDamageY = playerStats.damageY;

                                if (!onTrigger)
                                {
                                    playerStats.damageY = Mathf.FloorToInt(currentDamageY);
                                }
                                
                                if (onTime)
                                {

                                    StartCoroutine(OnTimeEffect());
                                    
                                    IEnumerator OnTimeEffect()
                                    {
                                        playerStats.damageY += Mathf.FloorToInt(modAmount);
                                        yield return new WaitForSeconds(onTimeDuration);
                                        playerStats.damageY = Mathf.FloorToInt(currentDamageY);
                                    }
                                }
                                else
                                {
                                    playerStats.damageY += Mathf.FloorToInt(modAmount);
                                }
                                break;
                                
                            case ValueToMod.DamageProjectile:
                                float currentDamageB = playerStats.damageProjectile;
                                if (!onTrigger)
                                {
                                    playerStats.damageProjectile = Mathf.FloorToInt(currentDamageB);
                                }
                                
                                if (onTime)
                                {

                                    StartCoroutine(OnTimeEffect());
                                    
                                    IEnumerator OnTimeEffect()
                                    {
                                        playerStats.damageProjectile += Mathf.FloorToInt(modAmount);
                                        yield return new WaitForSeconds(onTimeDuration);
                                        playerStats.damageProjectile = Mathf.FloorToInt(currentDamageB);
                                    }
                                }
                                else
                                {
                                    playerStats.damageProjectile += Mathf.FloorToInt(modAmount);
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
                                    enemyStats.lifePoint += Mathf.FloorToInt(modAmount);
                                }
                                break;
                            
                            case ValueToMod.BonusSpeed:
                                if (onTime)
                                {
                                }
                                else
                                {
                                    enemyStats.movementSpeed += modAmount;
                                }
                                break;
                            
                            case ValueToMod.ShieldPoint:
                                if (onTime)
                                {
                                }
                                else
                                {
                                    enemyStats.shieldPoint += Mathf.FloorToInt(modAmount);
                                }
        
                                break;
                            
                            case ValueToMod.DamageX: //DEALT
                                if (onTime)
                                {
                                }
                                else
                                {
                                    enemyStats.damageDealt += Mathf.FloorToInt(modAmount);
                                }
                                break;
                            
                            case ValueToMod.DamageY: //AOE
                                if (onTime)
                                {
                                }
                                else
                                {
                                    enemyStats.damageAoe += Mathf.FloorToInt(modAmount);
                                }
                                break;
                                
                            case ValueToMod.DamageProjectile: //AFTER EXPLOSION
                                if (onTime)
                                {
                                }
                                else
                                {
                                    enemyStats.damageAoeAfterExplosion += Mathf.FloorToInt(modAmount);
                                }
                                break;
                            
                        }
                        
                        break;
        
                    }
    }

    public IEnumerator DestroyObject(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    



   


    
 
    
    
}
