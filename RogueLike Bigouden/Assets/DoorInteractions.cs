using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractions : MonoBehaviour
{
    public bool isRoomClear;
    public Animator animatorDoor;
    private static readonly int Enter = Animator.StringToHash("Enter");
    private static readonly int Open = Animator.StringToHash("Open");
    private static readonly int Close = Animator.StringToHash("Close");

    public void Start()
    {
        animatorDoor.SetBool(Open, false);
        animatorDoor.SetBool(Close, false);
    }

    public void LateUpdate()
    {
        if (animatorDoor != null)
        {
            if (isRoomClear)
            {
                animatorDoor.SetBool(Open, true);
                animatorDoor.SetBool(Close, false);
            }
        }
        else
        {
            //animatorDoor.SetBool(Open, false);
        }
    }


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