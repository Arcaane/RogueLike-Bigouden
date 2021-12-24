using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CinematicBoss : MonoBehaviour
{
    private bool isCinematic;
    private int i;
    public float time;
    
    [Header("Start Cinematic")]
    private Transform playerLocation;
    private Transform stageFrontLocation;
    public DialogueSO startCinematicDialogue;
    private UIManager _uiManager;

    [Header("Tansition Cinematic")]
    public DialogueSO transitionCinematicDialogue;

    [Header("End Cinematic")] 
    public DialogueSO endCinematicDialogue;

    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>();
        isCinematic = true;
    }

    void StartCinematic()
    {
        //SCENE LOAD, PLAYER CAN'T CONTROLL THE CHARACTER
        
        //PLAYER DO MOVE TO THE FRONT OF THE STAGE
        playerLocation.DOMove(stageFrontLocation.position, 1f);
        //WHEN HE'S ON THE POINT, DIALOGUE BOSS START
        StartCoroutine(LoadDialogue(startCinematicDialogue));
        //AT THE END OF DIALOGUE, FEW SECONDS AND THE PLAYER IS ALLOW TO MOVE BY HIS OWN
    }

    void TransitionCinematic()
    {
        //WHEN P1 IS OVER, ENTER THE TRANSITION PHASE = CINEMATIC
        //PLAYER CAN'T CONTROLL THE CHARACTER AND A BOSS DIALOGUE STRAT
        StartCoroutine(LoadDialogue(transitionCinematicDialogue));
        //DIALOGUE LINES SKIP AFTER A TIME IN SECONDS
        //AT THE END OF DIALOGUE, FEW SECONDS AND THE PLAYER IS ALLOW TO MOVE BY HIS OWN
    }

    void EndCinematic()
    {
        //WHEN THE BOSS IS DOWN, PLAYER LOSE CONTROL OF THE CHARACTER AND DIALOGUE LOAD
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
        if (i <= dialogueSelect.dialogueLine.Length)
        {
            StartCoroutine(LoadDialogue(dialogueSelect));
        }
        else if(i > dialogueSelect.dialogueLine.Length)
        {
            i = 0;
            StopCoroutine(LoadDialogue(dialogueSelect));
        }
    }
}
