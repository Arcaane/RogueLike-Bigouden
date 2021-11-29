using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    [Header("Projectile Rouleau")]
    [SerializeField] private int damage;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isDeploy;
    
    [Header("Component")] 
    [SerializeField] private Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isDeploy = false;
    }

    public void GoDirection(Vector2 direction, float p_speed, int p_dmg, float p_delay)
    {
        rb.velocity = (direction * p_speed);
        damage = p_dmg;
        Invoke(nameof(ProjectileStop), p_delay);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ennemy") && !isDeploy)
        {
            Debug.Log("Ennemy here");
            ProjectileStop();
            other.GetComponent<MannequinStatsManager>().TakeDamage(damage);
            other.GetComponent<EnnemyStatsManager>().TakeDamage(damage);
        }

        if (other.gameObject.CompareTag("Border") && !isDeploy)
        {
            Debug.Log("Border here");
            ProjectileStop();
        }

        if (other.gameObject.CompareTag("Player") && isDeploy)
        {
            other.gameObject.GetComponent<PlayerAttribut>().p_delay -= other.gameObject.GetComponent<PlayerAttribut>().delayProjectileReduction;
            Destroy(gameObject, 0.2f);
        }
    }

    private void ProjectileStop()
    {
        rb.velocity = Vector2.zero;
        isDeploy = true;
        Debug.Log(isDeploy);
    }
}