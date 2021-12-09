using System;
using System.Diagnostics;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem.UI;
using static UnityEngine.InputSystem.InputAction;
using Debug = UnityEngine.Debug;

public class PlayerInput_Final : MonoBehaviour
{
    private PlayerConfiguration playerConfig;

    //PlayerController
    private BAV_PlayerController controls;

    [Header("Utiliser Mouse Position ?")] [SerializeField]
    public bool kbMouse;

    [SerializeField] Camera _Camera;
    private Vector2 _MousePos;

    [SerializeField] PlayerAttribut playerAttribut;
    public bool isMoving;

    [Header("Boutton Value A")]
    //Concerne la valeur d'input de A
    [SerializeField]
    private float buttonAValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _A_isDash;
    [SerializeField] private bool _A_isAttack;
    [SerializeField] private bool _A_isProjectile;

    [Header("Boutton Value B")]
    //Concerne la valeur d'input de B
    [SerializeField]
    private float buttonBValue;

    [SerializeField] private float buttonBHoldValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _B_isDash;
    [SerializeField] private bool _B_isAttack;
    [SerializeField] private bool _B_isProjectile;

    [Header("Boutton Value X")]
    //Concerne la valeur d'input de X
    [SerializeField]
    private float buttonXValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _X_isDash;
    [SerializeField] private bool _X_isAttack;
    [SerializeField] private bool _X_isProjectile;

    [Header("Boutton Value Y")]
    //Concerne la valeur d'input de Y
    [SerializeField]
    private float buttonYValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _Y_isDash;
    [SerializeField] private bool _Y_isAttack;
    [SerializeField] private bool _Y_isProjectile;

    [Header("Boutton Value Top Left ")]
    //Concerne la valeur d'input de Top Left Trigger
    [SerializeField]
    private float trigger_LeftTopValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _LeftTop_isDash;
    [SerializeField] private bool _LeftTop_isAttack;
    [SerializeField] private bool _LeftTop_isProjectile;

    [Header("Boutton Value Top Right ")]
    //Concerne la valeur d'input de Top Right Trigger
    [SerializeField]
    private float trigger_RightTopValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _RightTop_isDash;
    [SerializeField] private bool _RightTop_isAttack;
    [SerializeField] private bool _RightTop_isProjectile;

    [Header("Boutton Value Bottom Left ")]
    //Concerne la valeur d'input de Bottom Left Trigger
    [SerializeField]
    private float trigger_LeftBottomValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _LeftBottom_isDash;
    [SerializeField] private bool _LeftBottom_isAttack;
    [SerializeField] private bool _LeftBottom_isProjectile;


    [Header("Boutton Value Bottom Right ")] [SerializeField]
    private float trigger_RightBottomValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _RightBottom_isDash;
    [SerializeField] private bool _RightBottom_isAttack;
    [SerializeField] private bool _RightBottom_isProjectile;

    [Header("Boutton Value Left Stick Press ")] [SerializeField]
    private float stick_LeftPressValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _LeftPress_isDash;
    [SerializeField] private bool _LeftPress_isAttack;
    [SerializeField] private bool _LeftPress_isProjectile;
    [SerializeField] private bool _LeftPress_IsUlt;

    [Header("Boutton Value Left Stick Press ")] [SerializeField]
    private float stick_RightPressValue;

    //Can be delete for the Final Build
    [SerializeField] private bool _RightPress_isDash;
    [SerializeField] private bool _RightPress_isAttack;
    [SerializeField] private bool _RightPress_isProjectile;
    [SerializeField] private bool _RightPress_IsUlt;

    private float duration = 0.2f;
    private Vector2 lookValue;
    private Vector2 moveValue;
    private Vector2 lookLocker;
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


        //Use Button----------
        controls.Player.AButton.performed += Input_AButton;
        controls.Player.BButton.performed += Input_BButton;
        controls.Player.BButton.started += Input_BButton;
        controls.Player.BButton.canceled += Input_BButton;
        controls.Player.XButton.performed += Input_XButton;
        controls.Player.YButton.performed += Input_YButton;
        controls.Player.Left_Stick_Press.performed += LeftStickPress;
        controls.Player.Right_Stick_Press.performed += RightStickPress;
        //Use Trigger----------
        controls.Player.Left_Top_Trigger.performed += LeftTopTrigger;
        controls.Player.Left_Bottom_Trigger.performed += LeftBottomTrigger;
        controls.Player.Right_Top_Trigger.performed += RightTopTrigger;
        controls.Player.Right_Bottom_Trigger.performed += RightBottomTrigger;
        //Use Stick----------
        playerConfig.Input.onActionTriggered += OnMove;
        //controls.Player.Move.performed += OnMove;
        //controls.Player.Look.performed += OnLook;
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
    /// Permet d'appeler l'input du Boutton A
    /// </summary>
    /// <param name="buttonA"></param>
    public void Input_AButton(CallbackContext buttonA)
    {
        buttonAValue = buttonA.ReadValue<float>();
        if (buttonA.started)
        {
            //Debug.Log("Button A Started" + buttonAValue);
        }

        if (buttonA.performed)
        {
            if (_A_isDash)
            {
                playerAttribut.Dash();
            }

            if (_A_isAttack)
            {
                playerAttribut.AttackTypeX();
            }

            if (_A_isProjectile)
            {
                //playerAttribut.LaunchProjectile();
            }

            Debug.Log("Button A performed");
        }

        if (buttonA.canceled)
        {
            //Debug.Log("Button A Canceled");
        }
    }


    /// <summary>
    /// Permet d'appeler l'input du Boutton B
    /// </summary>
    /// <param name="buttonB"></param>
    public void Input_BButton(CallbackContext buttonB)
    {
        buttonBValue = buttonB.ReadValue<float>();
        if (playerAttribut.canLaunchProjectile)
        {
            if (buttonB.started)
            {
                //Debug.Log("Button B Started");
                playerAttribut.launchProjectileFeedback.SetActive(true);
            }

            if (buttonB.performed)
            {
                if (_B_isDash)
                {
                    playerAttribut.Dash();
                }

                if (_B_isAttack)
                {
                    playerAttribut.AttackTypeX();
                }

                if (_B_isProjectile)
                {
                    playerAttribut.launchProjectileFeedback.SetActive(true);

                    if (buttonBValue >= InputSystem.settings.defaultHoldTime)
                    {
                        Debug.Log("Button Held");
                        playerAttribut.launchProjectile = true;
                    }
                    else
                    {
                        if (buttonBValue <= InputSystem.settings.defaultButtonPressPoint)
                        {
                            playerAttribut.launchProjectile = false;
                            Debug.Log("Button tapped");
                        }
                    }
                }
            }
        }

        if (buttonB.canceled)
        {
            //Debug.Log("Button B Canceled");
            playerAttribut.launchProjectileFeedback.SetActive(false);
        }
    }

    /// <summary>
    /// Permet d'appeler l'input du Boutton X
    /// </summary>
    /// <param name="buttonX"></param>
    public void Input_XButton(CallbackContext buttonX)
    {
        buttonXValue = buttonX.ReadValue<float>();
        if (buttonX.started)
        {
            //Debug.Log("Button X Started");
        }

        if (buttonX.performed)
        {
            if (_X_isDash)
            {
                playerAttribut.Dash();
            }

            if (_X_isAttack)
            {
                playerAttribut.AttackTypeX();
            }

            if (_X_isProjectile)
            {
                //playerAttribut.LaunchProjectile();
            }
            //Debug.Log("Button X Performed");
        }

        if (buttonX.canceled)
        {
            //Debug.Log("Button X Canceled");
        }
    }

    /// <summary>
    /// Permet d'appeler l'input du Boutton Y
    /// </summary>
    /// <param name="buttonY"></param>
    public void Input_YButton(CallbackContext buttonY)
    {
        buttonYValue = buttonY.ReadValue<float>();
        if (buttonY.started)
        {
            //Debug.Log("Button Y Started");
        }

        if (buttonY.performed)
        {
            if (_Y_isDash)
            {
                playerAttribut.Dash();
            }

            if (_Y_isAttack)
            {
                playerAttribut.AttackTypeX();
            }

            if (_Y_isProjectile)
            {
                // playerAttribut.LaunchProjectile();
            }
            //Debug.Log("Button Y Performed");
        }

        if (buttonY.canceled)
        {
            //Debug.Log("Button Y Canceled");
        }
    }

    /// <summary>
    /// Permet d'appeler l'input de la Gachette en Haut à Gauche
    /// </summary>
    /// <param name="LeftTopTrigger"></param>
    public void LeftTopTrigger(CallbackContext LeftTopTrigger)
    {
        trigger_LeftTopValue = LeftTopTrigger.ReadValue<float>();
        if (LeftTopTrigger.started)
        {
            //Debug.Log("Button LeftTopTrigger Started");
        }

        if (LeftTopTrigger.performed)
        {
            if (_LeftTop_isDash)
            {
                playerAttribut.Dash();
            }

            if (_LeftTop_isAttack)
            {
                playerAttribut.AttackTypeX();
            }

            if (_LeftTop_isProjectile)
            {
                //playerAttribut.LaunchProjectile();
            }
            //Debug.Log("Button LeftTopTrigger Performed");
        }

        if (LeftTopTrigger.canceled)
        {
            //Debug.Log("Button LeftTopTrigger Canceled");
        }
    }

    /// <summary>
    /// Permet d'appeler l'input de la Gachette en Bas à Gauche
    /// </summary>
    /// <param name="LeftBottomTrigger"></param>
    public void LeftBottomTrigger(CallbackContext LeftBottomTrigger)
    {
        trigger_LeftBottomValue = LeftBottomTrigger.ReadValue<float>();
        if (LeftBottomTrigger.started)
        {
            //Debug.Log("Button LeftBottomTrigger Started");
        }

        if (LeftBottomTrigger.performed)
        {
            if (_LeftBottom_isDash)
            {
                playerAttribut.Dash();
            }

            if (_LeftBottom_isAttack)
            {
                playerAttribut.AttackTypeX();
            }

            if (_LeftBottom_isProjectile)
            {
                //playerAttribut.LaunchProjectile();
            }
            //Debug.Log("Button LeftBottomTrigger Performed");
        }

        if (LeftBottomTrigger.canceled)
        {
            //Debug.Log("Button LeftBottomTrigger Canceled");
        }
    }

    /// <summary>
    /// Permet d'appeler l'input de la Gachette en Haut à Droite
    /// </summary>
    /// <param name="RightTopTrigger"></param>
    public void RightTopTrigger(CallbackContext RightTopTrigger)
    {
        trigger_RightTopValue = RightTopTrigger.ReadValue<float>();
        if (RightTopTrigger.started)
        {
            //Debug.Log("Button RightTopTrigger Started");
        }

        if (RightTopTrigger.performed)
        {
            if (_RightTop_isDash)
            {
                playerAttribut.Dash();
            }

            if (_RightTop_isAttack)
            {
                playerAttribut.AttackTypeX();
            }

            if (_RightTop_isProjectile)
            {
                //playerAttribut.LaunchProjectile();
            }
        }

        if (RightTopTrigger.canceled)
        {
            //Debug.Log("Button RightTopTrigger Canceled");
        }
    }

    /// <summary>
    /// Permet d'appeler l'input de la Gachette Bas à Droite
    /// </summary>
    /// <param name="RightBottomTrigger"></param>
    public void RightBottomTrigger(CallbackContext RightBottomTrigger)
    {
        trigger_RightBottomValue = RightBottomTrigger.ReadValue<float>();
        if (RightBottomTrigger.started)
        {
            //Debug.Log("Button RightBottomTrigger Started");
        }

        if (RightBottomTrigger.performed)
        {
            if (_RightBottom_isDash)
            {
                playerAttribut.Dash();
            }

            if (_RightBottom_isAttack)
            {
                playerAttribut.AttackTypeX();
            }

            if (_RightBottom_isProjectile)
            {
                //playerAttribut.LaunchProjectile();
            }
            //Debug.Log("Button RightBottomTrigger Performed");
        }

        if (RightBottomTrigger.canceled)
        {
            //Debug.Log("Button RightBottomTrigger Canceled");
        }
    }

    /// <summary>
    /// Permet d'appeler l'input de la Gachette Bas à Droite
    /// </summary>
    /// <param name="LeftStickPress"></param>
    public void LeftStickPress(CallbackContext LeftStickPresss)
    {
        stick_LeftPressValue = LeftStickPresss.ReadValue<float>();
        if (LeftStickPresss.started)
        {
            //Debug.Log("Button RightBottomTrigger Started");
        }

        if (LeftStickPresss.performed)
        {
            if (_LeftPress_isDash)
            {
                playerAttribut.Dash();
            }

            if (_LeftPress_isAttack)
            {
                playerAttribut.AttackTypeX();
            }

            if (_LeftPress_isProjectile)
            {
                //playerAttribut.LaunchProjectile();
            }

            if (_LeftPress_IsUlt)
            {
                playerAttribut.LaunchUltimate();
            }
            //Debug.Log("Button RightBottomTrigger Performed");
        }

        if (LeftStickPresss.canceled)
        {
            //Debug.Log("Button RightBottomTrigger Canceled");
        }
    }

    /// <summary>
    /// Permet d'appeler l'input de la Gachette Bas à Droite
    /// </summary>
    /// <param name="RightStickPress"></param>
    public void RightStickPress(CallbackContext RightStickPress)
    {
        stick_RightPressValue = RightStickPress.ReadValue<float>();
        if (RightStickPress.started)
        {
            //Debug.Log("Button RightBottomTrigger Started");
        }

        if (RightStickPress.performed)
        {
            if (_RightPress_isDash)
            {
                playerAttribut.Dash();
            }

            if (_RightPress_isAttack)
            {
                playerAttribut.AttackTypeX();
            }

            if (_RightPress_isProjectile)
            {
                //playerAttribut.LaunchProjectile();
            }

            if (_RightPress_IsUlt)
            {
                playerAttribut.LaunchUltimate();
            }
        }

        if (RightStickPress.canceled)
        {
            //Debug.Log("Button RightBottomTrigger Canceled");
        }
    }

    /// <summary>
    /// Permet d'appeler l'input du Stick Gauche
    /// </summary>
    /// <param name="leftStick"></param>
    public void OnMove(CallbackContext leftStick)
    {
        moveValue = leftStick.ReadValue<Vector2>();
        //if (leftStick.performed)
        //{
        playerAttribut.SetInputVector(moveValue, false);
    }

    //if (leftStick.canceled)
    //{
    //}


    /// <summary>
    /// Permet d'appeler l'input du Stick Droit
    /// </summary>
    /// <param name="rightStick"></param>
    /// 
    /*
    public void OnLook(CallbackContext rightStick)
    {
        lookValue = rightStick.ReadValue<Vector2>();
        lookLocker = new Vector2(lookValue.x, lookValue.y);
        _MousePos = _Camera.ScreenToWorldPoint(lookValue);
        playerAttribut.SetInputVector(kbMouse ? _MousePos : lookLocker, true);
    }
    */

    public void Reset()
    {
        //Begin----------
        kbMouse = false;
        playerAttribut = GetComponent<PlayerAttribut>();
        _Camera = GetComponentInChildren<Camera>();

        //Dash Attribut----------
        _A_isDash = true;
        _RightBottom_isDash = true;
        //Attack----------
        _X_isAttack = true;
        _LeftBottom_isAttack = true;
        //Projectile----------
        _B_isProjectile = true;
        _RightTop_isProjectile = true;
    }
}