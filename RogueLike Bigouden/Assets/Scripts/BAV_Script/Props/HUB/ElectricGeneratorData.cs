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
    public List<Sprite> spriteSwapSO;
    
    //Color
    public Color spriteInactiveColorSO;
    public Color spriteActiveColorSO;
}