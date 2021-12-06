using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class Player_FeedBack : MonoBehaviour
{
    
    // Utilities
    public static Player_FeedBack fb_instance;
    [SerializeField] public PlayerAttribut p_attribut;
    // Camera Move
    [SerializeField] public Transform p_transform;
    [SerializeField] private float smoothSpeedCamDash = 2.5f;
    [SerializeField] private float smoothSpeedCamWalk = 1.7f;


    private void Awake()
    {
        if (fb_instance == null){

            fb_instance = this;
            DontDestroyOnLoad(this.gameObject);
    
            //Rest of your Awake code
    
        } else {
            Destroy(this);
        }
    }

    public Vector3 offset;
    
    private void LateUpdate()
    {
        Debug.Log(p_attribut._isDashing);
        
        if (p_attribut._isDashing)
        {
            Vector3 desiredPos = p_transform.position + offset; 
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeedCamDash * Time.deltaTime);
            transform.position = smoothedPos;
        }
        else
        {
            Vector3 desiredPos = p_transform.position + offset; 
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeedCamWalk * Time.deltaTime);
            transform.position = smoothedPos;
        }
        
    }
}