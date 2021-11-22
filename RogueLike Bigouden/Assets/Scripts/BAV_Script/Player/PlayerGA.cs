using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static UnityEngine.InputSystem.InputAction;

public class PlayerGA : MonoBehaviour
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
        playerConfig.Input.onActionTriggered += Input_MoveTrigger;
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

        switch (obj.action.name == controls.Player.XButton.name)
        {
            case true:

                break;
            case false:
                break;
        }

        switch (obj.action.name == controls.Player.XButton.name)
        {
            case true:

                break;
            case false:
                break;
        }
    }

    #region Appel des différentes Inputs

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
                //playerAttribut.DodgeAttack();
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
    private void Input_BButton(CallbackContext buttonB)
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
                playerAttribut.AttackTypeX();
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
    private void Input_YButton(CallbackContext buttonY)
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
    private void Input_RightTrigger(CallbackContext rightTrigger)
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
    /// <summary>
    /// Permet d'appeler l'input du Stick Droit
    /// </summary>
    /// <param name="rightStick"></param>
    public void OnLook(InputAction.CallbackContext rightStick)
    {
        playerAttribut.SetInputVector(rightStick.ReadValue<Vector2>(), true);
    }

    public void OnMove(CallbackContext leftStick)
    {
        playerAttribut.SetInputVector(leftStick.ReadValue<Vector2>(), false);
        isMoving = true;
    }

    #endregion
}