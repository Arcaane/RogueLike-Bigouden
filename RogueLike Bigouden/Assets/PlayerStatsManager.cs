using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public PlayerData playerData;

    #region Player Variable Assignation

    private string name
    {
        get { return playerData.nameSO; }
        set { playerData.nameSO = name; }
    }

    private string description
    {
        get { return playerData.descriptionSO; }
        set { playerData.descriptionSO = description; }
    }

    private int actualUltPointSO
    {
        get { return playerData.actualUltPointSO; }
        set { playerData.actualUltPointSO = actualUltPointSO; }
    }

    private int ultMaxPointSO
    {
        get { return playerData.ultMaxPointSO; }
        set { playerData.ultMaxPointSO = ultMaxPointSO; }
    }

    private int lifePointSO
    {
        get { return playerData.lifePointsSO; }
        set { playerData.lifePointsSO = lifePointSO; }
    }

    private int shieldPointSO
    {
        get { return playerData.shieldPointSO; }
        set { playerData.shieldPointSO = shieldPointSO; }
    }

    private int damageXSO
    {
        get { return playerData.damageXSO; }
        set { playerData.damageXSO = damageXSO; }
    }

    private int damageYSO
    {
        get { return playerData.damageYSO; }
        set { playerData.damageYSO = damageYSO; }
    }

    private int damageProjectileSO
    {
        get { return playerData.damageProjectileSO; }
        set { playerData.damageProjectileSO = damageProjectileSO; }
    }

    private int ultPointToAddPerHitSO
    {
        get { return playerData.ultPointToAddPerHitSO; }
        set { playerData.ultPointToAddPerHitSO = ultPointToAddPerHitSO; }
    }

    private int ultPointToAddPerKillSO
    {
        get { return playerData.ultPointToAddPerKillSO; }
        set { playerData.ultPointToAddPerKillSO = ultPointToAddPerKillSO; }
    }

    private int damageUltSO
    {
        get { return playerData.damageUltSO; }
        set { playerData.damageUltSO = damageUltSO; }
    }

    private float movementSpeedSO
    {
        get { return playerData.movementSpeedSO; }
        set { playerData.movementSpeedSO = movementSpeedSO; }
    }

    private float attackRangeXSO
    {
        get { return playerData.attackRangeXSO; }
        set { playerData.attackRangeXSO = attackRangeXSO; }
    }

    private float attackCdXSO
    {
        get { return playerData.attackCdXSO; }
        set { playerData.attackCdXSO = attackCdXSO; }
    }

    private float attackRangeYSO
    {
        get { return playerData.attackRangeYSO; }
        set { playerData.attackRangeYSO = attackRangeYSO; }
    }

    private float attackCdYSO
    {
        get { return playerData.attackCdYSO; }
        set { playerData.attackCdYSO = attackCdYSO; }
    }

    private float attackRangeProjectileSO
    {
        get { return playerData.attackRangeProjectileSO; }
        set { playerData.attackRangeProjectileSO = attackRangeProjectileSO; }
    }

    private float attackCdBSO
    {
        get { return playerData.attackCdBSO; }
        set { playerData.attackCdBSO = attackCdBSO; }
    }

    private float dashRangeSO
    {
        get { return playerData.dashRangeSO; }
        set { playerData.dashRangeSO = dashRangeSO; }
    }

    private float dashCooldownSO
    {
        get { return playerData.dashCooldownSO; }
        set { playerData.dashCooldownSO = dashCooldownSO; }
    }

    private float ultDurationSO
    {
        get { return playerData.ultDurationSO; }
        set { playerData.ultDurationSO = ultDurationSO; }
    }

    private float bonusSpeedSO
    {
        get { return playerData.bonusSpeedSO; }
        set { playerData.bonusSpeedSO = bonusSpeedSO; }
    }

    private Vector2 firstAttackResetSO
    {
        get { return playerData.firstAttackResetSO; }
        set { playerData.firstAttackResetSO = firstAttackResetSO; }
    }

    private Vector2 secondAttackResetSO
    {
        get { return playerData.secondAttackResetSO; }
        set { playerData.secondAttackResetSO = secondAttackResetSO; }
    }

    private bool readyToAttackXSO
    {
        get { return playerData.readyToAttackXSO; }
        set { playerData.readyToAttackXSO = readyToAttackXSO; }
    }

    private bool readyToAttackYSO
    {
        get { return playerData.readyToAttackYSO; }
        set { playerData.readyToAttackYSO = readyToAttackYSO; }
    }

    private bool readyToAttackBSO
    {
        get { return playerData.readyToAttackBSO; }
        set { playerData.readyToAttackBSO = readyToAttackBSO; }
    }

    private bool isDashingSO
    {
        get { return playerData.isDashingSO; }
        set { playerData.isDashingSO = isDashingSO; }
    }

    private bool readyToDashSO
    {
        get { return playerData.readyToDashSO; }
        set { playerData.readyToDashSO = readyToDashSO; }
    }

    private bool onButterSO
    {
        get { return playerData.onButterSO; }
        set { playerData.onButterSO = onButterSO; }
    }

    //Ints 
    public int maxLifePoint;
    public int actualUltPoint; // Point d'ultimate collecté par le joueur
    public int ultMaxPoint; // Point d'ultime pour lancer l'ult
    public int lifePoint; // Point de vie du joueur
    public int shieldPoint; // Point de bouclier de l'ennemis
    public int damageX; // Dégats de l'attaque de base
    public int damageY; // Dégats de l'attaque spé
    public int damageProjectile; // Dégats du projectile
    public int ultPointToAddPerHit;
    public int ultPointToAddPerKill;
    public int damageUlt;

    // Floats
    public float movementSpeed;
    public float attackRangeX;
    public float attackCdX;
    public float attackRangeY;
    public float attackCdY;
    public float attackRangeProjectile;
    public float attackCdB;
    public float dashRange;
    public float dashCooldown;
    public float ultDuration;
    public float bonusSpeed;

    // Vectors 2
    public Vector2 firstAttackReset;
    public Vector2 secondAttackReset;

    // Bools
    public bool readyToAttackX;
    public bool readyToAttackY;
    public bool readyToAttackB;
    public bool isDashing;
    public bool readyToDash;
    public bool onButter;
    
    // Others
    public GameObject FloatingTextPrefab;
    
    #endregion

    public static PlayerStatsManager playerStatsInstance;

    void Awake()
    {
        if (playerStatsInstance != null && playerStatsInstance != this)
            Destroy(gameObject);

        playerStatsInstance = this;
    }


    private void Start()
    {
        // Set int
        actualUltPoint = actualUltPointSO;
        ultMaxPoint = ultMaxPointSO;
        lifePoint = lifePointSO;
        maxLifePoint = lifePoint;
        shieldPoint = shieldPointSO;
        damageX = damageXSO;
        damageY = damageYSO;
        damageProjectile = damageProjectileSO;
        ultPointToAddPerHit = ultPointToAddPerHitSO;
        ultPointToAddPerKill = ultPointToAddPerKillSO;
        damageUlt = damageUltSO;

        //Set Float
        movementSpeed = movementSpeedSO;
        attackRangeX = attackCdXSO;
        attackCdX = attackCdXSO;
        attackRangeY = attackRangeYSO;
        attackCdY = attackCdYSO;
        attackRangeProjectile = attackRangeProjectileSO;
        attackCdB = attackCdBSO;
        dashRange = dashRangeSO;
        dashCooldown = dashCooldownSO;
        ultDuration = ultDurationSO;
        bonusSpeed = bonusSpeedSO;

        // Set Vector
        firstAttackReset = firstAttackResetSO;
        secondAttackReset = secondAttackResetSO;

        // Bools
        readyToAttackX = readyToAttackXSO;
        readyToAttackY = readyToAttackYSO; // Peut utiliser l'attaque Y
        readyToAttackB = readyToAttackBSO; // Peut utiliser l'attaque projectile
        isDashing = isDashingSO;
        readyToDash = readyToDashSO;
        onButter = onButterSO;
    }

    #region Functions

    public void TakeDamage(int damage)
    {
        //if (!isDashing)
        //{
            if (shieldPoint > 0)
            {
                shieldPoint -= damage;
                if (shieldPoint < 0)
                    shieldPoint = 0;
            }
            else
                lifePoint -= damage;

            Debug.Log("Player took " + damage + " damage");
            ShowFloatingText(damage);
            UIManager.instance.RefreshUI();
            
        
            if (lifePoint <= 0)
                Death();
        //}
    }

    private void Death()
    {
        // Play Death Animation
        Debug.Log(gameObject.name + " is Dead !");
    }

    public void TakeShield(int shield)
    {
        shieldPoint += shield;
        // Play TakeShield Animation
    }

    public void EarnUltPoint(bool isKill)
    {
        if (isKill)
            actualUltPoint += ultPointToAddPerKill;
        else
            actualUltPoint += ultPointToAddPerHit;

        if (actualUltPoint > 100)
        {
            actualUltPoint = 100;
        }

        UIManager.instance.RefreshUI();
        Debug.Log("Actual ult point : " + actualUltPoint);
    }


    void ShowFloatingText(int damageToShow)
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMeshPro>().SetText(damageToShow.ToString());
    }
    
    #endregion
}