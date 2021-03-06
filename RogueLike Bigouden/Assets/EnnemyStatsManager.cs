using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyStatsManager : MonoBehaviour
{
    #region Ennemy Stats Assignation
    public EnnemyData ennemyData;
    
    private int lifePointSO
    {
        get { return ennemyData.lifePointSO; }
        set { ennemyData.lifePointSO = lifePointSO; }
    } 
    private int shieldPointSO
    {
        get { return ennemyData.shieldPointSO; }
        set { ennemyData.shieldPointSO = shieldPointSO; }
    }
    private int damageSO
    {
        get { return ennemyData.damageSO; }
        set { ennemyData.damageSO = damageSO; }
    }
    private float detectZoneSO
    {
        get { return ennemyData.detectZoneSO; }
        set { ennemyData.detectZoneSO = detectZoneSO; }
    }
    private float attackRangeSO
    {
        get { return ennemyData.attackRangeSO; }
        set { ennemyData.attackRangeSO = attackRangeSO; }
    }
    private float attackDelaySO
    {
        get { return ennemyData.attackDelaySO; }
        set { ennemyData.attackDelaySO = attackDelaySO; }
    }
    private float timeBeforeAggroSO
    {
        get { return ennemyData.timeBeforeAggroSO; }
        set { ennemyData.timeBeforeAggroSO = timeBeforeAggroSO; }
    }
    private float movementSpeedSO
    {
        get { return ennemyData.movementSpeedSO; }
        set { ennemyData.movementSpeedSO = movementSpeedSO; }
    }
    private bool isPlayerInAttackRangeSO
    {
        get { return ennemyData.isPlayerInAttackRangeSO; }
        set { ennemyData.isPlayerInAttackRangeSO = isPlayerInAttackRangeSO; }
    }
    private bool isPlayerInAggroRangeSO
    {
        get { return ennemyData.isPlayerInAggroRangeSO; }
        set { ennemyData.isPlayerInAggroRangeSO = isPlayerInAggroRangeSO; }
    }
    private bool isReadyToShootSO
    {
        get { return ennemyData.isReadyToShootSO; }
        set { ennemyData.isReadyToShootSO = isReadyToShootSO; }
    }
    private bool isAggroSO
    {
        get { return ennemyData.isAggroSO; }
        set { ennemyData.isAggroSO = isAggroSO; }
    }
    private bool isAttackingSO
    {
        get { return ennemyData.isAttackingSO; }
        set { ennemyData.isAttackingSO = isAttackingSO; }
    }
    private bool isStunSO
    {
        get { return ennemyData.isStunSO; }
        set { ennemyData.isStunSO = isStunSO; }
    }
    
    // BARMAN VARIABLES 
    private int damageAoeSO
    {
        get { return ennemyData.damageAoeSO; }
        set { ennemyData.damageAoeSO = damageAoeSO; }
    }
    private int damageAoeAfterExplosionSO
    {
        get { return ennemyData.damageAoeAfterExplosionSO; }
        set { ennemyData.damageAoeAfterExplosionSO = damageAoeAfterExplosionSO; }
    }
    private float rangeAoeSO
    {
        get { return ennemyData.rangeAoeSO; }
        set { ennemyData.rangeAoeSO = rangeAoeSO; }
    }
    
    // SHOOTER VARIABLES 
    private Vector2 bulletSpreadSO
    {
        get { return ennemyData.bulletSpreadSO; }
        set { ennemyData.bulletSpreadSO = bulletSpreadSO; }
    }
    private Vector2 bulletSpeedSO
    {
        get { return ennemyData.bulletSpeedSO; }
        set { ennemyData.bulletSpeedSO = bulletSpeedSO; }
    }
    
    // RUNNER VARIABLE
    private float dashSpeedSO
    {
        get { return ennemyData.dashSpeedSO; }
        set { ennemyData.dashSpeedSO = dashSpeedSO; }
    }
    private float stunDurationSO
    {
        get { return ennemyData.stunDurationSO; }
        set { ennemyData.stunDurationSO = stunDurationSO; }
    }
    private float rushDelaySO
    {
        get { return ennemyData.rushDelaySO; }
        set { ennemyData.rushDelaySO = rushDelaySO; }
    }
    private bool isReadyToDashSO
    {
        get { return ennemyData.isReadyToDashSO; }
        set { ennemyData.isReadyToDashSO = isReadyToDashSO; }
    }
    
    // Common Int
    public int lifePoint;
    public int shieldPoint;
    public int damageDealt;
    
    // Common Float 
    public float detectZone;
    public float attackRange;
    public float attackDelay;
    public float timeBeforeAggro;
     public float movementSpeed;
    
    // Common Bool 
    public bool isPlayerInAggroRange;
    public bool isPlayerInAttackRange;
    public bool isReadyToShoot;
    public bool isAggro;
    public bool isAttacking;
    public bool isStun;
    
    // Barman Variables 
    public int damageAoe;
    public int damageAoeAfterExplosion;
    public float rangeAoe;

    // Shooter Variables 
    public Vector2 bulletSpread;
    public Vector2 bulletSpeed;
    
    // Runner Variables
    public float dashSpeed;
    public float stunDuration;
    public float rushDelay;
    public bool isReadyToDash;
    #endregion

    private void Start()
    {
        lifePoint = lifePointSO;
        shieldPoint = shieldPointSO;
        damageDealt = damageSO;
        detectZone = detectZoneSO;
        attackRange = attackRangeSO;
        attackDelay = attackDelaySO;
        timeBeforeAggro = timeBeforeAggroSO;
        movementSpeed = movementSpeedSO;
        isPlayerInAttackRange = isPlayerInAttackRangeSO;
        isPlayerInAggroRange = isPlayerInAggroRangeSO;
        isReadyToShoot = isReadyToShootSO;
        isAggro = isAggroSO;
        isAttacking = isAttackingSO;
        isStun = isStunSO;
        damageAoe = damageAoeSO;
        damageAoeAfterExplosion = damageAoeAfterExplosionSO;
        rangeAoe = rangeAoeSO;
        bulletSpread = bulletSpreadSO;
        bulletSpeed = bulletSpeedSO;
        dashSpeed = dashSpeedSO;
        stunDuration = stunDurationSO;
        rushDelay = rushDelaySO;
        isReadyToDash = isReadyToDashSO;
    }
    
    #region Ennemy Damage & Heal Gestion 
    public void TakeDamage(int damage)
    {
        if (shieldPoint > 0)
        {
            shieldPoint -= damage;
            if (shieldPoint < 0)
                shieldPoint = 0;
        }
        else
            lifePoint -= damage;

        if (lifePoint <= 0)
            Death();
    }

    private void Death()
    {
        // Play Death Animation
        Destroy(gameObject);
    }

    public void TakeHeal(int heal)
    {
        if (lifePoint !< 1)
        {
            lifePoint += heal;
            // Animation Heal Particles
        }
    }
    #endregion
}
