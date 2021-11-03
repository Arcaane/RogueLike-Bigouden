using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCocktailBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask isEnnemy;
    
    void Start()
    {
        InvokeRepeating(nameof(HealEnnemiesTick), 1, 1.5f);
        Destroy(gameObject, 5f);
    }

    private void HealEnnemiesTick()
    {
        // Detect Ennemies
        Collider2D[] ennemyInRadius = Physics2D.OverlapCircleAll(transform.position, 2, isEnnemy);
        
        // Heal Them
        foreach (var ennemy in ennemyInRadius)
        {
            switch (ennemy.name)
            {
                case "Ennemy1 _ Shooter":
                    ennemy.GetComponent<IAShooter>().lifePoint += 1;
                    break;
                case "Ennemy2 _ Runner":
                    ennemy.GetComponent<IARunner>().lifePoint += 1;
                    break;
                case "Ennemy3_Barman":
                    ennemy.GetComponent<IABarman>().lifePoint += 1;
                    break;
                case "Ennemy4_Cac":
                    ennemy.GetComponent<IACac>().lifePoint += 1;
                    break;
                default:
                    break;
            }
            Debug.Log("Ennemy healed : " + ennemy.name);
        }
    }
    
}
