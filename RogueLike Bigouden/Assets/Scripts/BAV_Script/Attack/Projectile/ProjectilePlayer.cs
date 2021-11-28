using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    [Header("Projectile Rouleau")] [SerializeField]
    public float delayBeforeInactive;

    [SerializeField] public float damage;
    [SerializeField] public float speed;
    [SerializeField] public float radiusDamage;
    [SerializeField] public bool deploy;

    [Header("Component")] [SerializeField] private Animator animator;

    //Private Value
    //float---------------------------
    private Vector3 shootDir;

    [Header("Incrementation de float")] 
    [SerializeField] public float _delayIncrementation;

    public void PosShooter(Vector3 shootDir)
    {
        this.shootDir = shootDir;
        transform.eulerAngles = new Vector3(0, 0, UtilsMath.GetAngleFromVectorFloat(shootDir));
    }

    // Update is called once per frame
    void Update()
    {
        Damage();
        MoveBullet();
    }

    void Damage()
    {
        
    }

    void MoveBullet()
    {
        Vector3 moveDir = shootDir * speed * TimeManager.CustomDeltaTimeAttack;
        transform.position += moveDir;
        Debug.Log(moveDir);
    }
}