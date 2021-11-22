using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractions : MonoBehaviour
{
    public bool isRoomClear;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isRoomClear)
        {
            LoadManager.LoadManagerInstance.ChangeRoom();
        }
    }
}
