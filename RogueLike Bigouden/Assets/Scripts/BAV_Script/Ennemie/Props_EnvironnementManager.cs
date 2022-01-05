using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.Animations;
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
    

    [Header("Value to communicate to the Rack")]
    //Private Increment Value
    public float incrementFloat;

    // Common Int
    public int lifePoint; // Point de vie du props.

    //Common Animation.
    public Sprite spriteProps;
    public Color hitcolor;


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
    

    #endregion

    private void Start()
    {
        lifePoint = lifePointSO;
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
                Destroy(gameObject);
            }
            //Si le joueur ne spamm pas 
            else
            {
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
            StartCoroutine(HurtColorTint());
        }
    }


    IEnumerator HurtColorTint()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().DOColor(hitcolor, 0f);
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponentInChildren<SpriteRenderer>().DOColor(resetColor, 0f);
        hurt = false;
    }
}