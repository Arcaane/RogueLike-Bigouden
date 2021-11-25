using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayer : MonoBehaviour
{
    [Header("Projectile Rouleau")] [SerializeField]
    public float delayBeforeInactive;

    [SerializeField] public float damage;
    [SerializeField] public float radiusDamage;
    [SerializeField] public bool deploy;

    [Header("Component")] [SerializeField] private Animator animator;

    //Private Value
    //float---------------------------
    [Header("Incrementation de float")] [SerializeField]
    public float _delayIncrementation;

    [SerializeField] private Transform target;
    
    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        Damage();
        //Direction();
    }

    void Damage()
    {
    }

    /*
    void Direction()
    {
        Vector2 dir = target.position - transform.position;
        float angle = GetAngleFromVectorFloat(dir);
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
    */

    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}