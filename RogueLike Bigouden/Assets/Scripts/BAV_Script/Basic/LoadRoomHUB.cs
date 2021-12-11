using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRoomHUB : MonoBehaviour
{
    public string sceneToLoad;
    public float duration;
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(Fade());
        LoadManager.LoadManagerInstance.launchAnimator.SetBool("Enter", true);
    }
    IEnumerator Fade()
    {
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(sceneToLoad);
    }
    
}
