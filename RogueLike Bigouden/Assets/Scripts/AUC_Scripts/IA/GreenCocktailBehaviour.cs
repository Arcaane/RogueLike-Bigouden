using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCocktailBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask isEnnemy;
    
    void Start()
    {
        InvokeRepeating(nameof(HealEnnemiesTick), 1, 1.75f);
        Destroy(gameObject, 5f);
    }

    private void HealEnnemiesTick()
    {
        // Detect Ennemies
        Collider2D[] ennemyInRadius = Physics2D.OverlapCircleAll(transform.position, 1.5f, isEnnemy);
        
        // Heal Them
        foreach (var ennemy in ennemyInRadius)
        {
           ennemy.GetComponent<EnnemyStatsManager>().TakeHeal(2);
            Debug.Log("Ennemy healed : " + ennemy.name);
        }
    }
    
}
