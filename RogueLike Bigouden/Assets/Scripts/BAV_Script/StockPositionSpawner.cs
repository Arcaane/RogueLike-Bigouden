using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class StockPositionSpawner : MonoBehaviour
{
    public PlayerInputManager inputManager;
    public int numberOfPlayers;
    [SerializeField] private bool switchParam = false;


    public List<Transform> spawnPoint;

    //Spawn
    //public Transform spawnRingCenter;
    //public float spawnRingRadius;

    private void Awake()
    {
        switchParam = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        SpawnPoint();
    }
    
    void SpawnPoint()
    {
        inputManager.playerPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.red;
        inputManager.playerPrefab.transform.position = spawnPoint[0].transform.position;
        numberOfPlayers++;
        if (inputManager.JoinPlayer(1,1,null,Gamepad.current))
        {
            inputManager.playerPrefab.GetComponent<Renderer>().sharedMaterial.color = Color.blue;
            inputManager.playerPrefab.transform.position = spawnPoint[1].transform.position;
        }
    }
    

    private void OnDrawGizmos()
    {
        for (int i = 0; i < spawnPoint.Count; i++)
        {
            Handles.DrawWireDisc(spawnPoint[i].transform.position, new Vector3(0, 0, 90f), 3);
        }
    }
}