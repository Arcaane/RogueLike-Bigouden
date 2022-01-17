using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStatsManager : MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField] PlayerAttribut playerAttribut;
    
    #region Player Variable Assignation

    private string name
    {
        get => playerData.nameSO;
        set => playerData.nameSO = name;
    }

    private string description
    {
        get => playerData.descriptionSO;
        set => playerData.descriptionSO = description;
    }

    private int actualUltPointSO
    {
        get => playerData.actualUltPointSO;
        set => playerData.actualUltPointSO = actualUltPointSO;
    }

    private int ultMaxPointSO
    {
        get => playerData.ultMaxPointSO;
        set => playerData.ultMaxPointSO = ultMaxPointSO;
    }

    private int lifePointSO
    {
        get => playerData.lifePointsSO;
        set => playerData.lifePointsSO = lifePointSO;
    }

    private int dashCounterSO
    {
        get => playerData.dashCounterSO;
        set => playerData.dashCounterSO = dashCounterSO;
    }

    private int shieldPointSO
    {
        get => playerData.shieldPointSO;
        set => playerData.shieldPointSO = shieldPointSO;
    }

    private int damageFirstXSO
    {
        get => playerData.damageFirstXSO;
        set => playerData.damageFirstXSO = damageFirstXSO;
    }

    private int damageSecondXSO
    {
        get => playerData.damageSecondXSO;
        set => playerData.damageSecondXSO = damageSecondXSO;
    }

    private int damageYSO
    {
        get => playerData.damageYSO;
        set => playerData.damageYSO = damageYSO;
    }

    private int damageProjectileSO
    {
        get => playerData.damageProjectileSO;
        set => playerData.damageProjectileSO = damageProjectileSO;
    }

    private int ultPointToAddPerHitSO
    {
        get => playerData.ultPointToAddPerHitSO;
        set => playerData.ultPointToAddPerHitSO = ultPointToAddPerHitSO;
    }

    private int ultPointToAddPerKillSO
    {
        get => playerData.ultPointToAddPerKillSO;
        set => playerData.ultPointToAddPerKillSO = ultPointToAddPerKillSO;
    }

    private int damageUltSO
    {
        get => playerData.damageUltSO;
        set => playerData.damageUltSO = damageUltSO;
    }

    private float movementSpeedSO
    {
        get => playerData.movementSpeedSO;
        set => playerData.movementSpeedSO = movementSpeedSO;
    }

    private float attackRangeXSO
    {
        get => playerData.attackRangeXSO;
        set => playerData.attackRangeXSO = attackRangeXSO;
    }

    private float attackCdXSO
    {
        get => playerData.attackCdXSO;
        set => playerData.attackCdXSO = attackCdXSO;
    }

    private float attackRangeYSO
    {
        get => playerData.attackRangeYSO;
        set => playerData.attackRangeYSO = attackRangeYSO;
    }

    private float attackCdYSO
    {
        get => playerData.attackCdYSO;
        set => playerData.attackCdYSO = attackCdYSO;
    }

    private float attackRangeProjectileSO
    {
        get => playerData.attackRangeProjectileSO;
        set => playerData.attackRangeProjectileSO = attackRangeProjectileSO;
    }

    private float attackCdBSO
    {
        get => playerData.attackCdBSO;
        set => playerData.attackCdBSO = attackCdBSO;
    }

    private float DashSpeedSo
    {
        get { return playerData.dashSpeedSO; }
        set { playerData.dashSpeedSO = DashSpeedSo; }
    }

    private float dashDurationSO
    {
        get { return playerData.dashDurationSO; }
        set { playerData.dashDurationSO = dashDurationSO; }
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

    private bool isAttackFirstXSO
    {
        get { return playerData.isAttackFirstXSO; }
        set { playerData.isAttackFirstXSO = isAttackFirstXSO; }
    }

    private bool isAttackSecondXSO
    {
        get { return playerData.isAttackSecondXSO; }
        set { playerData.isAttackSecondXSO = isAttackSecondXSO; }
    }

    private bool isAttackingXSO
    {
        get { return playerData.isAttackingXSO; }
        set { playerData.isAttackingXSO = isAttackingXSO; }
    }


    private bool readyToAttackYSO
    {
        get { return playerData.readyToAttackYSO; }
        set { playerData.readyToAttackYSO = readyToAttackYSO; }
    }

    private bool isAttackYSO
    {
        get { return playerData.isAttackYSO; }
        set { playerData.isAttackYSO = isAttackYSO; }
    }

    private bool isDeployBSO
    {
        get { return playerData.isDeployBSO; }
        set { playerData.isDeployBSO = isDeployBSO; }
    }

    private bool isAttackBSO
    {
        get { return playerData.isAttackBSO; }
        set { playerData.isAttackBSO = isAttackBSO; }
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
    public int damageFirstX; // Dégats de l'attaque de base
    public int damageSecondX; // Dégats de l'attaque de base
    public int damageY; // Dégats de l'attaque spé
    public int damageProjectile; // Dégats du projectile
    public int ultPointToAddPerHit;
    public int ultPointToAddPerKill;
    public int damageUlt;
    public int money;

    // Floats
    public float movementSpeed;
    public float attackRangeX;
    public float attackCdX;
    public float attackRangeY;
    public float attackCdY;
    public float attackRangeProjectile;
    public float attackCdB;
    public float dashSpeed;
    public float dashCounter;
    public float dashDuration;
    public float dashCooldown;
    public float ultDuration;
    public float bonusSpeed;
    public float invincibilityDuration;
    public float timerInvincibility;

    // Vectors 2
    public Vector2 firstAttackReset;
    public Vector2 secondAttackReset;

    // Bools
    public bool isAttackingX;
    public bool readyToAttackX;
    public bool isAttackFirstX;
    public bool isAttackSecondX;
    public bool readyToAttackY;
    public bool isAttackingY;
    public bool readyToAttackB;
    public bool isAttackB;
    public bool isDeployB;
    public bool isDashing;
    public bool readyToDash;
    public bool onButter;
    public bool getHurt;
    public bool isInvincible;
    public bool loadInvincibilty;
    public bool isR;
    public bool isDead;
    
    // Others
    public GameObject FloatingTextPrefab;
    public GameObject HurtDamagescreen;

    [SerializeField] int lifePointSave;

    #endregion

    public static PlayerStatsManager playerStatsInstance;

    void Awake()
    {
        if (playerStatsInstance != null && playerStatsInstance != this)
            Destroy(gameObject);
        playerStatsInstance = this;
        
        timerInvincibility = invincibilityDuration;
    }


    private void Start()
    {
        ResetPlayerStats();
        playerAttribut = GetComponent<PlayerAttribut>();
    }

    private void Update()
    {
        if (lifePoint > maxLifePoint)
        {
            lifePoint = maxLifePoint;
        }
    }
    
    #region Functions
    IEnumerator HurtColorTint()
    {
        GetComponentInChildren<SpriteRenderer>().DOColor(Color.red, 0f);
        yield return new WaitForSeconds(0.2f);
        GetComponentInChildren<SpriteRenderer>().DOColor(Color.white, 0f);
    }

    public void EarnMoney(int addMoney)
    {
        money += addMoney;
    }


    public void TakeDamage(int damage)
    { 
        if (!isDashing)
        {
            
            if (lifePoint <= 0) 
            {
                if (!isDead)
                {
                    isDead = true;
                    StartCoroutine(Death());
                    isR = false;
                }
            }
            
            if (isR)
            {
                if (lifePoint > 0)
                {
                    UIManager.instance.playerAnimation.Play("hurt");
                    SoundManager.instance.PlaySound("P_hurt");
                    StartCoroutine(HurtColorTint());
            
                    if (shieldPoint > 0)
                    {
                        shieldPoint -= damage;
                
                        if (shieldPoint < 0)
                        {
                            shieldPoint = 0;
                        }
                    }
                    else
                    { 
                        lifePoint -= damage;
                        isR = false;
                        StartCoroutine(GoR());
                    }

                    Debug.Log($"Player took {damage} damage");
                    ShowFloatingText(damage);
                    UIManager.instance.RefreshUI();

                    //loadInvincibilty = true;
                }
            }
        }
    }

    private IEnumerator GoR()
    {
        yield return new WaitForSeconds(0.37f);
        isR = true;
    }

    
    private void FixedUpdate()
    {
        if (getHurt)
        {
            getHurt = false;
        }
    }

    private IEnumerator Death()
    {
        movementSpeed = 0f;
        playerAttribut.animatorPlayer.SetBool("isDead", true);
        ScoreManager.instance.UpdateScore();
        yield return new WaitForSeconds(1.5f);
        ShowDeadPannel();
    }

    private void ShowDeadPannel()
    {
        UIManager.instance.LoadGameOver();
        playerAttribut.animatorPlayer.SetBool("isDead", false);
        Time.timeScale = 0;
    }

    public void TakeShield(int shield)
    {
        shieldPoint += shield;
    }

    public void EarnUltPoint(bool isKill)
    {
        if (!playerAttribut.isUlting || !playerAttribut.isUltingAnim)
        {
            if (isKill)
                actualUltPoint += ultPointToAddPerKill;
            else
                actualUltPoint += ultPointToAddPerHit;
        }
        
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

    void ApplyDamageUIFeedBack()
    {
        if (lifePoint < lifePointSave / 4)
        {
            HurtDamagescreen.SetActive(true);
            HurtDamagescreen.GetComponentInChildren<Image>().material.SetFloat("_Intensity", 1);
            HurtDamagescreen.GetComponentInChildren<Image>().material.SetFloat("_Position", -0.15f);
            if (lifePoint < lifePointSave / 8)
            {
                HurtDamagescreen.GetComponentInChildren<Image>().material.SetFloat("_Intensity", 3);
                HurtDamagescreen.GetComponentInChildren<Image>().material.SetFloat("_Position", -0.14f);
            }
        }
        else
        {
            HurtDamagescreen.GetComponentInChildren<Image>().material.SetFloat("_Intensity", 1);
            HurtDamagescreen.GetComponentInChildren<Image>().material.SetFloat("_Position", -0.15f);
            HurtDamagescreen.SetActive(false);
        }
    }

    public void LateUpdate()
    {
        ApplyDamageUIFeedBack();
    }


    public void ResetPlayerStats()
    {
        // Set int
        actualUltPoint = actualUltPointSO;
        ultMaxPoint = ultMaxPointSO;
        lifePoint = lifePointSO;
        maxLifePoint = lifePoint;
        shieldPoint = shieldPointSO;
        dashCounter = dashCounterSO;
        damageFirstX = damageFirstXSO;
        damageSecondX = damageSecondXSO;
        damageY = damageYSO;
        damageProjectile = damageProjectileSO;
        ultPointToAddPerHit = ultPointToAddPerHitSO;
        ultPointToAddPerKill = ultPointToAddPerKillSO;
        damageUlt = damageUltSO;
        money = 0;

        //Set Float
        movementSpeed = 3.4f;
        attackRangeX = attackRangeXSO;
        attackCdX = attackCdXSO;
        attackRangeY = attackRangeYSO;
        attackCdY = 2f;
        attackRangeProjectile = attackRangeProjectileSO;
        attackCdB = attackCdBSO;
        dashSpeed = DashSpeedSo;
        dashDuration = dashDurationSO;
        dashCooldown = dashCooldownSO;
        ultDuration = ultDurationSO;
        bonusSpeed = bonusSpeedSO;

        // Set Vector
        firstAttackReset = firstAttackResetSO;
        secondAttackReset = secondAttackResetSO;

        // Bools
        isAttackingX = isAttackingXSO;
        readyToAttackX = readyToAttackXSO;
        isAttackFirstX = isAttackFirstXSO;
        isAttackSecondX = isAttackSecondXSO;
        readyToAttackY = true; // Peut utiliser l'attaque Y
        isAttackingY = isAttackYSO; // Peut utiliser l'attaque Y
        readyToAttackB = true; // Peut utiliser l'attaque projectile
        isAttackB = isAttackBSO; // Peut utiliser l'attaque projectile
        isDeployB = isDeployBSO; // Peut utiliser l'attaque projectile
        isDashing = isDashingSO;
        readyToDash = readyToDashSO;
        onButter = onButterSO;
        isR = true;
        isDead = false;


        //Other
        lifePointSave = lifePoint;
        HurtDamagescreen.SetActive(false);
        getHurt = false;
    }

    #endregion
}