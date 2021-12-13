using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimator_Props : MonoBehaviour
{
    public Props_EnvironnementManager props;
    public Animator animator;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            props.TakeDamagePropsDestruction(1);
            animator.SetTrigger("Hit");
        }
    }
}
