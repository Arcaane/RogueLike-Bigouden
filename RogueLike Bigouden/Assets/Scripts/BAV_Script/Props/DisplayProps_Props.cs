using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayProps_Props : MonoBehaviour
{
    public PropsGeneratorData propsGeneratorData;

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

    // Start is called before the first frame update
    void Start()
    {
        baseSpriteData = baseSpriteSO;
        spriteSwap = spriteSwapSO;
        baseSprite.material.SetColor("_DiffuseColor", propsColor);
        baseSprite.material.SetColor("_DiffuseColor", emissiveColor);
        baseSprite.material.SetFloat("_Intensity", intensity);

        if (useBaseSprite && !useRandomNumber)
        {
            baseSprite.sprite = spriteSwap[variantNumber];
        }


        if (useRandomNumber && useBaseSprite)
        {
            variantNumber = Random.Range(0, spriteSwap.Count);
            if (useBaseSprite)
            {
                baseSprite.sprite = spriteSwap[variantNumber];
            }

            if (!useBaseSprite)
            {
                baseSprite.sprite = baseSpriteData;
                variantSprite.sprite = spriteSwap[variantNumber];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (updateInRealtime)
        {
            baseSprite.sprite = spriteSwap[variantNumber];
            baseSprite.material.SetColor("_DiffuseColor", propsColor);
            baseSprite.material.SetColor("_DiffuseColor", emissiveColor);
            baseSprite.material.SetFloat("_Intensity", intensity);
        }
    }
}