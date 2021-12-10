using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCinematic : MonoBehaviour
{
    private GameObject player;
    private PaternTimer m_paterTimer;
    // Start is called before the first frame update
    void Start()
    {
        m_paterTimer = GetComponent<PaternTimer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
