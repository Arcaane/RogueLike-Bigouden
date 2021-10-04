using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    //private int playerID;
    //public PlayerInputManager playerInputManager;

    public GameObject[] spawnPoints;
    public List<PlayerInput> playerList = new List<PlayerInput>();
    [SerializeField] InputAction jointAction;
    [SerializeField]  InputAction leaveAction;
    [Range(1,4)]
    [SerializeField] int maxPlayerCount = 2;

    //INSTANCES
    public static GameManager instance = null;

    //EVENTS 
    public event System.Action<PlayerInput> PlayerJoinedGame;
    public event System.Action<PlayerInput> PlayerLeftGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        jointAction.Enable();
        jointAction.performed += context => JoinAction(context);
        
        leaveAction.Enable();
        leaveAction.performed += context => LeaveAction(context);
    }

    private void Start()
    {
        //PlayerInputManager.instance.JoinPlayer(0, -1, null);

    }


    void OnPlayerJoined(PlayerInput playerInput)
    {
        playerList.Add(playerInput);
        if (PlayerJoinedGame != null)
        {
            PlayerJoinedGame(playerInput);
        }
    }

    /*
    private void Update()
    {
        //playerInputManager.CheckIfPlayerCanJoin(playerID);
    }*/

    void JoinAction(InputAction.CallbackContext context)
    {
        PlayerInputManager.instance.JoinPlayerFromActionIfNotAlreadyJoined(context);
    }
    
    void LeaveAction(InputAction.CallbackContext context)
    {
        
    }
}