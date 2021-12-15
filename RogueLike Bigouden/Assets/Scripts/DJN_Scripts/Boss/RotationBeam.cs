using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBeam : MonoBehaviour
{
    private LineRenderer line;
    private RaycastHit2D hit;

    public Transform startPoint;
    public Transform ghostTarget;

    public Transform[] keyPoints;

    public int damage;
    public float raySpeed;
    public float startingTime;
    public LayerMask layerMask;
    
    private PlayerStatsManager _playerStatsManager;
    private bool pHit;
    
    private bool isMoving;
    [HideInInspector] public bool isActive;

    private void Update()
    {
        if (isActive)
        {
            LoadRBeam();
            line.enabled = true;
        }
    
    }

    void LoadRBeam()
    {
        
            hit = Physics2D.Linecast(startPoint.position, ghostTarget.position, layerMask);
            
            line.SetPosition(0, startPoint.position);

            if (hit)
            {
                line.SetPosition(1, hit.point);

                if (hit.collider.GetComponent<PlayerStatsManager>())
                {
                    _playerStatsManager = hit.collider.GetComponent<PlayerStatsManager>();

                    if (!pHit)
                    {
                        _playerStatsManager.TakeDamage(damage);
                        pHit = true;
                    }
                }
            }
            else
            {
                line.SetPosition(1, ghostTarget.position);
                pHit = false;
            }

            StartCoroutine(MoveBeam());
    }

    IEnumerator MoveBeam()
    {
        yield return new WaitForSeconds(startingTime);

        int p = 0;

        if (ghostTarget.position != keyPoints[keyPoints.Length].position)
        {
            ghostTarget.position =
                Vector2.MoveTowards(ghostTarget.position, keyPoints[p].position, raySpeed * Time.deltaTime);
        
            if (ghostTarget.position == keyPoints[p].position)
            {
                p++;
            }
        }
        
        if (ghostTarget.position == keyPoints[keyPoints.Length].position)
        {
            line.enabled = false;
            isActive = false;
        }
        
    }
}
