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

    private bool onTime;
    private float onTimeDuration;
    private bool onTimeStart;
    private float overtimeDurationActual;
    private bool onCD;
    public float onCdDuration;
    private float cd;
    
    
    private PlayerStatsManager playerStats;
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<UIManager>();
        playerStats = GetComponent<PlayerStatsManager>();
    }

    private void Update()
    {
     

        
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
        
        switch (i.effectOn)
        {
            case Items.Effect.Variable:

                switch (i.augmentation)
                {
                    case Items.Augmentation.DamageX:

                        if (roll <= i.rate)
                        {
                            if (i.overTime)
                            {
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.damageX, i.modAmount, playerStats.damageX));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.damageY, i.modAmount, playerStats.damageY));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.damageProjectile, i.modAmount, playerStats.damageProjectile));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.attackRangeX, i.modAmount, playerStats.attackRangeX));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.attackRangeY, i.modAmount, playerStats.attackRangeY));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.attackRangeProjectile, i.modAmount, playerStats.attackRangeProjectile));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.attackCdX, i.modAmount, playerStats.attackCdX));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.attackCdY, i.modAmount, playerStats.attackCdY));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.attackCdB, i.modAmount, playerStats.attackCdB));

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
                                    StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.lifePoint, i.modAmount, playerStats.lifePoint));

                                }
                                else
                                {
                                    StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.maxLifePoint, i.modAmount, playerStats.maxLifePoint));

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
                                    StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.actualUltPoint, i.modAmount, playerStats.actualUltPoint));

                                }
                                else
                                {
                                    StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.ultMaxPoint, i.modAmount, playerStats.ultMaxPoint));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.dashRange, i.modAmount, playerStats.dashRange));
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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, currentMoney, i.modAmount, currentMoney));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, moneyCollect, i.modAmount, moneyCollect));

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
                                StartCoroutine(ApplyOverTime(i.overTimeDuration, playerStats.bonusSpeed, i.modAmount, playerStats.bonusSpeed));
                            }
                            else
                            {
                                playerStats.bonusSpeed += i.modAmount;
                            }
                        }
                        break;

                }
                
                break;
            
            case Items.Effect.Object:
                switch (i.alteration)
                {
                    case Items.Alteration.Creation:

                        for (int j = 0; j < i.spawnAmount; j++)
                        {
                            GameObject objectSpawn = Instantiate(i.objectPrefab, i.spawnPoint);
                            StartCoroutine(DelayToDestroy(i.spawnTime, objectSpawn));
                        }
                        break;
                    
                    case Items.Alteration.Destruction:
                        break;
                }
                break;
        }
    }

    IEnumerator ApplyOverTime(float duration, float modVariable, float modAmount, float backup)
    {
        modVariable += modAmount;
        yield return new WaitForSeconds(duration);
        modVariable = backup;
    }

    IEnumerator DelayToDestroy(float duration, GameObject gameObject)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

}
