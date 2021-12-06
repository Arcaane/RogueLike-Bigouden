using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FeedBack : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform p_transform;

    private void Awake()
    {
        mainCam.transform.position = p_transform.position;
    }
}
    

