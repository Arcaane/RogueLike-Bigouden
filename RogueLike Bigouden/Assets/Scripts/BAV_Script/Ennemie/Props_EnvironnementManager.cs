using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using UnityEngine;

public class Props_EnvironnementManager : MonoBehaviour
{
    #region Ennemy Stats Assignation

    public PropsData propsData;

    private int lifePointSO
    {
        get => propsData.lifePointSO;
        set => propsData.lifePointSO = lifePointSO;
    }

    private int damageSO
    {
        get => propsData.damageSO;
        set => propsData.damageSO = damageSO;
    }
    
    private int counterDamageForLaunchAnimSO
    {
        get => propsData.counterDamageForLaunchAnimSO;
        set => propsData.counterDamageForLaunchAnimSO = counterDamageForLaunchAnimSO;
    }
    
    private float timeBeforeLaunchAnimationSO
    {
        get => propsData.timeBeforeLaunchAnimationSO;
        set => propsData.timeBeforeLaunchAnimationSO = timeBeforeLaunchAnimationSO;
    }
    
    private float animationSpeedSO
    {
        get => propsData.animationSpeedSO;
        set => propsData.animationSpeedSO = animationSpeedSO;
    }
    
    private bool isDestructibleSO
    {
        get => propsData.isDestructibleSO;
        set => propsData.isDestructibleSO = isDestructibleSO;
    }
    
    private bool isDamageSO
    {
        get => propsData.isDamageSO;
        set => propsData.isDamageSO = isDamageSO;
    }
    
    private bool isDestructSO
    {
        get => propsData.isDestructSO;
        set => propsData.isDestructSO = isDestructSO;
    }
    
    private bool isTriggerSO
    {
        get => propsData.isTriggerSO;
        set => propsData.isTriggerSO = isTriggerSO;
    }
    
    private SpriteRenderer spritePropsSO
    {
        get => propsData.spritePropsSO;
        set => propsData.spritePropsSO = spritePropsSO;
    }
    private Animator animatorPropsSO
    {
        get => propsData.animatorPropsSO;
        set => propsData.animatorPropsSO = animatorPropsSO;
    }
    private List<Collider2D> propsColliderSO
    {
        get => propsData.propsColliderSO;
        set => propsData.spritePropsSO = spritePropsSO;
    }


    // Common Int
    public int lifePoint; // Point de vie du props.
    public int damage; // Damage of the Props.
    public int counterDamageForLaunchAnim; // Damage of the Props.

    // Common Float
    public float timeBeforeLaunchAnimation; // Delay avant qu'on lance l'animation.
    public float animationSpeed; // Vitesse de l'animator.

    // Common Bool
    public bool isDestructible; // Le props est-il destructible ?
    public bool isDamage; //Le props est-il endommagé ?
    public bool isDestruct; //Le props est-il detruit ?
    public bool isTrigger; //Le Collider est-il trigger ?

    //Common Animation.
    public SpriteRenderer spriteProps;
    public Animator animatorProps;
    public List<Collider2D> propsCollider;

    //private 
    private Color hitcolor = Color.red;
    private Color notHurtColor = Color.white;
    [SerializeField] bool hurt = false;
    [SerializeField] private float counterBeforeReset;

    #endregion

    private void Start()
    {
        lifePoint = lifePointSO;
        damage = damageSO;
        counterDamageForLaunchAnim = counterDamageForLaunchAnimSO;
        counterDamageForLaunchAnim = counterDamageForLaunchAnimSO;
        timeBeforeLaunchAnimation = timeBeforeLaunchAnimationSO;
        animationSpeed = animationSpeedSO;
        isDestructible = isDestructibleSO;
        isDamage = isDamageSO;
        isDestruct = isDestructSO;
        isTrigger = isTriggerSO;
        spriteProps = spritePropsSO;
        animatorProps = animatorPropsSO;
        propsCollider = propsColliderSO;
    }

    #region Ennemy Damage & Heal Gestion

    //public void TakeDamage(int damage, Animator animatorProps, bool hurtAnim, bool destroyAnim)
    public void TakeDamage(int damage, Animator animatorProps)
    {
        PlayerStatsManager.playerStatsInstance.EarnUltPoint(false);
        lifePoint -= damage;
        hurt = true;
        if (lifePoint <= 0)
        {
            //animatorProps.SetBool("Destroy", destroyAnim);
            Destroy(gameObject, 3f);
        }
    }
    
    #endregion

    public void Update()
    {
        if (hurt)
        {
            SpriteSwap();
            CounterBeforeReset();
        }
    }

    public void SpriteSwap()
    {
        if (hurt)
        {
            //TimeManager.SlowDownGame();
            gameObject.GetComponentInChildren<SpriteRenderer>().color = hitcolor;
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = notHurtColor;
        }
    }

    public void CounterBeforeReset()
    {
        counterBeforeReset += Time.deltaTime;
        if (counterBeforeReset > 0.2f)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = notHurtColor;
            counterBeforeReset = 0f;
            hurt = false;
        }
    }
}