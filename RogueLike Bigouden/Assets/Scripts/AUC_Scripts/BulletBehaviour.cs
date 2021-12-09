using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb; 
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
        //Modif Baptiste
        GameObject objColid = colid.gameObject;
        PlayerStatsManager stats = objColid.GetComponent<PlayerStatsManager>();
        if (objColid.CompareTag("Player") && !stats.isDashing)
        {
            stats.TakeDamage(1);
            gameObject.SetActive(false);
        }

        if (objColid.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }
}
