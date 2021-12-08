using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player_FeedBack : MonoBehaviour
{
    
    // Utilities
    public static Player_FeedBack fb_instance;
    [SerializeField] public PlayerAttribut p_attribut;
    
    // Camera Move
    [SerializeField] public Transform p_transform;
    [SerializeField] private float smoothSpeedCamDash = 2.5f;
    [SerializeField] private float smoothSpeedCamWalk = 1.7f;
    
    // Camera Shake 
    [SerializeField] private float s_duration;
    [SerializeField] private float s_magnitude;
    
    // MM Feedback
    [SerializeField] private MMFeedbacks _mmFeedbacks;


    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = Vector3.Lerp(transform.position, new Vector3(transform.position.x + x, transform.position.y + y, originalPos.z), duration * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    
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
        if (Input.GetMouseButtonDown(0))
        {
           //StartCoroutine(Shake(s_duration, s_magnitude));
           _mmFeedbacks.PlayFeedbacks();
        }

        if (p_attribut != null)
        {
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
    
}