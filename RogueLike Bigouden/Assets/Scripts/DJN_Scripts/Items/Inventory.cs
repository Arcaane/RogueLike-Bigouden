using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<Items> items;
    public Items itemOnTheFloor;
    public bool conditionActivate;
    private int moneyCollect;
    public int roll;

    private PlayerStatsManager playerStats;
    private UIManager uiManager;

    private PlayerInput_Final _playerInputFinal;
    private DropSystem _dropSystem;
    private EnnemyStatsManager enemyStats;
    private IABarman ennemyBarman;
    private IAShooter ennemyShooter;
    private IARunner ennemyRunner;
    private IACac ennemyCac;

    private bool isChecking;
    public bool mediKit;
    private int mediKitHeal;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject); // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerStats = GetComponent<PlayerStatsManager>();
        _playerInputFinal = GetComponent<PlayerInput_Final>();
        enemyStats = FindObjectOfType<EnnemyStatsManager>();
        ennemyBarman = FindObjectOfType<IABarman>();
        ennemyShooter = FindObjectOfType<IAShooter>();
        ennemyRunner = FindObjectOfType<IARunner>();
        ennemyCac = FindObjectOfType<IACac>();
        isChecking = false;
    }

    private void Update()
    {
        CheckItemCondition();
    }

    public void CheckItemCondition()
    {
        foreach (Items i in items)
        {
            if (!isChecking)
            {
                switch (i.condition)
                {
                    case Items.Condition.None:
                        ApplyItemEffect(i);
                        items.Remove(i);
                        break;

                    case Items.Condition.Action:

                        switch (i.action)
                        {
                            case Items.Action.AttackX:
                                if (playerStats.isAttackingX)
                                {
                                    ApplyItemEffect(i);
                                    isChecking = true;
                                }

                                break;

                            case Items.Action.AttackY:
                                if (playerStats.isAttackingY)
                                {
                                    ApplyItemEffect(i);
                                    isChecking = true;
                                }

                                break;

                            case Items.Action.AttackDistance:
                                if (playerStats.isAttackB)
                                {
                                    ApplyItemEffect(i);
                                    isChecking = true;
                                }

                                break;

                            case Items.Action.AttackUltime:
                                //addAttackUltime
                                break;

                            case Items.Action.Dash:
                                if (playerStats.isDashing)
                                {
                                    ApplyItemEffect(i);
                                    Debug.Log("Conditon OK");
                                    isChecking = true;
                                }

                                break;

                            case Items.Action.Death:
                                if (playerStats.lifePoint == 0)
                                {
                                    ApplyItemEffect(i);
                                    isChecking = true;
                                }

                                break;

                            case Items.Action.GetHurt:
                                if (playerStats.getHurt)
                                {
                                    ApplyItemEffect(i);
                                    isChecking = true;
                                }

                                break;

                            case Items.Action.KillOrDestroy:
                                switch (i.actionTarget)
                                {
                                    case Items.ActionTarget.None:
                                        break;

                                    case Items.ActionTarget.Player:

                                        switch (i.actionPlayer)
                                        {
                                            case Items.ActionPlayer.CurrentPlayer:
                                                //nah
                                                break;

                                            case Items.ActionPlayer.Everyone:
                                                //nah
                                                break;
                                        }

                                        break;

                                    case Items.ActionTarget.Enemy:
                                        switch (i.actionEnemy)
                                        {
                                            case Items.ActionEnemy.All:
                                                if (enemyStats != null)
                                                {
                                                    if (enemyStats.lifePoint <= 0)
                                                    {
                                                        ApplyItemEffect(i);
                                                        isChecking = true;
                                                    }
                                                }

                                                break;

                                            case Items.ActionEnemy.Barman:
                                                if (ennemyBarman != null)
                                                {
                                                    if (ennemyBarman.GetComponent<EnnemyStatsManager>()
                                                        .lifePoint <= 0)
                                                    {
                                                        ApplyItemEffect(i);
                                                        isChecking = true;
                                                    }
                                                }

                                                break;

                                            case Items.ActionEnemy.Cac:
                                                if (ennemyCac != null)
                                                {
                                                    if (ennemyCac.GetComponent<EnnemyStatsManager>()
                                                        .lifePoint <= 0)
                                                    {
                                                        ApplyItemEffect(i);
                                                        isChecking = true;
                                                    }
                                                }

                                                break;

                                            case Items.ActionEnemy.Rush:
                                                if (ennemyRunner != null)
                                                {
                                                    if (ennemyRunner.GetComponent<EnnemyStatsManager>()
                                                        .lifePoint <= 0)
                                                    {
                                                        ApplyItemEffect(i);
                                                        isChecking = true;
                                                    }
                                                }

                                                break;

                                            case Items.ActionEnemy.Tir:
                                                if (ennemyShooter != null)
                                                {
                                                    if (ennemyShooter.GetComponent<EnnemyStatsManager>()
                                                        .lifePoint <= 0)
                                                    {
                                                        ApplyItemEffect(i);
                                                        isChecking = true;
                                                    }
                                                }
                                                break;
                                        }
                                        break;

                                    case Items.ActionTarget.Props:
                                        FindObjectOfType<ApplyAttack>().mediKit = true;
                                        FindObjectOfType<ApplyAttack>().mediKitHeal = i.modAmount;
                                        break;
                                }

                                break;
                        }

                        break;

                    case Items.Condition.Value:
                        switch (i.value)
                        {
                            case Items.Value.Health:
                                if (playerStats.lifePoint == Mathf.FloorToInt(i.conditionValueToReach))
                                {
                                    ApplyItemEffect(i);
                                    isChecking = true;
                                }

                                break;

                            case Items.Value.Energy:
                                if (playerStats.actualUltPoint == Mathf.FloorToInt(i.conditionValueToReach))
                                {
                                    ApplyItemEffect(i);
                                    isChecking = true;
                                }

                                break;

                            case Items.Value.Money:
                                if (playerStats.money == Mathf.FloorToInt(i.conditionValueToReach))
                                {
                                    ApplyItemEffect(i);
                                    isChecking = true;
                                }

                                break;
                        }

                        break;

                    case Items.Condition.State:

                        switch (i.state)
                        {
                            case Items.State.Alive:

                                switch (i.target)
                                {
                                    case Items.Target.Player:

                                        switch (i.player)
                                        {
                                            case Items.Player.CurrentPlayer:
                                                if (playerStats.lifePoint > 0)
                                                {
                                                    ApplyItemEffect(i);
                                                    isChecking = true;
                                                }

                                                break;

                                            case Items.Player.Everyone:
                                                // foreach player in the game, check if they are all alive
                                                break;
                                        }

                                        break;

                                    case Items.Target.Enemy:

                                        switch (i.enemy)
                                        {
                                            //chercher tout les enemys dans la scene et voir ceux qui sont actifs
                                        }

                                        break;
                                }

                                break;

                            case Items.State.Dead:

                                switch (i.target)
                                {
                                    case Items.Target.Player:

                                        switch (i.player)
                                        {
                                            case Items.Player.CurrentPlayer:
                                                if (playerStats.lifePoint <= 0)
                                                {
                                                    ApplyItemEffect(i);
                                                    isChecking = true;
                                                    
                                                    conditionActivate = false;
                                                }

                                                break;

                                            case Items.Player.Everyone:
                                                // foreach player in the game, check if they are all alive
                                                break;
                                        }

                                        break;

                                    case Items.Target.Enemy:

                                        switch (i.enemy)
                                        {
                                        }

                                        break;
                                }

                                break;
                        }

                        break;
                }
            }
        }
    }

    void ApplyItemEffect(Items i)
    {
        roll = UnityEngine.Random.Range(0, 100);
        conditionActivate = false;
        switch (i._operator)
        {
            case Items.Operator.Add:
                i.modAmount = i.modAmount;
                break;

            case Items.Operator.Multiplie:
                //à voir
                break;

            case Items.Operator.Substract:
                i.modAmount = -i.modAmount;
                break;
        }

        if (i.modIsAnotherVariable)
        {
            switch (i.variableTarget)
            {
                case Items.VariableTarget.Money:
                    i.modAmount = Mathf.FloorToInt(playerStats.money * i.anotherVariableModPourcentage);
                    break;

                case Items.VariableTarget.Health:
                    i.modAmount = Mathf.FloorToInt(playerStats.lifePoint * i.anotherVariableModPourcentage);
                    break;

                case Items.VariableTarget.Energy:
                    i.modAmount = Mathf.FloorToInt(playerStats.actualUltPoint * i.anotherVariableModPourcentage);
                    break;
            }
        }

        switch (i.effectOn)
        {
            case Items.Effect.Variable:

                switch (i.augmentation)
                {
                    case Items.Augmentation.Damage:

                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseDamageX = playerStats.damageFirstX;
                                    float baseDamageY = playerStats.damageY;
                                    float baseDamageB = playerStats.damageProjectile;

                                    playerStats.damageFirstX += i.modAmount;
                                    playerStats.damageY += i.modAmount;
                                    playerStats.damageProjectile += i.modAmount;

                                    yield return new WaitForSeconds(i.overTimeDuration);

                                    playerStats.damageFirstX = Mathf.FloorToInt(baseDamageX);
                                    playerStats.damageY = Mathf.FloorToInt(baseDamageY);
                                    playerStats.damageProjectile = Mathf.FloorToInt(baseDamageB);
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.damageFirstX += i.modAmount;
                                playerStats.damageY += i.modAmount;
                                playerStats.damageProjectile += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.DamageX:

                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseDamageX = playerStats.damageFirstX;
                                    playerStats.damageFirstX += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.damageFirstX = Mathf.FloorToInt(baseDamageX);
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.damageFirstX += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.DamageY:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseDamageY = playerStats.damageY;
                                    playerStats.damageY += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.damageY = Mathf.FloorToInt(baseDamageY);
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.damageY += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.DamageB:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseDamageB = playerStats.damageProjectile;
                                    playerStats.damageProjectile += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.damageProjectile = Mathf.FloorToInt(baseDamageB);
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.damageProjectile += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.DamageUlt:
                        //setup damage ultime
                        break;

                    case Items.Augmentation.AttackRangeX:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseAttackRangeX = playerStats.attackRangeX;
                                    playerStats.attackRangeX += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.attackRangeX = baseAttackRangeX;
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.attackRangeX += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.AttackRangeY:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseAttackRangeY = playerStats.attackRangeY;
                                    playerStats.attackRangeY += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.attackRangeY = baseAttackRangeY;
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.attackRangeY += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.AttackRangeB:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseAttackRangeB = playerStats.attackRangeProjectile;
                                    playerStats.attackRangeProjectile += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.attackRangeProjectile = baseAttackRangeB;
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.attackRangeProjectile += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.AttackRangeUlt:
                        //setup ult range;
                        break;

                    case Items.Augmentation.AttackSpeed:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseAttackSpeedX = playerStats.attackCdX;
                                    float baseAttackSpeedY = playerStats.attackCdY;
                                    float baseAttackSpeedB = playerStats.attackCdB;

                                    playerStats.attackCdX += i.modAmount;
                                    playerStats.attackCdY += i.modAmount;
                                    playerStats.attackCdB += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.attackCdX = baseAttackSpeedX;
                                    playerStats.attackCdY = baseAttackSpeedY;
                                    playerStats.attackCdB = baseAttackSpeedB;
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.attackCdX += i.modAmount;
                                playerStats.attackCdY += i.modAmount;
                                playerStats.attackCdB += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.AttackSpeedX:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseAttackSpeedX = playerStats.attackCdX;
                                    playerStats.attackCdX += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.attackCdX = baseAttackSpeedX;
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.attackCdX += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.AttackSpeedY:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseAttackSpeedY = playerStats.attackCdY;
                                    playerStats.attackCdY += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.attackCdY = baseAttackSpeedY;
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.attackCdY += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.AttackSpeedB:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseAttackSpeedB = playerStats.attackCdB;
                                    playerStats.attackCdB += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.attackCdB = baseAttackSpeedB;
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.attackCdB += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.AttackSpeedUlt:
                        //setup speed ult
                        break;

                    case Items.Augmentation.Health:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                if (i.onCurrent)
                                {
                                    StartCoroutine(OnTimeEffect());

                                    IEnumerator OnTimeEffect()
                                    {
                                        float baseActualLifePoint = playerStats.lifePoint;
                                        playerStats.lifePoint += i.modAmount;
                                        yield return new WaitForSeconds(i.overTimeDuration);
                                        playerStats.lifePoint = Mathf.FloorToInt(baseActualLifePoint);
                                        isChecking = false;
                                    }
                                }
                                else
                                {
                                    StartCoroutine(OnTimeEffect());

                                    IEnumerator OnTimeEffect()
                                    {
                                        float baseMaxLifePoint = playerStats.maxLifePoint;
                                        playerStats.maxLifePoint += i.modAmount;
                                        yield return new WaitForSeconds(i.overTimeDuration);
                                        playerStats.maxLifePoint = Mathf.FloorToInt(baseMaxLifePoint);
                                        isChecking = false;
                                    }
                                }
                            }
                            else
                            {
                                if (i.onCurrent)
                                {
                                    playerStats.lifePoint += i.modAmount;
                                    isChecking = false;
                                }
                                else
                                {
                                    playerStats.maxLifePoint += i.modAmount;
                                    isChecking = false;
                                }
                            }
                        }

                        break;

                    case Items.Augmentation.Energy:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                if (i.onCurrent)
                                {
                                    StartCoroutine(OnTimeEffect());

                                    IEnumerator OnTimeEffect()
                                    {
                                        float baseActualUltPoint = playerStats.actualUltPoint;
                                        playerStats.actualUltPoint += i.modAmount;
                                        yield return new WaitForSeconds(i.overTimeDuration);
                                        playerStats.actualUltPoint = Mathf.FloorToInt(baseActualUltPoint);
                                        isChecking = false;
                                    }
                                }
                                else
                                {
                                    StartCoroutine(OnTimeEffect());

                                    IEnumerator OnTimeEffect()
                                    {
                                        float baseMaxUltPoint = playerStats.ultMaxPoint;
                                        playerStats.ultMaxPoint += i.modAmount;
                                        yield return new WaitForSeconds(i.overTimeDuration);
                                        playerStats.ultMaxPoint = Mathf.FloorToInt(baseMaxUltPoint);
                                        isChecking = false;
                                    }
                                }
                            }
                            else
                            {
                                if (i.onCurrent)
                                {
                                    playerStats.actualUltPoint += i.modAmount;
                                    isChecking = false;
                                }
                                else
                                {
                                    playerStats.ultMaxPoint += i.modAmount;
                                    isChecking = false;
                                }
                            }
                        }

                        break;

                    case Items.Augmentation.DashRange:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseDashRange = playerStats.dashSpeed;
                                    playerStats.dashSpeed += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.dashSpeed = baseDashRange;
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.dashSpeed += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.AddMoney:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseCMoney = playerStats.money;
                                    playerStats.money += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.money = Mathf.FloorToInt(baseCMoney);
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.money += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.MoreMoney:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseMoney = moneyCollect;
                                    moneyCollect += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    moneyCollect = Mathf.FloorToInt(baseMoney);
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                moneyCollect += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.Speed:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseBSpeed = playerStats.bonusSpeed;
                                    playerStats.bonusSpeed += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.bonusSpeed = baseBSpeed;
                                    isChecking = false;
                                }
                            }
                            else
                            {
                                playerStats.bonusSpeed += i.modAmount;
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Augmentation.Shield:
                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(OnTimeEffect());

                                IEnumerator OnTimeEffect()
                                {
                                    float baseShield = playerStats.shieldPoint;
                                    Debug.Log(baseShield);
                                    playerStats.shieldPoint += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.shieldPoint = Mathf.FloorToInt(baseShield);
                                    isChecking = false;
                                }
                            }
                        }

                        break;
                }

                break;

            case Items.Effect.Object:
                switch (i.alteration)
                {
                    case Items.Alteration.Creation:

                        if (roll <= i.rate)
                        {
                            for (int j = 0; j < i.spawnAmount; j++)
                            {
                                GameObject objectSpawn = Instantiate(i.objectPrefab);

                                switch (i.spawnPoint)
                                {
                                    case Items.SpawnPoint.Player:
                                        switch (i.playerSpawn)
                                        {
                                            case Items.PlayerSpawn.CurrentPlayer:
                                                objectSpawn.transform.localPosition =
                                                    gameObject.transform.localPosition;
                                                break;
                                        }

                                        break;

                                    case Items.SpawnPoint.Enemy:
                                        switch (i.enemySpawn)
                                        {
                                            case Items.EnemySpawn.Target:
                                                //objectSpawn.transform.localPosition = lastEnemyHit;
                                                break;
                                            case Items.EnemySpawn.All:
                                                //objectSpawn.transform.localPosition =  enemyTargetCondition;
                                                break;
                                        }

                                        break;
                                }

                                objectSpawn.transform.localPosition = i.specialSpawnPoint.transform.localPosition;
                                StartCoroutine(DelayToDestroy(i.spawnTime, objectSpawn));
                                isChecking = false;
                            }
                        }

                        break;

                    case Items.Alteration.Destruction:
                        break;
                }

                break;
        }

        uiManager.RefreshUI();
    }

    IEnumerator DelayToDestroy(float duration, GameObject spawnObject)
    {
        Debug.Log("This Object will be destroy in" + duration + " seconds");
        yield return new WaitForSeconds(duration);
        Destroy(spawnObject);
        Debug.Log(spawnObject + " have been destroyed");
    }

    public void MediKit()
    {
        
    }
}