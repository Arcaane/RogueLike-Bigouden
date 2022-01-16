using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCocktailBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask isPlayer;
    void Start()
    {
        InvokeRepeating(nameof(DamagePlayer), 1, 1.75f); 
        Destroy(gameObject, 5f);
    }

    private void DamagePlayer()
    {
        Collider2D[] playerCircleAll = Physics2D.OverlapCircleAll(transform.position, 3, isPlayer);
        foreach (var p in playerCircleAll)
        {
            p.GetComponent<PlayerStatsManager>().TakeDamage(1);
        }
    }
    
    
}
