using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractions : MonoBehaviour
{
    public bool isRoomClear;
    private static readonly int Enter = Animator.StringToHash("Enter");

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isRoomClear)
        {
            StartCoroutine(Fade());
            LoadManager.LoadManagerInstance.launchAnimator.SetBool("Enter", true);
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(LoadManager.LoadManagerInstance.transitionDuration);
        LoadManager.LoadManagerInstance.ChangeRoom();
    }
}