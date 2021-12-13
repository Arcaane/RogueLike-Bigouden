using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfacesScript : MonoBehaviour
{
    public GameObject firstSelectedButton;
    private void OnEnable()
    {
        FindObjectOfType<EventSystem>().SetSelectedGameObject(firstSelectedButton);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("BAV_HUB_BED");
    }

    public void Quit()
    {
        Application.Quit();
    }
}