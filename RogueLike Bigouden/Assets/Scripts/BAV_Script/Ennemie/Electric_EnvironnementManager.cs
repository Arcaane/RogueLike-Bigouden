using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[ExecuteAlways]
public class Electric_EnvironnementManager : MonoBehaviour
{
    #region Ennemy Stats Assignation

    public ElectricGeneratorData propsData;

    private int damageSO
    {
        get => propsData.damageSO;
        set => propsData.damageSO = damageSO;
    }

    private int incrementDamageSO
    {
        get => propsData.incrementDamageSO;
        set => propsData.incrementDamageSO = incrementDamageSO;
    }

    private float numberOfColliderSO
    {
        get => propsData.numberOfColliderSO;
        set => propsData.numberOfColliderSO = numberOfColliderSO;
    }

    private float delayBetweenArcSO
    {
        get => propsData.delayBetweenArcSO;
        set => propsData.delayBetweenArcSO = delayBetweenArcSO;
    }

    private bool isActiveSO
    {
        get => propsData.isActiveSO;
        set => propsData.isActiveSO = isActiveSO;
    }

    private bool isContactWaterSO
    {
        get => propsData.isContactWaterSO;
        set => propsData.isContactWaterSO = isContactWaterSO;
    }

    private bool isTriggerSO
    {
        get => propsData.isTriggerSO;
        set => propsData.isTriggerSO = isTriggerSO;
    }


    private List<Sprite> spriteSwapSO
    {
        get => propsData.spriteSwapSO;
        set => propsData.spriteSwapSO = spriteSwapSO;
    }

    private Color spriteInactiveColorSO
    {
        get => propsData.spriteInactiveColorSO;
        set => propsData.spriteInactiveColorSO = spriteInactiveColorSO;
    }

    private Color spriteActiveColorSO
    {
        get => propsData.spriteActiveColorSO;
        set => propsData.spriteActiveColorSO = spriteActiveColorSO;
    }

    #endregion

    [Header("Property of the Electric Trap")]
    //Common Int
    public int damageElect;

    public int incrementDamage;

    //Common Float
    public float numberOfCollider;
    public float delayBetweenArc;

    //Common Bool
    [Header("Status of the Electrick Trap")]
    public bool isActive; //le props est-il actif ? 

    public bool isContactWater; //le props est-il en contact avec un aquarium
    public bool isTrigger; //le props est-il trigger ?

    //Color
    public Color spriteIsActiveColor;


    //Sprite swap
    public List<Sprite> spriteSwap;

    [Tooltip(
        "0 = TOP/DOWN, 1 = LEFT/RIGHT, 2 = TOP/LEFT, 3 = TOP/RIGHT, 4 = DOWN/LEFT, 5 = DOWN/RIGHT, 6 = RIGHT/TOP/LEFT, 7 = TOP/LEFT/DOWN, 8 = LEFT/DOWN/RIGHT, 9 = DOWN/RIGHT/TOP")]
    [Range(0, 7)]
    public int variantNumber;

    //private 
    private Color resetColor = Color.white;
    [SerializeField] private float counterBeforeReset;
    [SerializeField] private float incrementFloatDamage;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private int damageElectData;

    //SpriteRenderer
    [Header("Tcheker")] public bool useBaseSprite;
    public bool useRandomNumber;
    public bool updateInRealtime;

    //--------------------SPRITE RENDERER OF THE ELECTRIC PROPS--------------------//
    [Header("Component of Props")] public SpriteRenderer baseSprite;

    public SpriteRenderer variantSprite;

    //--------------------CHANGE THE COLOR OF THE PROPS--------------------//
    [Header("Color of the props")] public Color propsColor;
    public Color emissiveColor;

    [Range(-10, 10)] public float intensity;

    //--------------------DEBUG PROPS COMPONENT--------------------//
    [Header("Debug Cable")] public bool debug;
    public GameObject debugLine;

    private void Start()
    {
        damageElect = damageSO;
        damageElectData = damageElect;
        incrementDamage = incrementDamageSO;
        numberOfCollider = numberOfColliderSO;
        delayBetweenArc = delayBetweenArcSO;
        isActive = isActiveSO;
        isContactWater = isContactWaterSO;
        isTrigger = isTriggerSO;
        spriteSwap = spriteSwapSO;
        spriteIsActiveColor = spriteInactiveColorSO;
        //--------------------IF USE DEBUG--------------------//
        if (debugLine != null && debug)
        {
            debugLine.SetActive(false);
        }
    }

    public void Update()
    {
        if (isActive)
        {
            SpriteSwap();
            //Launch Particule system for Spark
            CounterBeforeElectricity();
        }

        if (isTrigger)
        {
            DamageElectrity();
        }

        StartCheckSprite();
    }

    #region Swap Sprite

    public void SpriteSwap()
    {
        if (isActive)
        {
            gameObject.GetComponent<SpriteRenderer>().color = spriteIsActiveColor;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = resetColor;
        }

        //--------------------IF UPDATE MODE FOR SWITCHING PROPS--------------------//
        if (updateInRealtime)
        {
            baseSprite.sprite = spriteSwap[variantNumber];
            baseSprite.material.SetColor("_DiffuseColor", propsColor);
            baseSprite.material.SetColor("_DiffuseColor", emissiveColor);
            baseSprite.material.SetFloat("_Intensity", intensity);
        }
    }

    #endregion

    public void CounterBeforeElectricity()
    {
        counterBeforeReset += Time.deltaTime;
        if (counterBeforeReset > delayBetweenArc)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = resetColor;
            counterBeforeReset = 0f;
            isActive = false;
        }
    }

    public void DamageElectrity()
    {
        incrementFloatDamage += Time.deltaTime;
        if (incrementFloatDamage >= delayBetweenArc)
        {
            if (debug)
            {
                debugLine.GetComponent<SpriteRenderer>().color = Color.yellow;
            }

            incrementFloatDamage = 0f;
            damageElect++;
        }
    }

    #region BasicSetupSprite

    public void StartCheckSprite()
    {
        baseSprite.material.SetColor("_DiffuseColor", propsColor);
        baseSprite.material.SetColor("_DiffuseColor", emissiveColor);
        baseSprite.material.SetFloat("_Intensity", intensity);

        if (useBaseSprite && !useRandomNumber)
        {
            baseSprite.sprite = spriteSwap[variantNumber];
        }


        if (useRandomNumber && useBaseSprite)
        {
            variantNumber = UnityEngine.Random.Range(0, spriteSwap.Count);
            if (useBaseSprite)
            {
                baseSprite.sprite = spriteSwap[variantNumber];
            }

            if (!useBaseSprite)
            {
                variantSprite.sprite = spriteSwap[variantNumber];
            }
        }

        if (debug)
        {
            debugLine.SetActive(true);
            if (isActive && !isTrigger)
            {
                debugLine.SetActive(true);
                debugLine.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                debugLine.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else
        {
            debugLine.SetActive(false);
        }
    }

    #endregion

    public void SpriteDirection(int direction)
    {
        switch (direction)
        {
            ////--------------------MONO DIRECTION--------------------////
            //--------------------TOP/DOWN DIRECTION--------------------//
            case 0:
                baseSprite.sprite = spriteSwap[0];
                break;
            //--------------------LEFT/RIGHT DIRECTION--------------------//
            case 1:
                baseSprite.sprite = spriteSwap[0];
                break;
            ////--------------------TWO DIRECTION--------------------////
            //--------------------TOP LEFT DIRECTION--------------------//
            case 2:
                baseSprite.sprite = spriteSwap[0];
                break;
            //--------------------TOP RIGHT DIRECTION--------------------//
            case 3:
                baseSprite.sprite = spriteSwap[0];
                break;
            //--------------------DOWN LEFT DIRECTION--------------------//
            case 4:
                baseSprite.sprite = spriteSwap[0];
                break;
            //--------------------DOWN RIGHT DIRECTION--------------------//
            case 5:
                baseSprite.sprite = spriteSwap[0];
                break;
            ////--------------------THREE DIRECTION--------------------////
            //--------------------RIGHT TOP AND LEFT DIRECTION--------------------//
            case 6:
                baseSprite.sprite = spriteSwap[0];
                break;
            //--------------------TOP LEFT AND DOWN DIRECTION--------------------//
            case 7:
                baseSprite.sprite = spriteSwap[0];
                break;
            //--------------------LEFT DOWN AND RIGHT DIRECTION--------------------//
            case 8:
                baseSprite.sprite = spriteSwap[0];
                break;
            //--------------------DOWN RIGHT AND TOP DIRECTION--------------------//
            case 9:
                baseSprite.sprite = spriteSwap[0];
                break;
            ////--------------------FOUR DIRECTION--------------------////
            //--------------------ALL DIRECTION--------------------//
            case 10:
                baseSprite.sprite = spriteSwap[0];
                break;
        }
    }
}