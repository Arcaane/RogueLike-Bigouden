using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(target.position.x, target.position.y), ForceMode2D.Impulse);
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
