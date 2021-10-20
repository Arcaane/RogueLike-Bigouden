using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Items> items;

    public GameObject targetObject;


    void UpdateItems()
    {
        foreach (Items e in items)
        {
            for (int i = 0; i < e.itemEffects.Length; i++)
            {
                Items.ItemEffect currentItemEffect = e.itemEffects[i];
                
                //récupération des information : target, type
                
                #region TARGET
                
                switch (currentItemEffect.target)
                {
                    case Items.ItemEffect.Target.Player : Debug.Log("Player");
                        targetObject = GameObject.FindGameObjectWithTag("Player");
                        break;
                    
                    case Items.ItemEffect.Target.PlayerProjectile : Debug.Log("PlayerProjectile");
                        targetObject = GameObject.FindGameObjectWithTag("PlayerProjectile");
                        break;
                    
                    case Items.ItemEffect.Target.Enemy : Debug.Log("Enemy");
                        targetObject = GameObject.FindGameObjectWithTag("Enemy");
                        break;
                    
                    case Items.ItemEffect.Target.EnemyProjectile : Debug.Log("EnemyProjectile");
                        targetObject = GameObject.FindGameObjectWithTag("EnemyProjectile");
                        break;
                }
                
                #endregion

                #region TYPE
                
                switch (currentItemEffect.type)
                {
                    case Items.ItemEffect.Type.Bonus : Debug.Log("Bonus");
                        break;
                    
                    case Items.ItemEffect.Type.Malus : Debug.Log("Malus");
                        break;
                }
                
                #endregion
                
                //vérification des conditions : conditons

                #region CONDITION
                
                switch (currentItemEffect.condition)
                {
                    case Items.ItemEffect.Condition.None : Debug.Log("None");
                        break;
                    
                    case Items.ItemEffect.Condition.AttackX : Debug.Log("AttackX");
                        break;
                    
                    case Items.ItemEffect.Condition.AttackY : Debug.Log("AttackY");
                        break;
                    
                    case Items.ItemEffect.Condition.AttackUlt : Debug.Log("AttackUlt");
                        break;
                    
                    case Items.ItemEffect.Condition.Dash : Debug.Log("Dash");
                        break;
                    
                    case Items.ItemEffect.Condition.Health : Debug.Log("Health");
                        break;
                    
                    case Items.ItemEffect.Condition.Energy : Debug.Log("Energy");
                        break;
                    
                    case Items.ItemEffect.Condition.Money : Debug.Log("Money");
                        break;
                }
                
                #endregion
                
                //application des modifications : affected

                #region AFFECTED
                
                switch (currentItemEffect.affected)
                {
                    case Items.ItemEffect.Affected.Damage : Debug.Log("Damage");
                        break;
                    
                    case Items.ItemEffect.Affected.Dash : Debug.Log("DashE");
                        break;
                    
                    case Items.ItemEffect.Affected.Health : Debug.Log("Health");
                        break;
                    
                    case Items.ItemEffect.Affected.Energy : Debug.Log("Energy");
                        break;
                    
                    case Items.ItemEffect.Affected.Money : Debug.Log("Money");
                        break;
                    
                    case Items.ItemEffect.Affected.Speed : Debug.Log("Speed");
                        break;
                }
                
                #endregion
            }
        }
    }
}
