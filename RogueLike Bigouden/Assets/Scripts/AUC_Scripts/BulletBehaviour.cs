using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isReverse;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    public void GoDirection(Vector2 direction, float b_Speed)
    {
        rb.velocity = (direction * b_Speed);
    }
    
    private void OnTriggerEnter2D(Collider2D colid)
    {
        GameObject objColid = colid.gameObject;
        PlayerStatsManager stats = objColid.GetComponent<PlayerStatsManager>();
        if (objColid.CompareTag("Player"))
        {
            stats.TakeDamage(1);
            gameObject.SetActive(false);
        }

        if (objColid.CompareTag("Border") || objColid.CompareTag("Sofa") || objColid.CompareTag("Pillier") || objColid.CompareTag("Obstacle") || objColid.CompareTag("Destructible"))
        {
            gameObject.SetActive(false);
        }

        if (isReverse && objColid.CompareTag("Ennemy"))
        {
            objColid.GetComponent<EnnemyStatsManager>().TakeDamage(1);
            gameObject.SetActive(false);
        }
    }
}
