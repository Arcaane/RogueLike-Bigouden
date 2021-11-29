using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    // Scene State
    public GameObject[] doors;
    public int numberOfEnnemies;
    public LayerMask isEnnemy;
    public GameObject _waveManager;
    private bool stopCheckEnemies;
    
    public List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        _waveManager.SetActive(false);
    }

    private void Start()
    {
        CheckforEnnemies();
        InvokeRepeating(nameof(CheckforEnnemies), 5, 1.3f);
    }
    
    private void CheckforEnnemies()
    {
        if (stopCheckEnemies)
        {
            return;
        }
        else
        {
            numberOfEnnemies = 0;
            Collider2D[] ennemyInRoom = Physics2D.OverlapCircleAll(transform.position, 10f, isEnnemy);
            foreach (var ctx in ennemyInRoom)
            {
                enemyList.Add(ctx.gameObject);
                numberOfEnnemies++;
            }

            stopCheckEnemies = true;
        }
        
    }

    public void ClearedRoom()
    {
        foreach (var d  in doors)
        {
            d.GetComponent<DoorInteractions>().isRoomClear = true;
        }
    }
}