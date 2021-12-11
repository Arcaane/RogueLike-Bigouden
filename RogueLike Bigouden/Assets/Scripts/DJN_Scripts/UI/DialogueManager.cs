using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueSO[] dialogue;
    public DialogueSO selectDialogue;
    public GameObject triggerFeedback;
    public int line;

    private PlayerAttribut player;
    
    
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
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerAttribut>() != null)
        {
            player = other.GetComponent<PlayerAttribut>();
            triggerFeedback.SetActive(true);
            player.canTalk = true;
        }
    }
}
