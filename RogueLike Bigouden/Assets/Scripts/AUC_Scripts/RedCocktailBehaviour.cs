using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCocktailBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask isPlayer;
    void Start()
    {
        InvokeRepeating(nameof(DamagePlayer), 1, 1.5f);
        Destroy(gameObject, 5f);
    }

    private void DamagePlayer()
    {
        Collider2D[] playerCircleAll = Physics2D.OverlapCircleAll(transform.position, 2, isPlayer);
        foreach (var p in playerCircleAll)
        {
            p.GetComponent<PlayerStatsManager>().TakeDamage(2);
            Debug.Log("Player hit : " + p.name + " by red cocktail");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
