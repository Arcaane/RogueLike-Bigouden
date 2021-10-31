using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public BAV_PlayerController playerConfig;
    // Start is called before the first frame update
    void Start()
    {
        playerConfig = new BAV_PlayerController();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
