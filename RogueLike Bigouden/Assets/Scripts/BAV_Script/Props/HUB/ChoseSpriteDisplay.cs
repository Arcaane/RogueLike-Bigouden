using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoseSpriteDisplay : MonoBehaviour
{
    public  PropsGeneratorData propsGeneratorData;

    private Sprite baseSpriteSO
    {
        get => propsGeneratorData.baseSpriteSO;
        set => propsGeneratorData.baseSpriteSO = baseSpriteSO;
    }
    
    private List<Sprite> spriteSwapSO
    {
        get => propsGeneratorData.spriteSwapSO;
        set => propsGeneratorData.spriteSwapSO = spriteSwapSO;
    }
    
    //Common Sprite--
    public Sprite baseSpriteData;
    public List<Sprite> spriteSwap;
    public int variantNumber;
    
    //SpriteRenderer
    public SpriteRenderer baseSprite;
    public SpriteRenderer variantSprite;

    public void Start()
    {
        baseSpriteData = baseSpriteSO;
        spriteSwap = spriteSwapSO;
        
        variantNumber = UnityEngine.Random.Range(0, spriteSwap.Count);
        baseSprite.sprite = baseSpriteData; 
        variantSprite.sprite = spriteSwap[variantNumber];
    }


    public void ChangeSprite()
    {
        variantNumber = UnityEngine.Random.Range(0, spriteSwap.Count);
        variantSprite.sprite = spriteSwap[variantNumber];
    }
}
