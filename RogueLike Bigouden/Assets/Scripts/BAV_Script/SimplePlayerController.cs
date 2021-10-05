using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimplePlayerController : MonoBehaviour
{
    public float speed = 5;
    private Vector2 movementInput;
    [SerializeField] private int playerID;


    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }


    void Move()
    {
        transform.Translate(new Vector3(movementInput.x,movementInput.y, 0) * speed * Time.deltaTime);
    }
    
    private void Update()
    {
        Move();
    }
}
