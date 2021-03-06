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
        objectToRotate.position = new Vector3(transform.position.x + radius, transform.position.y, 0);
        
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
        
        Vector3 lookPos = posTarget - rotPos;
        
        // Rotate autour du radius
        float rotationXObj = Mathf.Atan2(posTarget.y, posTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotationXObj, Vector3.forward);
        
        // Rotate uniqument l'enfant
        //float rotationX = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        //objectToRotate.rotation = Quaternion.AngleAxis(rotationX, Vector3.forward);
        
        //objectToRotate.position = new Vector3(radius,0 ,0);
        Debug.DrawRay(rotPos, posTarget - rotPos, Color.red);
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, new Vector3(transform.position.x, 0, transform.position.z - 90f),
            radius);
    }
}