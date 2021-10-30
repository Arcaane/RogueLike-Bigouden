using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StartMenuUIManager : MonoBehaviour
{
    [Header("Boutton Nombre de Joueur")] [SerializeField]
    private List<Button> selectNumberOfPlayer;

    [Header("Menu UI de  ")] [SerializeField]
    private List<GameObject> menuStart;

    [SerializeField] private List<GameObject> osefController;

    private int numberPlayer;

    private void Awake()
    {
        menuStart[0].SetActive(true);
        for (int i = 0; i < osefController.Count; i++)
        {
            osefController[0].GetComponent<PlayerInputManager>().DisableJoining();
            osefController[1].SetActive(false);
        }
    }


    public void GoNumberOfPlayer(GameObject uiObject)
    {
        uiObject.SetActive(true);
        for (int i = 0; i < selectNumberOfPlayer.Count; i++)
        {
            menuStart[0].SetActive(false);
            selectNumberOfPlayer[i].interactable = true;
            selectNumberOfPlayer[i].Select();
        }
    }

    public void ChangeNumberOfPlayerToLaunch(int numberPlayer)
    {
        menuStart[1].SetActive(false);
        for (int i = 0; i < osefController.Count; i++)
        {
            osefController[0].GetComponent<PlayerConfigurationManager>().MaxPlayers = numberPlayer;
            osefController[0].GetComponent<PlayerInputManager>().EnableJoining();
            osefController[1].SetActive(true);
        }
    }
}