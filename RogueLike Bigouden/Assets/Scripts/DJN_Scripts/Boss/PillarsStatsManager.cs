using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarsStatsManager : MonoBehaviour
{
        public int health;
        public int shieldPoint;
        public bool isDestroyed;
        
        public void TakeDamage(int damage)
        {
                if (shieldPoint > 0)
                { 
                        damage -= shieldPoint;

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
                        isDestroyed = true;
                }
        }
}
