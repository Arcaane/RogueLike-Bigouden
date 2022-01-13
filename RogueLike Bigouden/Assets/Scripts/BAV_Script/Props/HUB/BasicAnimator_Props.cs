using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimator_Props : MonoBehaviour
{
    public float damageToLaunchHitAnim = 1;
    public Props_EnvironnementManager props;
    public Animator animator;

    private PlayerStatsManager playerStats;
    private BoxCollider2D box;

    public void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject objtrigger = other.gameObject;
        PlayerStatsManager objManager = objtrigger.GetComponent<PlayerStatsManager>();
        if (objtrigger.CompareTag("Weapon"))
        {
            animator.SetTrigger("Hit");
        }
    }
}
