using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UtilsMath;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class FireBullets : MonoBehaviour
{
    enum Pattern
    {
        Pattern1,
        Pattern2,
        Pattern3,
        Pattern4,
        Pattern5
    }

    [SerializeField] private Pattern myPattern;

    [Header("Paramater for Ultimate")] [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField] private float notEnoughBulletsInPool = 0.1f;
    [SerializeField] private Vector2 notEnoughBulletsInPoolRandom;
    [SerializeField] private float numberOfSpline = 2f;
    [SerializeField] private float angle = 0f;

    [SerializeField] private float speedAngle = 0f;
    private Vector2 bulletMoveDirection;
    [SerializeField] private Vector2 limitAngle = new Vector2(90f, 270f);
    private float angleIncrement;
    private float angleIncrement2;
    private bool changeSign;
    
    //private const float tau = 6.28318548f;

    private GameObject bul;
    
    private void OnEnable()
    {
        switch (myPattern)
        {
            case Pattern.Pattern1:
                InvokeRepeating(("FirePattern1"), TimeManager._timeManager.CustomDeltaTimeEnnemy,
                    notEnoughBulletsInPool);
                break;
            case Pattern.Pattern2:
                InvokeRepeating(("FirePattern2"), TimeManager._timeManager.CustomDeltaTimeEnnemy,
                    notEnoughBulletsInPool);
                break;
            case Pattern.Pattern3:
                InvokeRepeating(("FirePattern3"), TimeManager._timeManager.CustomDeltaTimeEnnemy,
                    notEnoughBulletsInPool);
                break;
            case Pattern.Pattern4:
                InvokeRepeating(("FirePattern4"), TimeManager._timeManager.CustomDeltaTimeEnnemy,
                    notEnoughBulletsInPool);
                break;
            case Pattern.Pattern5:
                InvokeRepeating(("FirePattern4"), TimeManager._timeManager.CustomDeltaTimeEnnemy,
                    Random.Range(notEnoughBulletsInPoolRandom.x, notEnoughBulletsInPoolRandom.y));
                InvokeRepeating(("FirePattern5"), TimeManager._timeManager.CustomDeltaTimeEnnemy,
                    Random.Range(notEnoughBulletsInPoolRandom.x, notEnoughBulletsInPoolRandom.y));
                break;
        }
        
        UIManager.instance.RefreshUI();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void FirePattern1()
    {
        float angleStep = (limitAngle.y - limitAngle.x) / bulletsAmount;
        float angle = limitAngle.x;

        for (int i = 0; i < bulletsAmount; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            bul = ObjectPooler.Instance.SpawnFromPool("BulletUlt", transform.position, transform.rotation);
            bul.GetComponent<BulletForUlt>().SetMoveDirection(bulDir);
            angle += angleStep;
        }
    }

    public void FirePattern2()
    {
        float bulDirX = transform.position.x + Mathf.Sin((angleIncrement * Mathf.PI) / 180f);
        float bulDirY = transform.position.y + Mathf.Cos((angleIncrement * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

        bul = ObjectPooler.Instance.SpawnFromPool("BulletUlt", transform.position, transform.rotation);
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

            bul = ObjectPooler.Instance.SpawnFromPool("BulletUlt", transform.position, transform.rotation);
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
        float bulDirX = transform.position.x + Mathf.Sin((angleIncrement * Mathf.PI) / 180f);
        float bulDirY = transform.position.y + Mathf.Cos((angleIncrement * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

        bul = ObjectPooler.Instance.SpawnFromPool("BulletUlt", transform.position, transform.rotation);
        bul.GetComponent<BulletForUlt>().SetMoveDirection(bulDir);

        angleIncrement += angle * speedAngle;
        if (angleIncrement > limitAngle.y)
        {
            angleIncrement = 0;
            angleIncrement = limitAngle.x;
        }
    }

    public void FirePattern5()
    {
        float bulDirX = transform.position.x + Mathf.Sin((angleIncrement2 * Mathf.PI) / 180f);
        float bulDirY = transform.position.y + Mathf.Cos((angleIncrement2 * Mathf.PI) / 180f);

        Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
        Vector2 bulDir = (bulMoveVector - transform.position).normalized;

        bul = ObjectPooler.Instance.SpawnFromPool("BulletUlt", transform.position, transform.rotation);
        bul.GetComponent<BulletForUlt>().SetMoveDirection(bulDir);

        angleIncrement2 -= angle * speedAngle;
        if (angleIncrement2 < limitAngle.x)
        {
            angleIncrement2 = 0;
            angleIncrement2 = limitAngle.y;
        }
    }
}