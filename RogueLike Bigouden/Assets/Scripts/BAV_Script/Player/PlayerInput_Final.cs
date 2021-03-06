using System.Diagnostics;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;
using Debug = UnityEngine.Debug;

public class PlayerInput_Final : MonoBehaviour
{
    private PlayerConfiguration playerConfig;

    //PlayerController
    private BAV_PlayerController controls;

    [SerializeField] PlayerAttribut playerAttribut;
    public bool isMoving;

    [Header("Boutton Value")]
    //Concerne les valeurs des Inputs renvoyer par les bouttons
    [SerializeField]
    private float buttonAValue;

    [SerializeField] private float buttonBValue;
    [SerializeField] private float buttonXValue;
    [SerializeField] private float buttonYValue;

    [Header("Trigger Value")]
    //Concerne les Inputs des trigger
    [SerializeField]
    private float leftPressTrigger;

    [SerializeField] private float rightPressTrigger;

    private float duration = 0.2f;
    private int inputPerformed = 0;

    private void Awake()
    {
        controls = new BAV_PlayerController();
        isMoving = false;
    }

    public void InitializePlayer(PlayerConfiguration config)
    {
        playerConfig = config;
        playerAttribut.playerMesh.material = config.playerMaterial;
        controls.Player.Move.performed += OnMove;
    }

    public void OnEnable()
    {
        controls.Enable();
    }

    public void OnDisable()
    {
        controls.Disable();
    }

    /// <summary>
    /// Permet de déclencher les Etats du boutton Input A
    /// </summary>
    /// <param name="isAEnable"></param>
    void AButton(bool isAEnable)
    {
        switch (isAEnable)
        {
            case true:
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

    /// <summary>
    /// Permet de déclencher les Etats du boutton Input B
    /// </summary>
    /// <param name="isBEnable"></param>
    void BButton(bool isBEnable)
    {
        switch (isBEnable)
        {
            case true:

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

    /// <summary>
    /// Permet de déclencher les Etats du boutton Input X
    /// </summary>
    /// <param name="isXEnable"></param>
    void XButton(bool isXEnable)
    {
        switch (isXEnable)
        {
            case true:
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

    /// <summary>
    /// Permet de déclencher les Etats du boutton Input Y
    /// </summary>
    /// <param name="isYEnable"></param>
    void YButton(bool isYEnable)
    {
        switch (isYEnable)
        {
            case true:
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

    /// <summary>
    /// Permet de déclencher les Etats de la gachette Droite.
    /// </summary>
    /// <param name="isRightTriggerEnable"></param>
    void RightTrigger(bool isRightTriggerEnable)
    {
        switch (isRightTriggerEnable)
        {
            case true:
                controls.Player.RightTrigger.Enable();
                controls.Player.RightTrigger.started += Input_RightTrigger;
                controls.Player.RightTrigger.performed += Input_RightTrigger;
                controls.Player.RightTrigger.canceled += Input_RightTrigger;
                break;
            case false:
                controls.Player.RightTrigger.Disable();
                break;
        }
    }

    /// <summary>
    /// Permet d'appeler l'input du Boutton A
    /// </summary>
    /// <param name="buttonA"></param>
    public void Input_AButton(CallbackContext buttonA)
    {
        buttonAValue = buttonA.ReadValue<float>();
        switch (buttonA.started)
        {
            case true:
                Debug.Log("Button A Started" + buttonAValue);
                break;
            case false:
                break;
        }

        switch (buttonA.performed)
        {
            case true:
                playerAttribut.Dash();
                Debug.Log("Button A performed");
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


    /// <summary>
    /// Permet d'appeler l'input du Boutton B
    /// </summary>
    /// <param name="buttonB"></param>
    public void Input_BButton(CallbackContext buttonB)
    {
        buttonBValue = buttonB.ReadValue<float>();
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

    /// <summary>
    /// Permet d'appeler l'input du Boutton X
    /// </summary>
    /// <param name="buttonX"></param>
    public void Input_XButton(CallbackContext buttonX)
    {
        buttonXValue = buttonX.ReadValue<float>();
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
                playerAttribut.AttackType();
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

    /// <summary>
    /// Permet d'appeler l'input du Boutton Y
    /// </summary>
    /// <param name="buttonY"></param>
    public void Input_YButton(CallbackContext buttonY)
    {
        buttonYValue = buttonY.ReadValue<float>();
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

    /// <summary>
    /// Permet d'appeler l'input de la Gachette Droite
    /// </summary>
    /// <param name="rightTrigger"></param>
    public void Input_RightTrigger(CallbackContext rightTrigger)
    {
        rightPressTrigger = rightTrigger.ReadValue<float>();
        switch (rightTrigger.started)
        {
            case true:
                Debug.Log("Button Left Trigger Started");
                break;
            case false:
                break;
        }

        switch (rightTrigger.performed)
        {
            case true:
                Debug.Log("Button Left Trigger Performed");
                break;
            case false:
                break;
        }

        switch (rightTrigger.canceled)
        {
            case true:
                Debug.Log("Button Left Trigger Canceled");
                break;
            case false:
                break;
        }
    }

    
    
    /// <summary>
    /// Permet d'appeler l'input du Stick Gauche
    /// </summary>
    /// <param name="leftStick"></param>
    public void OnMove(CallbackContext leftStick)
    {
        playerAttribut.SetInputVector(leftStick.ReadValue<Vector2>(), false);
        isMoving = true;
    }

    /// <summary>
    /// Permet d'appeler l'input du Stick Droit
    /// </summary>
    /// <param name="rightStick"></param>
    public void OnLook(CallbackContext rightStick)
    {
        playerAttribut.SetInputVector(rightStick.ReadValue<Vector2>(), true);
    }
}