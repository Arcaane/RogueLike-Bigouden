using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class MoveCircle : MonoBehaviour
{
    public Transform objectToRotate;
    public Transform objectTarget;

    public float radius = 1;

    private void Start()
    {
        objectToRotate.position = new Vector3(radius, 0, 0);
        objectTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        RotatePointer();
    }

    void RotatePointer()
    {
        // Determine which direction to rotate towards
        Vector3 posTarget = objectTarget.position;
        Vector3 rotPos = objectToRotate.position;
        Vector3 rotObjs = transform.position;
        
        Vector3 lookPos = posTarget - rotPos;
        
        
        float rotationXObj = Mathf.Atan2(posTarget.y, posTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotationXObj, Vector3.forward);

        /*
        float rotationX = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        objectToRotate.rotation = Quaternion.AngleAxis(rotationX, Vector3.forward);
        */
        //objectToRotate.position = new Vector3(radius,0 ,0);
        Debug.DrawRay(rotPos, posTarget - rotPos, Color.red);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}