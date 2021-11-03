using System;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    // Scene State
    public bool isRoomClear;
    public int numberOfEnnemies;
    public LayerMask isEnnemy;
    
    private void Start()
    {
        Collider2D[] ennemyInRoom = Physics2D.OverlapCircleAll(transform.position, 20f, isEnnemy);
        foreach (var ctx in ennemyInRoom)
        {
            numberOfEnnemies++;
        }

        if (numberOfEnnemies == 0)
        {
            isRoomClear = true;
        }
        
        InvokeRepeating(nameof(CheckforEnnemies), 5, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        
        if (isRoomClear)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                LoadManager.LoadManagerInstance.ChangeRoom();
            }
                
        }
    }

    private void CheckforEnnemies()
    {
        Collider2D[] ennemyInRoom = Physics2D.OverlapCircleAll(transform.position, 20f, isEnnemy);
        foreach (var ctx in ennemyInRoom)
        {
            numberOfEnnemies++;
        }

        if (numberOfEnnemies == 0)
        {
            isRoomClear = true;
        }
    }
}