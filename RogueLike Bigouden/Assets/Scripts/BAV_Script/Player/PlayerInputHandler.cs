using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
    public bool isMoving;
    public float speed = 5;

    //Animator
    [SerializeField] private Animator animatorPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        controls = new BAV_PlayerController();
        isMoving = false;
    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
        playerMesh.material = config.playerMaterial;
        config.Input.onActionTriggered += Input_MoveTrigger;
    }

    public void OnEnable()
    {
        AButton(true);
        BButton(true);
        XButton(true);
        YButton(true);
    }

    public void OnDisable()
    {
        AButton(false);
        BButton(false);
        XButton(false);
        YButton(false);
    }


    private void Input_MoveTrigger(CallbackContext obj)
    {
        switch (obj.action.name == controls.Player.Move.name)
        {
            case true:
                OnMove(obj);
                break;
            case false:
                break;
        }

        switch (obj.action.name == controls.Player.Look.name)
        {
            case true:
                OnLook(obj);
                break;
            case false:
                break;
        }
    }

    private void AButton(bool isEnable)
    {
        switch (isEnable)
        {
            case  true:
                controls.Player.AButton.Enable();
                controls.Player.AButton.started += Input_AButton;
                controls.Player.AButton.performed += Input_AButton;
                controls.Player.AButton.canceled += Input_AButton;
                break;
            case false:
                controls.Player.AButton.Disable();
                break;
        }
    }
    
    private void BButton(bool isEnable)
    {
        switch (isEnable)
        {
            case  true:
                controls.Player.BButton.Enable();
                controls.Player.BButton.started += Input_BButton;
                controls.Player.BButton.performed += Input_BButton;
                controls.Player.BButton.canceled += Input_BButton;
                break;
            case false:
                controls.Player.BButton.Disable();
                break;
        }
    }
    
    private void XButton(bool isEnable)
    {
        switch (isEnable)
        {
            case  true:
                controls.Player.XButton.Enable();
                controls.Player.XButton.started += Input_XButton;
                controls.Player.XButton.performed += Input_XButton;
                controls.Player.XButton.canceled += Input_XButton;
                break;
            case false:
                controls.Player.XButton.Disable();
                break;
        }
    }
    
    private void YButton(bool isEnable)
    {
        switch (isEnable)
        {
            case  true:
                controls.Player.YButton.Enable();
                controls.Player.YButton.started += Input_YButton;
                controls.Player.YButton.performed += Input_YButton;
                controls.Player.YButton.canceled += Input_YButton;
                break;
            case false:
                controls.Player.YButton.Disable();
                break;
        }
    }

    private void Input_AButton(InputAction.CallbackContext buttonA)
    {
        switch (buttonA.started)
        {
            case true:
                Debug.Log("Button A Started");
                break;
            case false:
                break;
        }
        
        switch (buttonA.performed)
        {
            case true:
                Debug.Log("Button A Performed");
                break;
            case false:
                break;
        }
        
        switch (buttonA.canceled)
        {
            case true:
                Debug.Log("Button A Canceled");
                break;
            case false:
                break;
        }
    }
    
    private void Input_BButton(InputAction.CallbackContext buttonB)
    {
        switch (buttonB.started)
        {
            case true:
                Debug.Log("Button B Started");
                break;
            case false:
                break;
        }
        
        switch (buttonB.performed)
        {
            case true:
                Debug.Log("Button B Performed");
                break;
            case false:
                break;
        }
        
        switch (buttonB.canceled)
        {
            case true:
                Debug.Log("Button B Canceled");
                break;
            case false:
                break;
        }
    }
    
    private void Input_XButton(InputAction.CallbackContext buttonX)
    {
        switch (buttonX.started)
        {
            case true:
                Debug.Log("Button X Started");
                break;
            case false:
                break;
        }
        
        switch (buttonX.performed)
        {
            case true:
                Debug.Log("Button X Performed");
                break;
            case false:
                break;
        }
        
        switch (buttonX.canceled)
        {
            case true:
                Debug.Log("Button X Canceled");
                break;
            case false:
                break;
        }
    }
    
    private void Input_YButton(InputAction.CallbackContext buttonY)
    {
        switch (buttonY.started)
        {
            case true:
                Debug.Log("Button Y Started");
                break;
            case false:
                break;
        }
        
        switch (buttonY.performed)
        {
            case true:
                Debug.Log("Button Y Performed");
                break;
            case false:
                break;
        }
        
        switch (buttonY.canceled)
        {
            case true:
                Debug.Log("Button Y Canceled");
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
    
    public void OnLook(InputAction.CallbackContext ctx)
    {
        lookAxis = ctx.ReadValue<Vector2>();
        isMoving = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        switch (lookAxis.x > 0 || lookAxis.x < 0 || lookAxis.y > 0 || lookAxis.y < 0)
        {
            case true:
                animatorPlayer.SetFloat("Horizontal", lookAxis.x);
                animatorPlayer.SetFloat("Vertical", lookAxis.y);
                //Debug.Log("0");
                break;
            case false:
                animatorPlayer.SetFloat("Horizontal", movementInput.x);
                animatorPlayer.SetFloat("Vertical", movementInput.y);
                //Debug.Log("1");
                break;
        }

        transform.Translate(new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime);
    }
}