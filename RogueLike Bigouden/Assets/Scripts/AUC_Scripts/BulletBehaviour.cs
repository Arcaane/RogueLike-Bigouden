using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask isPlayer;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    public void GoDirection(Vector2 direction, float b_Speed)
    {
        rb.velocity = (direction * b_Speed);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatsManager>().TakeDamage(1);
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }
}
