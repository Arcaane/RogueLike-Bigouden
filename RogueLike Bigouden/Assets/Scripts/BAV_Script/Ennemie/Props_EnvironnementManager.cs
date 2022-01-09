using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    private Sprite spritePropsSO
    {
        get => propsData.spritePropsSO;
        set => propsData.spritePropsSO = spritePropsSO;
    }
    
    private Color spriteHitColorSO
    {
        get => propsData.spriteHitColorSO;
        set => propsData.spriteHitColorSO = spriteHitColorSO;
    }
    //Private Increment Value
    public float incrementFloat;

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
    public Sprite spriteProps;
    public List<Collider2D> propsCollider;
    public Color hitcolor;
    
    //Common 
    public bool needLaunchAnim;
    public float animCounter;
    public float reachAnimCounter;
    public List<Animation> animStockage;

    public bool isProjector;
    //public List<Project> projector;

    //private 
    private Color resetColor = Color.white;
    public bool hurt = false;
    [SerializeField] private float counterBeforeReset;
    private SpriteRenderer spriteRenderer;
    public int incrementDamage;
    
    //Common Sprite--
    public Sprite baseSpriteData;
    public List<Sprite> spriteSwap;
    [Range(0, 2)] public int variantNumber;

    //SpriteRenderer
    [Header("Tcheker")] public bool useBaseSprite;
    public bool useRandomNumber;
    public bool updateInRealtime;
    [Header("Component of Props")] public SpriteRenderer baseSprite;
    public SpriteRenderer variantSprite;
    [Header("Color of the props")] public Color propsColor;
    public Color emissiveColor;
    [Range(-10, 10)] public float intensity;

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
        hitcolor = spriteHitColorSO;
    }

    #region Props Damage & Heal Gestion

    public void TakeDamagePilarDestruction(int damage)
    {
        incrementFloat++;
        lifePoint -= damage;
        hurt = true;
        if (lifePoint <= 0)
        {
            //Si le joueur spamm sur le props 
            if (lifePoint <= -3)
            {
                isDestruct = true;
                Destroy(gameObject);
            }
            //Si le joueur ne spamm pas 
            else
            {
                isDestruct = true;
                //animatorProps.SetBool("Destroy", destroyAnim);
                Destroy(gameObject, 3f);
            } 
        }
    }

    public void TakeDamagePropsDestruction()
    {
        incrementDamage++;
    }

    #endregion

    public void Update()
    {
        if (hurt)
        {
            SpriteSwap();
            //Launch jiggle Animation si Props Projecteur.
            CounterBeforeReset();
        }
    }

    public void SpriteSwap()
    {
        if (hurt)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = hitcolor;
        }
        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = resetColor;
        }
    }

    public void CounterBeforeReset()
    {
        counterBeforeReset += Time.deltaTime;
        if (counterBeforeReset > 0.2f)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = resetColor;
            counterBeforeReset = 0f;
            hurt = false;
        }
    }
}