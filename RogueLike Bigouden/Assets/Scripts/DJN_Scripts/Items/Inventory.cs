using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class Inventory : MonoBehaviour
{
    public List<Items> items;
    public Items itemOnTheFloor;
    public bool conditionActivate;
    public int currentMoney;
    private int moneyCollect;
    public int roll;

    private PlayerStatsManager playerStats;
    private UIManager uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerStats = GetComponent<PlayerStatsManager>();
    }

    private void Update()
    {
        //CheckItemCondition();
    }

    public void CheckItemCondition()
    {
        foreach (Items i in items)
        {
            switch (i.condition)
            {
                case Items.Condition.None:
                    ApplyItemEffect(i);
                    break;

                case Items.Condition.Action:

                    switch (i.action)
                    {
                        case Items.Action.AttackX :
                            if (!playerStats.readyToAttackX)
                            {
                                ApplyItemEffect(i);
                            }
                            break;
                        
                        case Items.Action.AttackY:
                            if (!playerStats.readyToAttackY)
                            {
                                ApplyItemEffect(i);
                            }

                            break;
                        
                        case Items.Action.AttackDistance:
                            if (!playerStats.readyToAttackB)
                            {
                                ApplyItemEffect(i);
                            }

                            break;
                        
                        case Items.Action.AttackUltime:
                            //addAttackUltime
                            break;
                        
                        case Items.Action.Dash:
                            if (!playerStats.readyToDash)
                            {
                                ApplyItemEffect(i);
                            }

                            break;
                        
                        case Items.Action.Death:
                            if (playerStats.lifePoint == 0)
                            {
                                ApplyItemEffect(i);
                            }

                            break;
                        
                        case Items.Action.GetHurt:
                            //addGetHurt
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
                            }

                            break;
                        
                        case Items.Value.Energy:
                            if (playerStats.actualUltPoint == Mathf.FloorToInt(i.conditionValueToReach))
                            {
                                ApplyItemEffect(i);
                            }

                            break;
                        
                        case Items.Value.Money:
                            if (currentMoney == Mathf.FloorToInt(i.conditionValueToReach))
                            {
                                ApplyItemEffect(i);
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
                    }
                    
                    break;
                
            }
        }
    }

    void ApplyItemEffect(Items i)
    {

        roll = UnityEngine.Random.Range(0, 100);

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
                    i.modAmount = Mathf.FloorToInt(currentMoney * i.anotherVariableModPourcentage);
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
                                    float baseDamageX = playerStats.damageX;
                                    float baseDamageY = playerStats.damageY;
                                    float baseDamageB = playerStats.damageProjectile;
                                    playerStats.damageX += i.modAmount;
                                    playerStats.damageY += i.modAmount;
                                    playerStats.damageProjectile += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.damageX = Mathf.FloorToInt(baseDamageX);
                                    playerStats.damageY = Mathf.FloorToInt(baseDamageY);
                                    playerStats.damageProjectile = Mathf.FloorToInt(baseDamageB);
                                }    

                            }
                            else
                            {
                                playerStats.damageX += i.modAmount;
                                playerStats.damageY += i.modAmount;
                                playerStats.damageProjectile += i.modAmount;
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
                                    float baseDamageX = playerStats.damageX;
                                    playerStats.damageX += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.damageX = Mathf.FloorToInt(baseDamageX);
                                }    

                            }
                            else
                            {
                                playerStats.damageX += i.modAmount;
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
                                }    
                                

                            }
                            else
                            {
                                playerStats.damageY += i.modAmount;
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
                                }    

                            }
                            else
                            {
                                playerStats.damageProjectile += i.modAmount;
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
                                }
                            }
                            else
                            {
                                playerStats.attackRangeX += i.modAmount;
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
                                }    

                            }
                            else
                            {
                                playerStats.attackRangeY += i.modAmount;
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
                                }    
                                

                            }
                            else
                            {
                                playerStats.attackRangeProjectile += i.modAmount;
                            }
                            
                        }

                        break;
                    
                    case Items.Augmentation.AttackRangeUlt:
                        //setup ult range;
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
                                }    
                                
                            }
                            else
                            {
                                playerStats.attackCdX += i.modAmount;
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
                                }    
                                
                            }
                            else
                            {
                                playerStats.attackCdY += i.modAmount;
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
                                }    
                                
                            }
                            else
                            {
                                playerStats.attackCdB += i.modAmount;
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
                                    }    
                                    
                                }
                            }
                            else
                            {
                                if (i.onCurrent)
                                {
                                    playerStats.lifePoint += i.modAmount;

                                }
                                else
                                {
                                    playerStats.maxLifePoint += i.modAmount;
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
                                    }    
                                    
                                }
                            }
                            else
                            {
                                if (i.onCurrent)
                                {
                                    playerStats.actualUltPoint += i.modAmount;

                                }
                                else
                                {
                                    playerStats.ultMaxPoint += i.modAmount;
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
                                    float baseDashRange = playerStats.dashRange;
                                    playerStats.dashRange += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    playerStats.dashRange = baseDashRange;
                                }                            
                                
                            }
                            else
                            {
                                playerStats.dashRange += i.modAmount;
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
                                    float baseCMoney = currentMoney;
                                    currentMoney += i.modAmount;
                                    yield return new WaitForSeconds(i.overTimeDuration);
                                    currentMoney = Mathf.FloorToInt(baseCMoney);
                                }
                            }
                            else
                            {
                                currentMoney += i.modAmount;
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
                                }
                                
                            }
                            else
                            {
                                moneyCollect += i.modAmount;
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
                                }
                                
                            }
                            else
                            {
                                playerStats.bonusSpeed += i.modAmount;
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

                                switch(i.spawnPoint)
                                {
                                    case Items.SpawnPoint.Player:
                                        switch (i.playerSpawn)
                                        {
                                            case Items.PlayerSpawn.CurrentPlayer:
                                                objectSpawn.transform.localPosition = gameObject.transform.localPosition;
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
    

}
