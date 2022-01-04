using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForUlt : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int damageDealt;
    private Vector2 moveDirection;

    private void Awake()
    {
        damageDealt = PlayerStatsManager.playerStatsInstance.damageUlt;
    }

    private void OnEnable()
    {
        Invoke(("Destroy"), lifeTime);
    }

    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * TimeManager._timeManager.CustomDeltaTimeProjectile);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ennemy"))
        {
            other.gameObject.GetComponent<EnnemyStatsManager>().TakeDamage(damageDealt);
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Sofa"))
        {
            gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Destructible"))
        {
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}