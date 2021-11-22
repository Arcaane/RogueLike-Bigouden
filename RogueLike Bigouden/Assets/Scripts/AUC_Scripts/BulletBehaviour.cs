using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    public float b_speed = 8f;
    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    public void GoDirection(Vector2 direction)
    {
        rb.velocity =(direction * b_speed);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Border"))
        {
            gameObject.SetActive(false);
        }
    }
}
