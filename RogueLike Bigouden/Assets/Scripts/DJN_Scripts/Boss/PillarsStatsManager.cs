using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarsStatsManager : MonoBehaviour
{
        public Pillar pillar;
        public enum Pillar{TopLeft, TopRight, DownLeft, DownRight}
        public Beam childBeam;
        public Animator pillarAnimator;
        public int health;
        public int shieldPoint;
        public bool isDestroyed;

        private bool getHurt;
        
        private void Update()
        {
                if (childBeam.isActive && !isDestroyed && !getHurt)
                {
                        switch (pillar)
                        {
                                case Pillar.DownLeft:
                                        pillarAnimator.Play("BL_Open");
                                        break;
                                
                                case Pillar.DownRight:
                                        pillarAnimator.Play("BR_Open");
                                        break;
                                
                                case Pillar.TopLeft:
                                        pillarAnimator.Play("TL_open");
                                        break;
                                
                                case Pillar.TopRight:
                                        pillarAnimator.Play("TR_Open");
                                        break;
                        }
                }
                
                if(!childBeam.isActive && !isDestroyed && !getHurt)
                {
                        switch (pillar)
                        {
                                case Pillar.DownLeft:
                                        pillarAnimator.Play("BL_CloseIdle");
                                        break;
                                
                                case Pillar.DownRight:
                                        pillarAnimator.Play("BR_CloseIdle");
                                        break;
                                
                                case Pillar.TopLeft:
                                        pillarAnimator.Play("TL_CloseIdle");
                                        break;
                                
                                case Pillar.TopRight:
                                        pillarAnimator.Play("TR_CloseIdle");
                                        break;
                        }
                }
        }

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
                        isDestroyed = true;
                        switch (pillar)
                        {
                                  
                                case Pillar.DownLeft:
                                        pillarAnimator.Play("BL_DeadIdle");
                                        break;
                                
                                case Pillar.DownRight:
                                        pillarAnimator.Play("BR_DeadIdle");
                                        break;
                                
                                case Pillar.TopLeft:
                                        pillarAnimator.Play("TL_DeadIdle");
                                        break;
                                
                                case Pillar.TopRight:
                                        pillarAnimator.Play("TR_DeadIdle");
                                        break;
                        }
                }
                else
                {
                        getHurt = true;
                        switch (pillar)
                        {
                                  
                                case Pillar.DownLeft:

                                        if (health < 14)
                                        { 
                                                pillarAnimator.Play("BL_Damage_1");

                                        }

                                        if (health > 14 && health < 28)
                                        {
                                                pillarAnimator.Play("BL_Damage_2");

                                        }

                                        if (health > 28 && health < 42)
                                        {
                                                pillarAnimator.Play("BL_Damage_3");

                                        }
                                        break;
                                
                                case Pillar.DownRight:
                                        
                                        
                                        if (health < 14)
                                        { 
                                                pillarAnimator.Play("BR_Damage_1");
                                        }

                                        if (health > 14 && health < 28)
                                        {
                                                pillarAnimator.Play("BR_Damage_2");
                                        }

                                        if (health > 28 && health < 42)
                                        {
                                                pillarAnimator.Play("BR_Damage_3");
                                        }
                                        break;
                                
                                case Pillar.TopLeft:
                                        
                                        if (health < 14)
                                        { 
                                                pillarAnimator.Play("TL_Damage_1");

                                        }

                                        if (health > 14 && health < 28)
                                        {
                                                pillarAnimator.Play("TL_Damage_2");

                                        }

                                        if (health > 28 && health < 42)
                                        {
                                                pillarAnimator.Play("TL_Damage_3");

                                        }
                                        break;
                                
                                case Pillar.TopRight:
                                        
                                        if (health < 14)
                                        { 
                                                pillarAnimator.Play("TR_Damage_1");
                                        }

                                        if (health > 14 && health < 28)
                                        {
                                                pillarAnimator.Play("TR_Damage_2");

                                        }

                                        if (health > 28 && health < 42)
                                        {
                                                pillarAnimator.Play("TR_Damage_3");

                                        }
                                        break;
                        }
                }

               
        }

        public void ResetHurt()
        {
                getHurt = false;
        }
}
