using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public Material Bigoumat;

    private void Start()
    {
        PlayerConfigurationManager.Instance.ReadyPlayer(0);
        PlayerConfigurationManager.Instance.SetPlayerColor(0, Bigoumat);
    }
}