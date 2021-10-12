using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance playerInstance;
    public List<GameObject> Player;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (playerInstance == null)
            playerInstance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        Player.Add(GameObject.Find("Player 1"));
        Player.Add(GameObject.Find("Player 2"));
    }
}
