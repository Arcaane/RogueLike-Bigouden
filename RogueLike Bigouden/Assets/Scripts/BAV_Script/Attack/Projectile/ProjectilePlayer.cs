using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    [Header("Projectile Rouleau")] [SerializeField]
    private int damage;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isDeploy;

    [Header("Component")] [SerializeField] private Animator animator;
    [SerializeField] float m_MySliderValue;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isDeploy = false;
    }

    public void Start()
    {
        //GoDirection(Vector2.up, 1,1,20);
        animator.speed = m_MySliderValue;
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
            other.GetComponent<EnnemyStatsManager>().TakeDamage(damage);
        }

        if (other.gameObject.CompareTag("Border") && !isDeploy)
        {
            Debug.Log("Border here");
            ProjectileStop();
        }

        if (other.gameObject.CompareTag("Player") && isDeploy)
        {
            other.gameObject.GetComponent<PlayerAttribut>().p_delay -=
                other.gameObject.GetComponent<PlayerAttribut>().delayProjectileReduction;
            Destroy(gameObject, 0.2f);
        }
    }

    private void ProjectileStop()
    {
        rb.velocity = Vector2.zero;
        isDeploy = true;
        Debug.Log(isDeploy);
    }

    void Update()
    {
        if (!isDeploy)
        {
            animator.SetBool("isRotate", true);
        }
        else
        {
            animator.SetBool("isRotate", false);
        }
    }
}