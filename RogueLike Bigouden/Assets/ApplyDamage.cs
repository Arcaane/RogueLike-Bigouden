using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDamage : MonoBehaviour
{
    public ProjectilePath _projectilePath;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_projectilePath.isAttacking && other.gameObject.CompareTag("Ennemy"))
        {
            switch (other.gameObject.name)
            {
                case "Ennemy1 _ Shooter":
                    other.gameObject.GetComponent<IAShooter>().TakeDamage(GetComponentInParent<PlayerStatsManager>().damageX);
                    break;
                case "Ennemy2 _ Runner":
                    other.gameObject.GetComponent<IARunner>().TakeDamage(GetComponentInParent<PlayerStatsManager>().damageX);
                    break;
                case "Ennemy3_Barman":
                    other.gameObject.GetComponent<IABarman>().TakeDamage(GetComponentInParent<PlayerStatsManager>().damageX);
                    break;
                case "Ennemy4_Cac":
                    other.gameObject.GetComponent<IACac>().TakeDamage(GetComponentInParent<PlayerStatsManager>().damageX);
                    break;
                default:
                    break;
            }
            Debug.Log("Ennemy damaged : " + other.gameObject.name);
        }
    }
}
