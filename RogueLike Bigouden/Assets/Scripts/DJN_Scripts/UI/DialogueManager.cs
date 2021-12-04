using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private UIManager uiManager;
    public DialogueSO[] dialogue;
    public DialogueSO selectDialogue;
    public GameObject triggerFeedback;
    public int line;
    
    public Activation activaction;
    private bool dialActive;

    private bool inputPressed;
    //Trigger
    private bool lookForTrigger = false;
    private bool triggered;
    
    //Event
    private bool lookForEvent = false; 
    public enum Activation{Trigger, Event}

    private PlayerInput_Final _playerInputFinal;

    void Start()
    {
        triggerFeedback.SetActive(false);
        uiManager = FindObjectOfType<UIManager>();
        selectDialogue = dialogue[UnityEngine.Random.Range(0, dialogue.Length)];
    }

    private void Update()
    {
        switch (activaction)
        {
            case Activation.Trigger:
                lookForTrigger = true;
                break;
            
            case Activation.Event:
                lookForEvent = true;
                break;
        }

        if (_playerInputFinal)
        {
            if (_playerInputFinal.trigger_RightTopStarted)
            {
                uiManager.dialogueText.text = selectDialogue.dialogueLine[line];
                line++;
            }
        }
       
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerStatsManager>() && lookForTrigger)
        {
            _playerInputFinal = other.GetComponent<PlayerInput_Final>();
            selectDialogue = dialogue[UnityEngine.Random.Range(0, dialogue.Length)];
            triggerFeedback.SetActive(true);
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerStatsManager>())
        {
            triggerFeedback.SetActive(true);
            triggered = false;
            line = 0;
        }
    }
}
