using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForUlt : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 moveDirection;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * TimeManager.CustomDeltaTimeAttack);
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
}