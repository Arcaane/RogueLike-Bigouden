using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    private float timeCounter = 0;
    public Vector2 speedSize;
    private Vector3 posPlayer;

    private void Start()
    {
        posPlayer = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Circle();
    }

    void Circle()
    {
        timeCounter += Time.deltaTime * speedSize.x;

        float x = Mathf.Cos(timeCounter) * speedSize.y;
        float y = Mathf.Sin(timeCounter) * speedSize.y;
        float z = 10;

        transform.position =  posPlayer + new Vector3(x, y, z);
    }
}