using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCocktailBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask isPlayer;
    void Start()
    {
        InvokeRepeating(nameof(DamagePlayer), 1, 1.5f); 
        Destroy(gameObject, 5f);
    }

    private void DamagePlayer()
    {
        Collider2D[] playerCircleAll = Physics2D.OverlapCircleAll(transform.position, 1, isPlayer);
        foreach (var p in playerCircleAll)
        {
            p.GetComponent<PlayerStatsManager>().TakeDamage(1);
            Debug.Log("Player hit : " + p.name + " by yellow cocktail");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStatsManager.playerStatsInstance.movementSpeed *= 0.8f;
        }
    }
}
