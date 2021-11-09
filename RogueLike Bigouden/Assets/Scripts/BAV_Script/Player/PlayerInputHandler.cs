using System.Diagnostics;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;
using Debug = UnityEngine.Debug;

public class PlayerInputHandler : MonoBehaviour
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
        playerConfig.Input.onActionTriggered += ControlTrigger;
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
    /// Permet de déclencher le déplacement.
    /// </summary>
    /// <param name="obj"></param>
    private void ControlTrigger(CallbackContext obj)
    {
        
        //JoystickGauche---------------------------------------------
        if (obj.action.name == controls.Player.Move.name)
        {
            OnMove(obj);
        }

        //JoystickDroit---------------------------------------------
        if (obj.action.name == controls.Player.Look.name)
        {
            OnLook(obj);
        }

        //BouttonX---------------------------------------------
        if (obj.action.name == controls.Player.XButton.name)
        {
            if (obj.performed)
            {
                playerAttribut.attackPath.launchAttack = true;
            }
        }

        //BouttonY---------------------------------------------
        if (obj.action.name == controls.Player.YButton.name)
        {
            if (obj.performed)
            {

            }
        }
        
        //BouttonA---------------------------------------------
        if (obj.action.name == controls.Player.AButton.name)
        {
            if (obj.started)
            {
            }

            if (obj.performed)
            {
                playerAttribut.Dash();
            }

            if (obj.canceled)
            {
                //playerAttribut.Dash();
            }
        }
        
        //BouttonB---------------------------------------------
        if (obj.action.name == controls.Player.BButton.name)
        {
            if (obj.performed)
            {
                playerAttribut.Dash();
            }
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