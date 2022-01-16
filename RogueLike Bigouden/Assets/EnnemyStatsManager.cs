using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

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
    
    // Others
    public GameObject FloatingTextEnnemiPrefab;
    
    #endregion
    
    private void Awake()
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
    [SerializeField] public float counterBeforeReset = 1f;
    
    #region Ennemy Damage & Heal Gestion 
    public void TakeDamage(int damage)
    {
        StartCoroutine(HurtColorTint());
        
        if (shieldPoint > 0)
        {
            shieldPoint -= damage;
            PlayerStatsManager.playerStatsInstance.EarnUltPoint(false);
            if (shieldPoint < 0)
                shieldPoint = 0;
        }
        else
        {
            lifePoint -= damage;
            PlayerStatsManager.playerStatsInstance.EarnUltPoint(false);
        }

        SoundManager.instance.PlaySound("E_hurt");
        ShowFloatingText(lifePoint);
        
        if (lifePoint <= 0)
            Death();
    }
    
    IEnumerator HurtColorTint()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().DOColor(Color.red, 0f);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponentInChildren<SpriteRenderer>().DOColor(Color.white, 0f);
    }

    private void Death()
    {
        if (gameObject.GetComponent<IABarman>())
        {
            Destroy(gameObject.GetComponent<IABarman>().projectile);
        }
        
        PlayerStatsManager.playerStatsInstance.EarnUltPoint(true);
        ScoreManager.instance.AddEnemyKilledScore(1);
        
        if (FindObjectOfType<PaternTimer>())
        {
            PaternTimer.instance.enemyInScene.Remove(gameObject);
        }
        
        Destroy(gameObject);

        int rand = Random.Range(0, 2);
        if (rand == 1)
        {
            int newrand = Random.Range(2, 6);
            PlayerStatsManager.playerStatsInstance.money += newrand;
            UIManager.instance.moneyAnimation.Play("gain");
            ScoreManager.instance.AddMoneyObtained(newrand);
        }
    }

    public void TakeHeal(int heal)
    {
        if (lifePoint !< 1)
        {
            lifePoint += heal;
            // Animation Heal Particles
        }
    }

    public void TakeShield(int shield)
    {
        shieldPoint += shield;
        // Play TakeShield Animation
    }
    
    void ShowFloatingText(int damageToShow)
    {
        var go = Instantiate(FloatingTextEnnemiPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().SetText(damageToShow.ToString());
    }
    
   
    
    #endregion
}
