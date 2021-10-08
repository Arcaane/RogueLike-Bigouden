using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int playerID;
   public PlayerInput playerInput;
    public InputActionMap gameplayActions;
    [SerializeField]  BAV_PlayerController inputController;

    //Current Control Scheme
    private string currentControlScheme;
    public float speed = 5;
    private Vector2 movementInput, lookAxis;
    private float actionYButton, actionXButton, actionAButton, actionBButton, actionMove;
    private float attackInput, dashInput;
    private Rigidbody2D rb;
    private bool isMoving;

    //Animator
    [SerializeField] private Animator animatorPlayer;


    private void Awake()
    {
        inputController = new BAV_PlayerController();
        //CheckActionMap();
        rb = GetComponent<Rigidbody2D>();
        playerID = playerInput.playerIndex;
        isMoving = false;
    }

    private void OnEnable()
    {
        inputController.Player.AButton.Enable();
    }


    private void OnDisable()
    {
        inputController.Disable();
        inputController.Player.AButton.Disable();
    }

    private void Start()
    {
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }


    #region InputConfig

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

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        actionAButton = ctx.ReadValue<float>();
        switch (ctx.performed)
        {
            case true:
                Attack();
                break;
            case false:
                break;
        }
        Attack();
    }

    #endregion


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


    void Attack()
    {
        /*
        if (actionAButton == 1)
        {
            Debug.Log("je suis pressé");
        }
        */

        if (actionAButton == 0)
        {
            Debug.Log("je ne suis pas pressé");
        }
    }


    void Dash(float x, float y)
    {
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * 2;
    }

    /*void CheckActionMap()
    {
        gameplayActions = inputController.Player;
        var aButton = inputController.Player.AButton;
        var bButton = inputController.Player.BButton;
        var xButton = inputController.Player.YButton;
        var yButton = inputController.Player.XButton;
    }*/


    #region Zone des controles de la manette

    public void OnControlsChanged()
    {
        if (playerInput.currentControlScheme != currentControlScheme)
        {
            currentControlScheme = playerInput.currentControlScheme;
        }
    }

    public void OnDeviceLost()
    {
        var gamepad = Gamepad.current;
        Debug.Log("Device " + gamepad + " Lost");
    }


    public void OnDeviceRegained()
    {
        var gamepad = Gamepad.current;
        StartCoroutine(WaitForDeviceToBeRegained());
        Debug.Log("Device " + gamepad + " Regained");
    }

    IEnumerator WaitForDeviceToBeRegained()
    {
        yield return new WaitForSeconds(0.1f);
    }

    #endregion


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (EditorApplication.isPlaying)
        {
            Vector2 moveInput = inputController.Player.Move.ReadValue<Vector2>();
            Handles.DrawLine(transform.position, transform.position + new Vector3(moveInput.x, moveInput.y, 0));
        }
    }

#endif

    #region Animation



    #endregion
}

