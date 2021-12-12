using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Props Destructible", menuName = "Props/Props Destructible")]
public class PropsData : ScriptableObject
{
    // Common Int
    public int lifePointSO; // Point de vie du props.
    public int damageSO; // Damage of the Props.
    public int counterDamageForLaunchAnimSO; // Damage of the Props.

    // Common Float
    public float timeBeforeLaunchAnimationSO; // Delay avant qu'on lance l'animation.
    public float animationSpeedSO; // Vitesse de l'animator.

    // Common Bool
    public bool isDestructibleSO; // Le props est-il destructible ?
    public bool isDamageSO; //Le props est-il endommagé ?
    public bool isDestructSO; //Le props est-il detruit ?
    public bool isTriggerSO; //Le Collider est-il trigger ?

    //Common Animation.
    public Color spriteHitColorSO;
}