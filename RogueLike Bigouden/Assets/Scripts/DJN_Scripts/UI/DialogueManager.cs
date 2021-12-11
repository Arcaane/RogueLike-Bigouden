using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueSO[] dialogue;
    public DialogueSO selectDialogue;
    public GameObject triggerFeedback;

    private PlayerAttribut player;
    

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

    private void OnTriggerExit2D(Collider2D other)
    {
        player.CloseDialogue();
        player.canTalk = false;
        triggerFeedback.SetActive(false);
    }
}
