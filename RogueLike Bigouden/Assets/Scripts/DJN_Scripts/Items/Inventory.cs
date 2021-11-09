using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool itemAdded;
    public List<Items> items;
    public string target;
    public Items lastItemAdded;
    public Items itemOnTheFloor;
    public bool conditionActivate;
    public PlayerStatsManager playerStats;
    public int currentMoney;
    
    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<UIManager>();
        playerStats = GetComponent<PlayerStatsManager>();
    }
    
    // Update is called once per frame
    public void Update()
    {
        
    }
    

    public void CheckCondition()
    {
        foreach (Items item in items)
        {
            for (int i = 0; i < item.itemEffects.Length; i++)
            {
                Items.ItemEffect currentItemEffect = item.itemEffects[i];
                
                 switch (currentItemEffect.condition)
                {
                    //si la condition est une action
                    case Items.ItemEffect.Condition.Action:
                        switch (currentItemEffect.action)
                        {
                            case Items.ItemEffect.Action.AttackX:
                                if (currentItemEffect.conditionActionMustBeDone)
                                {
                                    if(playerStats.readyToAttackX)
                                        conditionActivate = true;
                                }
                                break;
                            
                            case Items.ItemEffect.Action.AttackY:
                                if (currentItemEffect.conditionActionMustBeDone)
                                {
                                    if(playerStats.readyToAttackY)
                                        conditionActivate = true;
                                }
                                break;
                            
                            case Items.ItemEffect.Action.AttackUltime:
                                if (currentItemEffect.conditionActionMustBeDone)
                                {
                                    //if InputAttackUlt
                                    conditionActivate = true;
                                }
                                break;
                            
                            case Items.ItemEffect.Action.AttackDistance:
                                if (currentItemEffect.conditionActionMustBeDone)
                                {
                                    if(playerStats.readyToAttackB)
                                        conditionActivate = true;
                                }

                                break;
                            
                            case Items.ItemEffect.Action.Dash:
                                if (currentItemEffect.conditionActionMustBeDone)
                                {
                                    if(playerStats.isDashing)
                                        conditionActivate = true;
                                }

                                break;
                            
                            case Items.ItemEffect.Action.GetHurt:
                                if (currentItemEffect.conditionActionMustBeDone)
                                {
                                    //if getHurt
                                    conditionActivate = true;
                                }

                                break;
                            
                            
                        }
                        break;
                    
                    //si la condition est une valeur
                    case Items.ItemEffect.Condition.Value:
                        switch (currentItemEffect.value)
                        {
                            case Items.ItemEffect.Value.Health:
                                if(playerStats.lifePoint == currentItemEffect.conditionValueToReach)
                                    conditionActivate = true;
                                break;
                            
                            case Items.ItemEffect.Value.Energy:
                                if(playerStats.actualUltPoint == currentItemEffect.conditionValueToReach)
                                 conditionActivate = true;
                                break;
                            
                            case Items.ItemEffect.Value.Money:
                                if(currentMoney == currentItemEffect.conditionValueToReach)
                                    conditionActivate = true;
                                break;

                        }

                        break;
                    
                    case Items.ItemEffect.Condition.None:
                        conditionActivate = true;
                        break;

                }
            }
        }
    }
    
    public void UpdateItems()
    {
        //StartCoroutine(ResetCondition());
        Debug.Log("Updating Items...");
        foreach (Items item in items)
        {
            for(int i = 0; i < item.itemEffects.Length; i++)
            {
                Debug.Log("Looking for List...");
                Items.ItemEffect currentItemEffect = item.itemEffects[i];

                //here we setup the target
                switch (currentItemEffect.target)
                {

                    case(Items.ItemEffect.Target.Player):
                        target = "player";
                        Debug.Log("target is" + target);
                        break;
                    
                    case(Items.ItemEffect.Target.Enemy) :
                        target = "enemy";
                        Debug.Log("target is" + target);
                        break;
                }
                
                //now we setup the condition type to launch the action and say if this condition is filled or not

                //then we activate the effect
                
                    switch (currentItemEffect.effectOn)
                    {
                        case Items.ItemEffect.Effect.Variable:

                            switch (currentItemEffect.type)
                            {
                                case Items.ItemEffect.Type.Bonus:
                                    currentItemEffect.amount = currentItemEffect.amount;
                                    break;
                                case Items.ItemEffect.Type.Malus:
                                    currentItemEffect.amount = -currentItemEffect.amount;
                                    break;
                            }
                            
                            switch (currentItemEffect.augmentation)
                            {
                                case Items.ItemEffect.Augmentation.Health:
                                    Debug.Log("It's Okay Bro, that's work");
                                    
                                    if (currentItemEffect.onCurrent)
                                    {
                                        if (currentItemEffect.overTime)
                                        {
                                            StartCoroutine(OvertimeEffect(playerStats.lifePoint,
                                                currentItemEffect.amount, currentItemEffect.overTimeDuration));
                                        }

                                        else
                                        {
                                            playerStats.lifePoint += currentItemEffect.amount;

                                        }

                                    }
                                    else
                                    {
                                        if (currentItemEffect.overTime)
                                        {
                                            StartCoroutine(OvertimeEffect( playerStats.maxLifePoint,
                                                currentItemEffect.amount, currentItemEffect.overTimeDuration));
                                        }

                                        else
                                        {
                                            playerStats.maxLifePoint += currentItemEffect.amount;

                                        }
                                    }
                                    break;
                                
                                case Items.ItemEffect.Augmentation.Damage:
                                    playerStats.damageX += currentItemEffect.amount;
                                    playerStats.damageY += currentItemEffect.amount;
                                    break;
                                
                                case Items.ItemEffect.Augmentation.Dash:
                                    playerStats.dashRange += currentItemEffect.amount;
                                    break;
                                
                                case Items.ItemEffect.Augmentation.Energy:
                                    
                                    if (currentItemEffect.overTime)
                                    { 
                                        StartCoroutine(OvertimeEffect(playerStats.actualUltPoint,
                                            currentItemEffect.amount, currentItemEffect.overTimeDuration));
                                        
                                        if (target == "player")
                                        {
                                            playerStats.actualUltPoint += currentItemEffect.amount;
                                        }
                                        else if(target == "enemy")
                                        {
                                        }
                                    }
                                        
                                    break;
                                
                                case Items.ItemEffect.Augmentation.AttackRange:

                                    if (currentItemEffect.overTime)
                                    {
                                       //add overtime effect
                                    }
                                    else
                                    {
                                        if (target == "player")
                                        {
                                            playerStats.attackRangeX += currentItemEffect.amount;
                                            playerStats.attackRangeY += currentItemEffect.amount;
                                        }
                                    }
                                    
                                    break;
                                
                                case Items.ItemEffect.Augmentation.AttackSpeed:
                                    playerStats.attackCdX += currentItemEffect.amount;
                                    return;
                                
                            }

                            break;
                        
                        case Items.ItemEffect.Effect.Object:

                            switch ( currentItemEffect.alteration)
                            {
                                case Items.ItemEffect.Alteration.Creation:
                                    for (int j = 0; j < currentItemEffect.appearAmount; j++)
                                    {
                                        Instantiate(currentItemEffect.targetObject, currentItemEffect.pointOfAppear);
                                    }
                                    //add overtime effect
                                    return;
                                
                                case Items.ItemEffect.Alteration.Destruction:
                                    Destroy(currentItemEffect.targetObject);
                                    return;
                            }
                            
                            break;
                    }
                
            }
                
        }
    }
        
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            itemOnTheFloor = other.GetComponent<DropSystem>().itemSelect;
            uiManager.ItemPanelInformation();
            
            if (Input.GetKey(KeyCode.Space))
            {
                itemAdded = true;
                UpdateItems();
                items.Add(other.GetComponent<DropSystem>().itemSelect);
                lastItemAdded = other.GetComponent<DropSystem>().itemSelect;
                StartCoroutine(DestroyObject(other.gameObject));
            }
           
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            uiManager.ClosePanel();
        }
    }

    IEnumerator OvertimeEffect(int valueToChange, int amountValue, float time)
    {
        int backupValue = valueToChange;
        valueToChange += amountValue;
        yield return new WaitForSeconds(time);
        valueToChange = backupValue;
    }
    
    IEnumerator ResetCondition()
    {
        yield return new WaitForSeconds(0.0000000000001f);
        conditionActivate = false;
    }
    IEnumerator DestroyObject(GameObject item)
    {
        yield return new WaitForSeconds(0.00000000001f);
        itemAdded = false;
        Destroy(item);
    }
}
