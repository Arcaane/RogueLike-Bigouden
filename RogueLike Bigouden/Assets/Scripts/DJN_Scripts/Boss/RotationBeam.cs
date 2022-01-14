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
     
    public bool isActive;

    public Animator animator;

    public int p;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        p = 0;
        
        ghostTarget.position = keyPoints[0].position;
    }

    private void Update()
    {
        
        if (isActive)
        {
            LoadRBeam();
            line.enabled = true;
            animator.Play("TM_Open");
            animator.GetComponent<BoxCollider2D>().isTrigger = false;
            
        }
        
        if (ghostTarget.position == keyPoints[5].position)
        {
            line.enabled = false;
            isActive = false;
            p = 0;
            animator.GetComponent<BoxCollider2D>().isTrigger = true;
            animator.Play("TM_Close");
        }

        if (FindObjectOfType<BossStatsManager>())
        {
            BossStatsManager bossStat = FindObjectOfType<BossStatsManager>();

            if (bossStat.isDead)
            {
                isActive = false;
                line.enabled = false;
                p = 0;
                animator.GetComponent<BoxCollider2D>().isTrigger = true;
                animator.Play("TM_Close");
            }
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

            if (ghostTarget.position != keyPoints[p].position && p < keyPoints.Length)
            {
                MoveBeam();
            }
            else
            {
                p++;
            }


    }

    private void MoveBeam()
    { 
        
        ghostTarget.position =
                Vector2.MoveTowards(ghostTarget.position, keyPoints[p].position, raySpeed * Time.deltaTime);
        
        
    }
}
