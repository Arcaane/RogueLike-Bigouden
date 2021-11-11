using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canape : MonoBehaviour
{
    //gameObject-------------------
    public GameObject player;
    private Rigidbody2D _playerRigid;

    //float------------------------
    public float bounciness = 0.5f;
    public BoxCollider2D boxCollider;

    //list-------------------------
    public List<GameObject> objectEnter;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (_playerRigid != null && player != null)
        {
            _playerRigid = player.GetComponent<Rigidbody2D>();
        }
    }

    void Bounce()
    {
        Debug.Log("coucou");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (objectEnter.Contains(collider.gameObject))
        {
            player = collider.gameObject;
        }
        Bounce();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        player = null;
        Bounce();
    }
}