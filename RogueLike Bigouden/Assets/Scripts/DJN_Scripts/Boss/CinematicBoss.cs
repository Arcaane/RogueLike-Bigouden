using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicBoss : MonoBehaviour
{
    public static CinematicBoss instance;
    
    [Header("Informations")]
    public bool isCinematic;
    private int i;
    public float time;
    
    [Header("Start Cinematic")]
    public Transform stageFrontLocation;
    public DialogueSO startCinematicDialogue;
    private UIManager _uiManager;
    public float speed;
    private Transform playerLocation;
    [HideInInspector] public bool startEnded;

    [Header("Tansition Cinematic")]
    public DialogueSO transitionCinematicDialogue;

    public bool transiEnded;

    [Header("End Cinematic")] 
    public DialogueSO endCinematicDialogue;

    public bool endEnded;

    public bool sceneEnded;
    void Start()
    {
        if (instance != null && instance != this)
            Destroy(gameObject); // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
        
        _uiManager = FindObjectOfType<UIManager>();
       
    }

    public IEnumerator StartCinematic()
    {
        
       
        
        isCinematic = true;
        //SCENE LOAD, PLAYER CAN'T CONTROLL THE CHARACTER
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //PLAYER DO MOVE TO THE FRONT OF THE STAGE
        playerLocation.DOMove(stageFrontLocation.position, speed);
        //WHEN HE'S ON THE POINT, DIALOGUE BOSS START
        yield return new WaitForSeconds(speed);
        StartCoroutine(LoadDialogue(startCinematicDialogue));
        //AT THE END OF DIALOGUE, FEW SECONDS AND THE PLAYER IS ALLOW TO MOVE BY HIS OWN

    }

    public IEnumerator TransitionCinematic()
    {
        isCinematic = true;
        //WHEN P1 IS OVER, ENTER THE TRANSITION PHASE = CINEMATIC
        //PLAYER CAN'T CONTROLL THE CHARACTER AND A BOSS DIALOGUE STRAT
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //PLAYER DO MOVE TO THE FRONT OF THE STAGE
        playerLocation.DOMove(stageFrontLocation.position, speed);
        //WHEN HE'S ON THE POINT, DIALOGUE BOSS START
        yield return new WaitForSeconds(speed);
        StartCoroutine(LoadDialogue(transitionCinematicDialogue));
        //DIALOGUE LINES SKIP AFTER A TIME IN SECONDS
        //AT THE END OF DIALOGUE, FEW SECONDS AND THE PLAYER IS ALLOW TO MOVE BY HIS OWN
    }

    public IEnumerator EndCinematic()
    {
        isCinematic = true;
        //WHEN THE BOSS IS DOWN, PLAYER LOSE CONTROL OF THE CHARACTER AND DIALOGUE LOAD
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //PLAYER DO MOVE TO THE FRONT OF THE STAGE
        playerLocation.DOMove(stageFrontLocation.position, speed);
        //WHEN HE'S ON THE POINT, DIALOGUE BOSS START
        yield return new WaitForSeconds(speed);
        StartCoroutine(LoadDialogue(endCinematicDialogue));
        //DIALOGUE LINES SKIP AFTER A TIME IN SECONDS
        //AT THE END OF DIALOGUE, SCREEN FADE TO BLACK AND LOAD SCENE CREDITS_END
    }
 
    IEnumerator LoadDialogue(DialogueSO dialogueSelect)
    {
        _uiManager.dialogueBox.SetActive(true);
        _uiManager.dialogueText.DOText(dialogueSelect.dialogueLine[i], time);
        yield return new WaitForSeconds(time);
        i++;
        if (i < dialogueSelect.dialogueLine.Length)
        {
            _uiManager.dialogueText.text = String.Empty;
            StartCoroutine(LoadDialogue(dialogueSelect));
        }
        else if(i >= dialogueSelect.dialogueLine.Length)
        {
            _uiManager.dialogueText.text = String.Empty;
            _uiManager.dialogueBox.SetActive(false);
            i = 0;
            sceneEnded = true;
            isCinematic = false;

            if (dialogueSelect == startCinematicDialogue)
            {
                startEnded = true;
            }

            if (dialogueSelect == transitionCinematicDialogue)
            {
                transiEnded = true;
            }

            if (dialogueSelect == endCinematicDialogue)
            {
                endEnded = true;
            }
            StopCoroutine(LoadDialogue(dialogueSelect));
        }
    }
}
