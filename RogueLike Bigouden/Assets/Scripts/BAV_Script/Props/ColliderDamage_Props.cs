using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDamage_Props : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        GameObject objTrigger = collider2D.gameObject;
        //Ennemy
        EnnemyStatsManager objEnnemy = objTrigger.GetComponent<EnnemyStatsManager>();
        //Props
        Props_EnvironnementManager objProps = objTrigger.GetComponent<Props_EnvironnementManager>();
        //Props
        PlayerStatsManager objPlayer = objTrigger.GetComponent<PlayerStatsManager>();
        if (collider2D.gameObject.CompareTag("Player"))
        {
            objPlayer.TakeDamage(1);
        }
    }
    
    private void OnTriggerExit2D(Collider2D collider2D)
    {
        throw new NotImplementedException();
    }
}
