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
<<<<<<< HEAD
        transform.Translate(moveDirection * moveSpeed * TimeManager.CustomDeltaTimeEnnemy);
=======
        transform.Translate(moveDirection * moveSpeed * TimeManager.CustomDeltaTimeProjectile);
>>>>>>> BAV_1_12_SlowMo
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ennemy"))
        {
            other.gameObject.GetComponent<MannequinStatsManager>().TakeDamage(damageDealt);
            other.gameObject.GetComponent<EnnemyStatsManager>().TakeDamage(damageDealt);
        }
    }
}