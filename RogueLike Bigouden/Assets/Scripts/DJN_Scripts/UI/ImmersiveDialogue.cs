using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ImmersiveDialogue : MonoBehaviour
{
    public DialogueSO[] dialogues;
    public DialogueSO dialogueSelect;

    private float timer;
    public float timeToDraw;
    public float drawSpeed;
    public float timeToScreen;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToDraw)
        {
            StartDialogue();
            Debug.Log("ImmersiveLoad");
            timer = 0;
        }
    }

    public void StartDialogue()
    {
        dialogueSelect = dialogues[UnityEngine.Random.Range(0, dialogues.Length)];
        StartCoroutine(LoadImmersiveDialogue());

    }
    
    public IEnumerator LoadImmersiveDialogue()
    {
        UIManager.instance.immersiveDialogue.DOText(dialogueSelect.dialogueLine[0], drawSpeed);
        yield return new WaitForSeconds(timeToScreen);
        UIManager.instance.immersiveDialogue.text = String.Empty;
    }
}
