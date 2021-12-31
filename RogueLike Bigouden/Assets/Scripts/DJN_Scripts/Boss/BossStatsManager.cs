using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class BossStatsManager : MonoBehaviour
{
    public int health;
    public int shieldPoint;
    public bool isDead;

    public void TakeDamage(int damage)
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

        if (health <= 0)
        {
            isDead = true;
        }
    }
}
