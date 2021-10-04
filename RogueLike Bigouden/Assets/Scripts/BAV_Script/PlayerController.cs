using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private int playerID;
    public PlayerInput playerInput;
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
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(movementInput.x,movementInput.y,0) * speed *Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
    
    public void OnControlsChanged()
    {
        if(playerInput.currentControlScheme != currentControlScheme)
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

}
