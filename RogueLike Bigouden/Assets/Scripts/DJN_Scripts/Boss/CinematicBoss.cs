using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [HideInInspector] public bool transiEnded;

    [Header("End Cinematic")] 
    public DialogueSO endCinematicDialogue;
    [HideInInspector] public bool endEnded;

    void Start()
    {
        #region Instance
        if (instance != null && instance != this)
            Destroy(gameObject); // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
        #endregion
        
        _uiManager = FindObjectOfType<UIManager>();
       
    }

    public IEnumerator StartCinematic()
    {
        isCinematic = true;
        PlayerInput_Final.instance.enabled = false;
        PlayerStatsManager.playerStatsInstance.isInvincible = true;
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerLocation.DOMove(stageFrontLocation.position, speed);
        yield return new WaitForSeconds(speed);
        StartCoroutine(LoadDialogue(startCinematicDialogue));

    }

    public IEnumerator TransitionCinematic()
    {
        isCinematic = true;
        PlayerInput_Final.instance.enabled = false;
        PlayerStatsManager.playerStatsInstance.isInvincible = true;
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerLocation.DOMove(stageFrontLocation.position, speed);
        yield return new WaitForSeconds(speed);
        StartCoroutine(LoadDialogue(transitionCinematicDialogue));
    }

    public IEnumerator EndCinematic()
    {
        isCinematic = true;
        PlayerInput_Final.instance.enabled = false;
        PlayerStatsManager.playerStatsInstance.isInvincible = true;
        playerLocation = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerLocation.DOMove(stageFrontLocation.position, speed);
        yield return new WaitForSeconds(speed);
        StartCoroutine(LoadDialogue(endCinematicDialogue));
    }
 
    IEnumerator LoadDialogue(DialogueSO dialogueSelect)
    {
        _uiManager.dialogueBox.SetActive(true);
        _uiManager.dialogueText.DOText(dialogueSelect.dialogueLine[i], time);
        yield return new WaitForSeconds(time + 0.5f);
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
            isCinematic = false;
            PlayerStatsManager.playerStatsInstance.isInvincible = false;
            
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

            PlayerInput_Final.instance.enabled = true;
            StopCoroutine(LoadDialogue(dialogueSelect));
        }
    }
}
