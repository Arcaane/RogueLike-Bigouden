using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UtilsMath;
using Debug = UnityEngine.Debug;

public class FireBullets : MonoBehaviour
{
    enum Pattern
    {
        Pattern1,
        Pattern2,
        Pattern3,
        Pattern4
    }

    [SerializeField] private Pattern myPattern;

    [Header("Paramater for Ultimate")] [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField] private float notEnoughBulletsInPool = 0.1f;
    [SerializeField] private float numberOfSpline = 2f;
    [SerializeField] private float angle = 0f;

    [SerializeField] private float speedAngle = 0f;

    //Pattern1----------
    [Header("Pattern1")] [SerializeField] private float startAngle = 90f, endAngle = 270f;
    private Vector2 bulletMoveDirection;


    //Pattern4----------
    [Header("Pattern4")] [SerializeField] private Vector2 limitAngle = new Vector2(90f, 270f);
    private float angleIncrement;
    private bool changeSign;

    private void Start()
    {
    }

    private void OnEnable()
    {
        switch (myPattern)
        {
            case Pattern.Pattern1:
                InvokeRepeating("FirePattern1", TimeManager.CustomDeltaTimeAttack,
                    notEnoughBulletsInPool);
                break;
            case Pattern.Pattern2:
                InvokeRepeating("FirePattern2", TimeManager.CustomDeltaTimeAttack,
                    notEnoughBulletsInPool);
                break;
            case Pattern.Pattern3:
                InvokeRepeating("FirePattern3", TimeManager.CustomDeltaTimeAttack,
                    notEnoughBulletsInPool);
                break;
            case Pattern.Pattern4:
                InvokeRepeating("FirePattern4", TimeManager.CustomDeltaTimeAttack,
                    notEnoughBulletsInPool);
                break;
        }
    }

    private void FirePattern1()
    {
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount; i++)
        {
            float bulDirX = transform.position.x - Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y - Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPoolUlt.bulletPoolIntance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<BulletForUlt>().SetMoveDirection(bulDir);
            angle += angleStep;
        }
    }

    public void FirePattern2()
    {
        float bulDirX = transform.position.x - Mathf.Sin((angleIncrement * Mathf.PI) / 180f);
        float bulDirY = transform.position.y - Mathf.Cos((angleIncrement * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

        GameObject bul = BulletPoolUlt.bulletPoolIntance.GetBullet();
        bul.transform.position = transform.position;
        bul.transform.rotation = transform.rotation;
        bul.SetActive(true);
        bul.GetComponent<BulletForUlt>().SetMoveDirection(bulDir);
        angleIncrement += angle * speedAngle;
    }

    public void FirePattern3()
    {
        for (int i = 0; i <= numberOfSpline; i++)
        {
            float bulDirX = transform.position.x -
                            Mathf.Sin(((angleIncrement + (360f / numberOfSpline) * i) * Mathf.PI) / 180f);
            float bulDirY = transform.position.y -
                            Mathf.Cos(((angleIncrement + (360f / numberOfSpline) * i) * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPoolUlt.bulletPoolIntance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<BulletForUlt>().SetMoveDirection(bulDir);
        }

        angleIncrement += angle * speedAngle;

        if (angle >= 360f)
        {
            angle = 0f;
        }
    }

    public void FirePattern4()
    {
        for (int i = 0; i <= numberOfSpline; i++)
        {
            float bulDirX = transform.position.x -
                            Mathf.Sin(((angleIncrement + (360f / numberOfSpline) * i) * Mathf.PI) / 180f);
            float bulDirY = transform.position.y -
                            Mathf.Cos(((angleIncrement + (360f / numberOfSpline) * i) * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPoolUlt.bulletPoolIntance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<BulletForUlt>().SetMoveDirection(bulDir);
        }


        if (angleIncrement >= 360f)
        {
            angleIncrement -= angle * speedAngle;
        }
        else if (angleIncrement <= 360f)
        {
            angleIncrement += angle * speedAngle;
        }
    }
}