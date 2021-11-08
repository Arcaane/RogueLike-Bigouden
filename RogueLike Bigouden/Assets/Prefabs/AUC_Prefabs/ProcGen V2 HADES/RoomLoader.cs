using System;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    // Scene State
    public bool IsRoomClear;
    public int numberOfEnnemies;
    public LayerMask isEnnemy;

    private void Start()
    {
        CheckforEnnemies();
        InvokeRepeating(nameof(CheckforEnnemies), 5, 1.3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (IsRoomClear)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    LoadManager.LoadManagerInstance.ChangeRoom();
                }
            } 
        }
    }
    
    private void CheckforEnnemies()
    {
        numberOfEnnemies = 0;
        Collider2D[] ennemyInRoom = Physics2D.OverlapCircleAll(transform.position, 20f, isEnnemy);
        foreach (var ctx in ennemyInRoom)
        {
            numberOfEnnemies++;
        }

        if (numberOfEnnemies == 0)
        {
            IsRoomClear = true;
        }
    }
}