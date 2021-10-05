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
    
    [SerializeField]public  int playerID;
    public PlayerInput playerInput;
    public BAV_PlayerController inputController;
    /*
    [Header("Input Settings")]
    public float movementSmoothingSpeed = 1f;
    private Vector3 rawInputMovement;
    private Vector3 smoothInputMovement;
    */
    
    //Current Control Scheme
    private string currentControlScheme;
    public float speed = 5;
    private Vector2 movementInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        inputController = new BAV_PlayerController();
        currentControlScheme = playerInput.currentControlScheme;
        
        rb = GetComponent<Rigidbody2D>();
        playerID = playerInput.playerIndex;
    }

    private void OnEnable()
    {
        inputController.Enable();
    }

    
    private void OnDisable()
    {
        inputController.Disable();
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
    }
    #endregion
    
    void Move()
    {
        transform.Translate(new Vector3(movementInput.x,movementInput.y, 0) * speed * Time.deltaTime);
    }
    
    /*
    public void OnDash(InputAction.CallbackContext value)
    {
        Vector2 moveInput = inputController.Player_GK.Move.ReadValue<Vector2>();
            Debug.Log("Je suis la");
        if (value.started)
        {
            Dash(moveInput.x, moveInput.y);
        }
    }
    */
    
    void Dash(float x, float y)
    {
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, y).normalized * 2;
    }

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
    

    private void OnDrawGizmos()
    {
        if (EditorApplication.isPlaying)
        {
            Vector2 moveInput = inputController.Player_GK.Move.ReadValue<Vector2>();
            Handles.DrawLine(transform.position, transform.position + new Vector3(moveInput.x, moveInput.y, 0));
        }
    }
}
