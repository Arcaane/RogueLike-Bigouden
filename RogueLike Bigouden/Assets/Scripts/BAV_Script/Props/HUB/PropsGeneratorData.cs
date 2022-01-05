using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Props decoration", menuName = "Props/Props Decoration")]
public class PropsGeneratorData : ScriptableObject
{
    //Principal Sprite
    public Sprite baseSpriteSO;
    
    //Sprite swap
    public List<Sprite> spriteSwapSO;
    
    //Sprite Color
    public Color hurtColor;
    public Color originalColor = Color.white;
    
    //Animator swap
    public List<AnimatorController> spriteAnimatoSO;
    public List<AnimatorController> spriteAnimatoSecondaySO;
}