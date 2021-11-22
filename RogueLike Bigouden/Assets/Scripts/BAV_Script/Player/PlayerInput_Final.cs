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
    /// Permet d'appeler l'input du Boutton A
    /// </summary>
    /// <param name="buttonA"></param>
    public void Input_AButton(CallbackContext buttonA)
    {
        buttonAValue = buttonA.ReadValue<float>();
        if (buttonA.started)
        {
            Debug.Log("Button A Started" + buttonAValue);
        }

        if (buttonA.performed)
        {
                playerAttribut.Dash();
                Debug.Log("Button A performed");
        }

        if (buttonA.canceled)
        {
            Debug.Log("Button A Canceled");
        }
    }


    /// <summary>
    /// Permet d'appeler l'input du Boutton B
    /// </summary>
    /// <param name="buttonB"></param>
    public void Input_BButton(CallbackContext buttonB)
    {
        buttonBValue = buttonB.ReadValue<float>();
        if (buttonB.started)
        {
            Debug.Log("Button B Started");
        }

        if (buttonB.performed)
        {
            Debug.Log("Button B Performed");
        }

        if (buttonB.canceled)
        {
            Debug.Log("Button B Canceled");
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
            Debug.Log("Button X Started");
        }

        if (buttonX.performed)
        {
            playerAttribut.AttackTypeX();
            Debug.Log("Button X Performed");
        }

        if (buttonX.canceled)
        {
            Debug.Log("Button X Canceled");
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
            Debug.Log("Button Y Started");
        }

        if (buttonY.performed)
        {
            Debug.Log("Button Y Performed");
        }

        if (buttonY.canceled)
        {
            Debug.Log("Button Y Canceled");
        }
    }
    
    /// <summary>
    /// Permet d'appeler l'input de la Gachette en Haut à Gauche
    /// </summary>
    /// <param name="LeftTopTrigger"></param>
    public void LeftTopTrigger(CallbackContext LeftTopTrigger)
    {
        rightPressTrigger = LeftTopTrigger.ReadValue<float>();
        if (LeftTopTrigger.started)
        {
            Debug.Log("Button LeftTopTrigger Started");
        }

        if (LeftTopTrigger.performed)
        {
            Debug.Log("Button LeftTopTrigger Performed");
        }

        if (LeftTopTrigger.canceled)
        {
            Debug.Log("Button LeftTopTrigger Canceled");
        }
    }

    /// <summary>
    /// Permet d'appeler l'input de la Gachette en Bas à Gauche
    /// </summary>
    /// <param name="LeftBottomTrigger"></param>
    public void LeftBottomTrigger(CallbackContext LeftBottomTrigger)
    {
        rightPressTrigger = LeftBottomTrigger.ReadValue<float>();
        if (LeftBottomTrigger.started)
        {
            Debug.Log("Button LeftBottomTrigger Started");
        }

        if (LeftBottomTrigger.performed)
        {
            Debug.Log("Button LeftBottomTrigger Performed");
        }

        if (LeftBottomTrigger.canceled)
        {
            Debug.Log("Button LeftBottomTrigger Canceled");
        }
    }
    
    /// <summary>
    /// Permet d'appeler l'input de la Gachette en Haut à Droite
    /// </summary>
    /// <param name="RightTopTrigger"></param>
    public void RightTopTrigger(CallbackContext RightTopTrigger)
    {
        rightPressTrigger = RightTopTrigger.ReadValue<float>();
        if (RightTopTrigger.started)
        {
            Debug.Log("Button RightTopTrigger Started");
        }

        if (RightTopTrigger.performed)
        {
            Debug.Log("Button RightTopTrigger Performed");
        }

        if (RightTopTrigger.canceled)
        {
            Debug.Log("Button RightTopTrigger Canceled");
        }
    }
    
    /// <summary>
    /// Permet d'appeler l'input de la Gachette Bas à Droite
    /// </summary>
    /// <param name="RightBottomTrigger"></param>
    public void RightBottomTrigger(CallbackContext RightBottomTrigger)
    {
        rightPressTrigger = RightBottomTrigger.ReadValue<float>();
        if (RightBottomTrigger.started)
        {
            Debug.Log("Button RightBottomTrigger Started");
        }

        if (RightBottomTrigger.performed)
        {
            Debug.Log("Button RightBottomTrigger Performed");
        }

        if (RightBottomTrigger.canceled)
        {
            Debug.Log("Button RightBottomTrigger Canceled");
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