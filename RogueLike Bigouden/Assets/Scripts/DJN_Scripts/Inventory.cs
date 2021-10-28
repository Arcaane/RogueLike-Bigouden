using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool itemAdded;
    public List<Items> items;
    public GameObject target;
    public Items lastItemAdded;
    public Items itemOnTheFloor;
    public bool conditionActivate;

    private UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<UIManager>();
    }
    
    // Update is called once per frame
    public void Update()
    {
        if(conditionActivate)
            UpdateItems();
        
        if(!conditionActivate)
            CheckCondition();
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
                                    //if InputActtackX
                                    conditionActivate = true;
                                }
                                break;
                            
                            case Items.ItemEffect.Action.AttackY:
                                if (currentItemEffect.conditionActionMustBeDone)
                                {
                                    //if InputAttackY
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
                                    //if InputAttackDist
                                    conditionActivate = true;
                                }

                                break;
                            
                            case Items.ItemEffect.Action.Dash:
                                if (currentItemEffect.conditionActionMustBeDone)
                                {
                                    //if InputDash
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
                                //if player.health = currentItemEffect.conditionValueToReach
                                conditionActivate = true;
                                break;
                            
                            case Items.ItemEffect.Value.Energy:
                                //if player.energy = currentItemEffect.conditionValueToReach
                                conditionActivate = true;
                                break;
                            
                            case Items.ItemEffect.Value.Money:
                                //if player.money = currentItemEffect.conditionValueToReach
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
        StartCoroutine(ResetCondition());

        foreach (Items item in items)
        {
            for(int i = 0; i < item.itemEffects.Length; i++)
            {
                Items.ItemEffect currentItemEffect = item.itemEffects[i];

                //here we setup the target
                switch (currentItemEffect.target)
                {
                    case(Items.ItemEffect.Target.Player):
                        target = GameObject.FindGameObjectWithTag("Player");
                        break;
                    
                    case(Items.ItemEffect.Target.Enemy) :
                        target = GameObject.FindGameObjectWithTag("Enemy");
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
                                        //if(currentItemEffect.OverTime)
                                        
                                            //StartCoroutine(OvertimeEffect(int valueToChange == player.currentHealth, int amountValue == currentItemEffect.amount, float timeDuration == currentItemEffect.overTimeDuration))
                                            
                                        //else
                                            //player.currentHealth += currentItemEffect.amount;
                                  
                                    }
                                    else
                                    {
                                        //player.maxHealth += currentItemEffect.amount;
                                       
                                    }
                                    break;
                                
                                case Items.ItemEffect.Augmentation.Damage:
                                    //target.currentAttackDamage += currentItemEffect.amount;
                                    break;
                                
                                case Items.ItemEffect.Augmentation.Dash:
                                    //target.dashSpeed += currentItemEffect.amount;
                                    break;
                                
                                case Items.ItemEffect.Augmentation.Energy:
                                    
                                    //if(target.currentEnergy)
                                    if (currentItemEffect.onCurrent)
                                    { 
                                        //if(currentItemEffect.OverTime)
                                        
                                            //StartCoroutine(OvertimeEffect(int valueToChange == player.currentEnergy, int amountValue == currentItemEffect.amount, float timeDuration == currentItemEffect.overTimeDuration))
                                        
                                        //else
                                            //target.currentEnergy += currentItemEffect.amount;
                                    }
                                        
                                    break;
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
