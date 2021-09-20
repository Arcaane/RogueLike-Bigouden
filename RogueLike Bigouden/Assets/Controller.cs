using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Controller : MonoBehaviour 
{
    PlayerControls _controls;
    [SerializeField] private Transform playerCursor;

    // Player Movement
    [SerializeField] private Vector2 move;
    public float movementSpeed = 10f;
    
    // Camera Movement
    /* [SerializeField] private Vector2 moveCam;
    public bool rStick; */
    
    private void Awake()
    {
        _controls = new PlayerControls();
        
        _controls.Player.XAttack.performed += ctx => XAttack();
        _controls.Player.YAttack.performed += ctx => YAttack();
        _controls.Player.AButton.performed += ctx => AButton();
        _controls.Player.BButton.performed += ctx => BButton();

        _controls.Player.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => move = Vector2.zero;
    }

    private void Start()
    {
        playerCursor = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).transform;
        // rStick = false;
    }

    private void Update()
    {
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime * movementSpeed;
        transform.Translate(m, Space.World);
        /*
        Vector2 mCam = new Vector2(moveCam.x, moveCam.y) * Time.deltaTime * 50;
        playerCursor.transform.Translate(mCam, Space.World); */
    }

    void XAttack()
    {
        if(PlayerManager.instance.readyToAttackX)
            PlayerManager.instance.XAttack();
    }
    
    void YAttack()
    {
        if(PlayerManager.instance.readyToAttackY)
            PlayerManager.instance.YAttack();
    }

    void AButton()
    {
        Debug.Log("AButton Pressed!");
    }

    void BButton()
    {
        if(PlayerManager.instance.readyToDash)
            PlayerManager.instance.Dash();
    }
    
    /*
    void RStickInfo()
    {
        rStick = true;
    }
    */
    
    /* void ResetPosition()
    {
        playerCursor.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        moveCam = Vector2.zero;
        rStick = false;
        Debug.Log("Camera Reset Pos"); } */

    private void OnEnable()
    {
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }
}
