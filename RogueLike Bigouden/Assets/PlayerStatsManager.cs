using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public PlayerData playerData;

    #region Player Variable Assignation
    [SerializeField] private string name
    {
        get { return playerData.nameSO; }
        set { playerData.nameSO = name; }
    }
    [SerializeField] private string description
    {
        get { return playerData.descriptionSO; }
        set { playerData.descriptionSO = description; }
    }
    [SerializeField] private int attackSpeed
    {
        get { return playerData.attackSpeedSO; }
        set { playerData.attackSpeedSO = attackSpeed; }
    }
    [SerializeField] private int actualUltPoint
    {
        get { return playerData.actualUltPointSO; }
        set { playerData.actualUltPointSO = actualUltPoint; }
    }
    [SerializeField] private int ultMaxPoint
    {
        get { return playerData.ultMaxPointSO; }
        set { playerData.ultMaxPointSO = ultMaxPoint; }
    }
    [SerializeField] private int lifePoints
    {
        get { return playerData.lifePointsSO; }
        set { playerData.lifePointsSO = lifePoints; }
    }
    [SerializeField] private int shieldPoint
    {
        get { return playerData.shieldPointSO; }
        set { playerData.shieldPointSO = shieldPoint; }
    }
    [SerializeField] private int damageX
    {
        get { return playerData.damageXSO; }
        set { playerData.damageXSO = damageX; }
    }
    [SerializeField] private int damageY
    {
        get { return playerData.damageYSO; }
        set { playerData.damageYSO = damageY; }
    }
    [SerializeField] private int damageProjectile
    {
        get { return playerData.damageProjectileSO; }
        set { playerData.damageProjectileSO = damageProjectile; }
    }
    [SerializeField] private float movementSpeed
    {
        get { return playerData.movementSpeedSO; }
        set { playerData.movementSpeedSO = movementSpeed; }
    }
    
    [SerializeField] private float attackRangeX
    {
        get { return playerData.attackRangeXSO; }
        set { playerData.attackRangeXSO = attackRangeX; }
    }
    
    [SerializeField] private float attackCdX
    {
        get { return playerData.attackCdXSO; }
        set { playerData.attackCdXSO = attackCdX; }
    }
    
    [SerializeField] private float attackRangeY
    {
        get { return playerData.attackRangeYSO; }
        set { playerData.attackRangeYSO = attackRangeY; }
    }
    
    [SerializeField] private float attackCdY
    {
        get { return playerData.attackCdYSO; }
        set { playerData.attackCdYSO = attackCdY; }
    }
    
    [SerializeField] private float attackRangeProjectile
    {
        get { return playerData.attackRangeProjectileSO; }
        set { playerData.attackRangeProjectileSO = attackRangeProjectile; }
    }
    [SerializeField] private float attackCdB
    {
        get { return playerData.attackCdBSO; }
        set { playerData.attackCdBSO = attackCdB; }
    }
    [SerializeField] private float dashRange
    {
        get { return playerData.dashRangeSO; }
        set { playerData.dashRangeSO = dashRange; }
    }
    [SerializeField] private float dashCooldown
    {
        get { return playerData.dashCooldownSO; }
        set { playerData.dashCooldownSO = dashCooldown; }
    }
    [SerializeField] private float ultDuration
    {
        get { return playerData.ultDurationSO; }
        set { playerData.ultDurationSO = ultDuration; }
    }
    [SerializeField] private float bonusSpeed
    {
        get { return playerData.bonusSpeedSO; }
        set { playerData.bonusSpeedSO = bonusSpeed; }
    }
    [SerializeField] private Vector2 firstAttackReset
    {
        get { return playerData.firstAttackResetSO; }
        set { playerData.firstAttackResetSO = firstAttackReset; }
    }
    [SerializeField] private Vector2 secondAttackReset
    {
        get { return playerData.secondAttackResetSO; }
        set { playerData.secondAttackResetSO = secondAttackReset; }
    }
    [SerializeField] private bool readyToAttackX
    {
        get { return playerData.readyToAttackXSO; }
        set { playerData.readyToAttackXSO = readyToAttackX; }
    }
    [SerializeField] private bool readyToAttackY
    {
        get { return playerData.readyToAttackYSO; }
        set { playerData.readyToAttackYSO = readyToAttackY; }
    }
    [SerializeField] private bool readyToAttackB
    {
        get { return playerData.readyToAttackBSO; }
        set { playerData.readyToAttackBSO = readyToAttackB; }
    }
    [SerializeField] private bool isDashing
    {
        get { return playerData.isDashingSO; }
        set { playerData.isDashingSO = isDashing; }
    }
    [SerializeField] private bool readyToDash
    {
        get { return playerData.readyToDashSO; }
        set { playerData.readyToDashSO = readyToDash; }
    }
    [SerializeField] private bool onButter
    {
        get { return playerData.onButterSO; }
        set { playerData.onButterSO = onButter; }
    }
    #endregion
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
