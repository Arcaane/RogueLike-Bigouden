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
    private bool isOnDial;
    private bool dialIsEnded;

    void Start()
    {
        triggerFeedback.SetActive(false);
        uiManager = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<UIManager>();
        isOnDial = false;
        dialIsEnded = false;
        selectDialogue = dialogue[UnityEngine.Random.Range(0, dialogue.Length)];
        Debug.Log("Dialogue selected is " + selectDialogue.name);
    }

    private void Update()
    {
        if (isOnDial)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) && !dialIsEnded)
            {
                line++;
                uiManager.dialogueText.text = selectDialogue.dialogueLine[line];
            }
        }
        
        if (line == selectDialogue.dialogueLine.Length - 1 && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            dialIsEnded = true;
        }
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            triggerFeedback.SetActive(true);

            if (Input.GetKey(KeyCode.KeypadEnter))
            {
                uiManager.dialogueBox.SetActive(true);
                isOnDial = true;
                uiManager.dialogueText.text = selectDialogue.dialogueLine[line];
            }
        }
             
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        triggerFeedback.SetActive(false);
        uiManager.dialogueBox.SetActive(false);
        line = 0;
        dialIsEnded = false;
        isOnDial = false;
        
        selectDialogue = dialogue[UnityEngine.Random.Range(0, dialogue.Length)];
        Debug.Log("Dialogue selected is " + selectDialogue.name);
    }
}
