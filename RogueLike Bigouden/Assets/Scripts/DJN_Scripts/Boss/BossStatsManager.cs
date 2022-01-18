using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class BossStatsManager : MonoBehaviour
{
    public int health;
    public int shieldPoint;
    public bool isDead;
    private Animator _animator;
    public bool getHurt;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (PaternTimer.instance.phase2)
        {
            if (health > 0 )
            {
                if (shieldPoint > 0)
                {
                    shieldPoint -= damage;

                    if (shieldPoint < 0)
                    {
                        shieldPoint = 0;
                    }
                }
                else
                {
                    health -= damage;
                }

                getHurt = true;
                _animator.Play("DJ_Hurt");
            
            }

            if (health <= 0)
            {
                _animator.Play("DJ_Die");
                isDead = true;
            }
        }
        
    }

    public void ResetHurt()
    {
        getHurt = false;
    }
}
