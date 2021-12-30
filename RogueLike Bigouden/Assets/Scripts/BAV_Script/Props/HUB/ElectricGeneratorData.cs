using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Electric Props", menuName = "Props/Electric Props")]
public class ElectricGeneratorData : ScriptableObject
{
    //Common Int
    public int damageSO;
    public int incrementDamageSO;

    //Common Float
    public float numberOfColliderSO;
    public float delayBetweenArcSO;

    //Common Bool
    public bool isActiveSO; //le props est-il actif ? 
    public bool isContactWaterSO; //le props est-il en contact avec un aquarium
    public bool isTriggerSO; //le props est-il trigger ?

    //Principal Sprite
    public Sprite baseSpriteSO;

    //Sprite swap
    //--------------------SPRITE SWAP TWO DIRECTIONS--------------------//
    [Header("Sprite Two Directions")]
    [Tooltip(
        "TOP/DOWN = 0-5, LEFT/RIGHT = 1-6, TOP/LEFT = 2-7, TOP/RIGHT = 3-8, DOWN/LEFT = 4-9, DOWN/RIGHT = 5-10")]
    public List<Sprite> spriteSwapTwoDir;

    //--------------------SPRITE SWAP THREE DIRECTIONS--------------------//
    [Header("Sprite Three Directions")]
    [Tooltip(
        "6 = RIGHT/TOP/LEFT = 0-4, TOP/LEFT/DOWN = 1-5, LEFT/DOWN/RIGHT = 2-6, DOWN/RIGHT/TOP = 3-7")]
    public List<Sprite> spriteSwapThreeDir;

    //--------------------SPRITE SWAP QUAD DIRECTIONS--------------------//
    [Header("Sprite Quad Directions")] [Tooltip("INTERIOR = 1, EXTERIOR= 2")]
    public List<Sprite> spriteSwapQuadDir;

    //--------------------SPRITE SWAP END LINE--------------------//
    [Header("Sprite End Line")] [Tooltip("LEFT = 0-4, RIGHT = 1-5, TOP = 2-6, DOWN = 3-7")]
    public List<Sprite> spriteSwapEndDir;

    //--------------------ALL SPRITE SWAP--------------------//
    [Header("All Sprite of the Cable")]
    public List<Sprite> spriteSwapSO;

    //Color
    public Color spriteInactiveColorSO;
    public Color spriteActiveColorSO;
}