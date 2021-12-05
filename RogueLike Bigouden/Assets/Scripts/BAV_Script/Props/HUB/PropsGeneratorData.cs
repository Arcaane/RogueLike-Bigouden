using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Props decoration", menuName = "Props/Props Decoration")]
public class PropsGeneratorData : ScriptableObject
{
    //Principal Sprite
    public Sprite baseSpriteSO;
    
    //Sprite swap
    public List<Sprite> spriteSwapSO;
}