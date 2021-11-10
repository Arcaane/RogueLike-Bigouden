using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canape : MonoBehaviour
{
    //gameObject-------------------
    public GameObject player;
    
    //float------------------------
    public float bounciness = 0.5f;
    public BoxCollider2D boxCollider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Bounce()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
