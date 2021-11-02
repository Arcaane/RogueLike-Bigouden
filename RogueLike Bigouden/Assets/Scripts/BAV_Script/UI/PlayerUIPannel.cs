using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIPannel : MonoBehaviour
{
    private BAV_PlayerController pause;
    private float startButton;
    public bool isPaused;


    private void Awake()
    {
        pause = new BAV_PlayerController();
    }

    private void OnEnable()
    {
        pause.Enable();
    }

    private void OnDisable()
    {
        pause.Disable();
    }

    private void Start()
    {
        pause.UI.PauseGame.performed += Input_Start;
    }

    private void DeterminedPause()
    {
        switch (isPaused)
        {
            case true:
                ResumeGame();
                break;
            case false:
                PauseGame();
                break;
        }
    }

    private void Input_Start(InputAction.CallbackContext pauseButton)
    {
        startButton = pauseButton.ReadValue<float>();
        DeterminedPause();
    }


    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}