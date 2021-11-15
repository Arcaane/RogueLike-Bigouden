using System;
using UnityEngine;

public class RoomLoader : MonoBehaviour
{
    // Scene State
    public GameObject[] doors;
    public int numberOfEnnemies;
    public LayerMask isEnnemy;
    public GameObject _waveManager;
    private bool stopCheckEnemies;

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
                numberOfEnnemies++;
            }

            if (numberOfEnnemies == 0)
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