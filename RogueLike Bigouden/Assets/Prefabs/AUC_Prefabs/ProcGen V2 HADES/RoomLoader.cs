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
        InvokeRepeating(nameof(CheckforEnnemies), Single.MinValue, 1.7f);
    }
    
    private void CheckforEnnemies()
    {
        if (stopCheckEnemies)
        {
            return;
        }
        else
        {
            Collider2D[] ennemyInRoom = Physics2D.OverlapCircleAll(transform.position, 10f, isEnnemy);
            foreach (var ctx in ennemyInRoom)
            {
                enemyList.Add(ctx.gameObject);
                numberOfEnnemies++;
            }

            foreach (var _e in enemyList)
            {
                if (_e.GetComponent<EnnemyStatsManager>().lifePoint <= 0)
                {
                    enemyList.Remove(_e);
                    numberOfEnnemies--;
                }
            }

            if (enemyList.Count == 0)
            {
                stopCheckEnemies = true;
                _waveManager.SetActive(true);
            }
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