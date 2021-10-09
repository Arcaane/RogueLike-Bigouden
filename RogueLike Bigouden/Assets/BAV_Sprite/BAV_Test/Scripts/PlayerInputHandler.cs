using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerConfiguration playerConfig;

    [SerializeField] private SpriteRenderer playerMesh;

    //PlayerController
    private BAV_PlayerController controls;
    
    private Vector2 movementInput, lookAxis;
    private bool isMoving;
    public float speed = 5;

    //Animator
    [SerializeField] private Animator animatorPlayer;

    private void Awake()
    {
        controls = new BAV_PlayerController();
    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
        playerMesh.material = config.playerMaterial;
        config.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        switch (obj.action.name == controls.Player.Move.name)
        {
            case true:
                OnMove(obj);
                break;
            case false:
                break;
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
        isMoving = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        switch (isMoving && lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 || lookAxis.y < 0)
        {
            case true:
                animatorPlayer.SetFloat("Horizontal", lookAxis.x);
                animatorPlayer.SetFloat("Vertical", lookAxis.y);
                break;
            case false:
                animatorPlayer.SetFloat("Horizontal", movementInput.x);
                animatorPlayer.SetFloat("Vertical", movementInput.y);
                break;
        }

        transform.Translate(new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime);
    }
}