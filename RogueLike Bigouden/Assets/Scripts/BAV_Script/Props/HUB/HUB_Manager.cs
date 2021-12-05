using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUB_Manager : MonoBehaviour
{
    public List<ChoseSpriteDisplay> chooseDisplay;
    public int variantNumber;

    public void Start()
    {
        //TchekDoublon();
    }

    /*
    public void TchekDoublon()
    {
        variantNumber = UnityEngine.Random.Range(0, chooseDisplay.Count);
        if (chooseDisplay[0].variantNumber == chooseDisplay[1].variantNumber)
        {
            chooseDisplay[0].spriteSwap = chooseDisplay[variantNumber].spriteSwap;
        }

        if (chooseDisplay[0].variantNumber == chooseDisplay[2].variantNumber)
        {
            chooseDisplay[0].spriteSwap = chooseDisplay[variantNumber].spriteSwap;
        }

        if (chooseDisplay[0].variantNumber == chooseDisplay[3].variantNumber)
        {
            chooseDisplay[0].spriteSwap = chooseDisplay[variantNumber].spriteSwap;
        }

        if (chooseDisplay[1].variantNumber == chooseDisplay[2].variantNumber)
        {
            chooseDisplay[1].spriteSwap = chooseDisplay[variantNumber].spriteSwap;
        }

        if (chooseDisplay[1].variantNumber == chooseDisplay[3].variantNumber)
        {
            chooseDisplay[1].spriteSwap = chooseDisplay[variantNumber].spriteSwap;
        }

        if (chooseDisplay[2].variantNumber == chooseDisplay[3].variantNumber)
        {
            chooseDisplay[2].spriteSwap = chooseDisplay[variantNumber].spriteSwap;
        }
    }
    */
}